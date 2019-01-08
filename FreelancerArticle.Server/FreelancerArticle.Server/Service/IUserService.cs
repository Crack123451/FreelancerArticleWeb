using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FreelancerArticle.Server
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IUserService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        bool AddUser(string affiliation, string login, string password, string name, string lastName, string patronymic, string numberWallet);

        [OperationContract]
        bool CheckUserLogin(string Login);

		[OperationContract]
		string GetRoleUser(string login);

		[OperationContract]
        bool CheckUser(string affiliation, string Login, string Password);

        [OperationContract]
        List<Freelancer> GetListFreelancers();

        [OperationContract]
        List<Customer> GetListCustomers();
    }
}
