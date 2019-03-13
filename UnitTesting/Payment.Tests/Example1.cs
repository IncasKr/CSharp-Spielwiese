using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Tests
{

    public interface INetworkClient : IDisposable
    {
        event EventHandler<ReceivedEventArgs> BufferReceived;
        event EventHandler Disconnected;
        void Send(byte[] buffer, int offset, int count);
    }

    public class ReceivedEventArgs : EventArgs
    {
        public byte[] Buffer { get; private set; }
        public int Offset { get; private set; }
        public int Count { get; private set; }

        public ReceivedEventArgs(byte[] buffer)
        {
            Buffer = buffer ?? throw new ArgumentNullException("buffer");
            Offset = 0;
            Count = buffer.Length;
        }
    }
    public class ReceivedMessageEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public ReceivedMessageEventArgs(string message)
        {
            Message = message ?? throw new ArgumentNullException("message");
        }
    }

    public class SomeService
    {
        private readonly INetworkClient _networkClient;
        private string _buffer;

        public SomeService(INetworkClient networkClient)
        {
            _networkClient = networkClient ?? throw new ArgumentNullException("networkClient");
            _networkClient.Disconnected += OnDisconnect;
            _networkClient.BufferReceived += OnBufferReceived;
            Connected = true;
        }

        public bool Connected { get; private set; }

        public event EventHandler<ReceivedMessageEventArgs> MessageReceived = delegate { };

        public void Send(string msg)
        {
            if (msg == null) throw new ArgumentNullException("msg");
            if (Connected == false)
                throw new InvalidOperationException("Not connected");

            var buffer = Encoding.ASCII.GetBytes(msg + "\n");
            _networkClient.Send(buffer, 0, buffer.Length);
        }

        private void OnDisconnect(object sender, EventArgs e)
        {
            Connected = false;
            _buffer = "";
        }

        private void OnBufferReceived(object sender, ReceivedEventArgs e)
        {
            _buffer += Encoding.ASCII.GetString(e.Buffer, e.Offset, e.Count);
            var pos = _buffer.IndexOf('\n');
            while (pos > -1)
            {
                var msg = _buffer.Substring(0, pos);
                MessageReceived(this, new ReceivedMessageEventArgs(msg));

                _buffer = _buffer.Remove(0, pos + 1);
                pos = _buffer.IndexOf('\n');
            }
        }
    }

    [TestFixture]
    public class SomeServiceTests
    {
        [Test]
        public void Service_triggers_msg_event_when_a_complete_message_is_recieved()
        {
            var client = MockRepository.GenerateMock<INetworkClient>();
            var expected = "Hello world";
            var e = new ReceivedEventArgs(Encoding.ASCII.GetBytes(expected + "\n"));
            var actual = "";

            var sut = new SomeService(client);
            sut.MessageReceived += (sender, args) => actual = args.Message;
            client.BufferReceived += new EventHandler<ReceivedEventArgs>((sender, eArg) => { eArg = e; });

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Send_should_invoke_Send_of_networkclient()
        {
            /*var client = Substitute.For<INetworkClient>();
            var msg = "Hello world";

            var sut = new SomeService(client);
            sut.Send(msg);

            client.Received().Send(Arg.Any<byte[]>(), 0, msg.Length + 1);*/
        }

        [Test]
        public void Send_is_not_allowed_while_disconnected()
        {
            /* var client = Substitute.For<INetworkClient>();
             var msg = "Hello world";

             var sut = new SomeService(client);
             client.Disconnected += Raise.Event();
             Action actual = () => sut.Send(msg);

             actual.ShouldThrow<InvalidOperationException>();*/
        }
    }
}
