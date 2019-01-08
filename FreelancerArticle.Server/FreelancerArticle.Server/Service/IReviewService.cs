using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FreelancerArticle.Server
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IReviewService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IReviewService
    {
        [OperationContract]
        bool AddComment(string freelancer, string comment);

        [OperationContract]
        List<Reviews> GetListReviews();
    }
}
