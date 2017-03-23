namespace eBiB.Models
{
    public class Author
    {
        public int ID { get; private set; }

        public string Name { get; set; }

        public Author(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}