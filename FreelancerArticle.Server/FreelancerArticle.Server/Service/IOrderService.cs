using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FreelancerArticle.Server
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IOrderService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        bool AddOrder(string loginCustomer, string topic, string title, string description, int countSymbol, decimal money);

        [OperationContract]
        bool DeleteOrder(int numberOrder);

        [OperationContract]
        bool UpdateStatus(int numberOrder, string status);

        [OperationContract]
        List<Order> GetListOrders();

        [OperationContract]
        bool SendFile(int numberOrder, string file);

        [OperationContract]
        bool AssignFreelancer(int numberOrder, string assignedFreelancer);
    }
}
