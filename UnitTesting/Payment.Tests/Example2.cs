using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Tests
{
    public interface IMyNetworkStream
    {
        int Read([In, Out] byte[] buffer, int offset, int size);
        bool DataAvailable { get; }
    }

    public class MyNetworkStream : IMyNetworkStream
    {
        private readonly NetworkStream stream;

        public MyNetworkStream(NetworkStream ns)
        {
            this.stream = ns ?? throw new ArgumentNullException("ns");
        }

        public bool DataAvailable
        {
            get
            {
                return this.stream.DataAvailable;
            }
        }

        public int Read([In, Out] byte[] buffer, int offset, int size)
        {
            return this.stream.Read(buffer, offset, size);
        }

    }

    [TestFixture]
    public class Exmaple2
    {
        static NetworkStream streamer;
        public static void ReadDataIntoBuffer(IMyNetworkStream networkStream, Queue dataBuffer, byte[] tempRXBuffer)
        {
            if ((networkStream != null) && (dataBuffer != null) && (tempRXBuffer != null))
            {
                // read the data from the network stream into the temporary buffer
                while (networkStream.DataAvailable)
                {
                    Int32 numberOfBytesRead = networkStream.Read(tempRXBuffer, 0, tempRXBuffer.Length);
                    streamer.Write(tempRXBuffer, 0, tempRXBuffer.Length);
                    // move all data into the main buffer
                    for (Int32 i = 0; i < numberOfBytesRead; i++)
                    {
                        dataBuffer.Enqueue(tempRXBuffer[i]);
                    }
                }
            }
            else
            {
                if (networkStream == null)
                {
                    throw new ArgumentNullException("networkStream");
                }

                if (dataBuffer == null)
                {
                    throw new ArgumentNullException("dataBuffer");
                }
            }
        }

        [Test]
        public void TestReadDataIntoBuffer()
        {
            var networkStreamMock = new Mock<IMyNetworkStream>();
            var socketMock = new Mock<Socket>() { };
            TcpListener mockServer = new TcpListener(IPAddress.Parse("172.27.234.120"), 23610);
            mockServer.Start();
            Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            soc.Connect("127.0.0.1", 23610);
            streamer = new NetworkStream(soc);

            StringBuilder sb = new StringBuilder();

            sb.Append("_testMessageConstant1");
            sb.Append("_testMessageConstant2");
            sb.Append("_testMessageConstant3");
            sb.Append("_testMessageConstant4");
            sb.Append("_testMessageConstant5");


            // ARRANGE
            byte[] tempRXBuffer = Encoding.UTF8.GetBytes(sb.ToString());

            // return true so that the call to Read() is made
            networkStreamMock.Setup(x => x.DataAvailable).Returns(true);

            networkStreamMock.Setup(x => x.Read(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>())).Callback(() =>
            {
                // after the call to Read() re-setup the property so that
                // we exit the data reading loop again
                networkStreamMock.Setup(x => x.DataAvailable).Returns(false);

            }).Returns(tempRXBuffer.Length);

            Queue resultQueue = new Queue();
            var stream = new StreamReader(streamer, Encoding.Default);

            // ACT
            ReadDataIntoBuffer(networkStreamMock.Object, resultQueue, tempRXBuffer);

            // ASSERT
            Assert.AreEqual(Encoding.UTF8.GetBytes(sb.ToString()), resultQueue.ToArray());
            soc.Disconnect(false);
            mockServer.Stop();
        }
    }
}
