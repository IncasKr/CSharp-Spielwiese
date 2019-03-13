using System.Runtime.Serialization;

namespace WAA.DTO
{
    [DataContract]
    public class PersonEntity
    {
        [DataMember]
        public string ID { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string FirstName { get; set; }
    }
}
