namespace OwinSelfhostSample
{
    public class Data
    {
        public ushort ID { get; set; }
        public string Value { get; set; }

        public Data(ushort id, string value)
        {
            ID = id;
            Value = value;
        }
    }
}
