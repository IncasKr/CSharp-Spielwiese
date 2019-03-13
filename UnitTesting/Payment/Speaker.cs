namespace Payment
{
    public delegate string NameSourceDelegate(string first, string surname);

    public class Speaker
    {
        private readonly string firstName;
        private readonly string surname;
        private INameSource nameSource;
        public Speaker(string firstName, string surname, INameSource nameSource)
        {
            this.firstName = firstName;
            this.surname = surname;
            this.nameSource = nameSource;
        }
        public string Introduce()
        {
            string name = nameSource.CreateName(firstName, surname);
            return string.Format("Hi, my name is {0}", name);
        }
    }
}
