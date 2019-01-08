using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FreelancerArticle.Server
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IModeratorService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IMessengerService
    {
        [OperationContract]
        bool AddMessage(string loginUser, string message);

        [OperationContract]
        List<Messenger> GetListMessages();
    }
}
