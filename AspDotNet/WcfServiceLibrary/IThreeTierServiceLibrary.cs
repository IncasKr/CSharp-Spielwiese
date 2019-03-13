using System.ServiceModel;
using WAA.DTO;

namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IThreeTierServiceLibrary
    {
        [OperationContract]
        PersonEntity GetPersonByName(string name);
    }
}
