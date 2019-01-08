using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FreelancerArticle.Server
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IFeedbackService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IFeedbackService
    {
        [OperationContract]
        bool AddFeedback(string freelancer, int numberOrder);

        [OperationContract]
        List<Feedback> GetListFeedbacks();

        [OperationContract]
        bool DeleteAllFeedback(int numberOrder);
    }
}
