using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FreelancerArticle.Server
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "ReviewService" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы ReviewService.svc или ReviewService.svc.cs в обозревателе решений и начните отладку.
    public partial class FreelancerArticleService
    {
        public bool AddComment(string freelancer, string comment)
        {
            try
            {
                DataBase.Reviews.Add(new Reviews
                {
                    C__Отзыва = GetLastNumberReview() + 1,
                    Фрилансер = freelancer,
                    Комментарий = comment
                });
                DataBase.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private int GetLastNumberReview()
        {
            return DataBase.Reviews.Count() > 0 ? DataBase.Reviews.Select(a => a.C__Отзыва).Max() : 0;
        }

        public List<Reviews> GetListReviews()
        {
            return DataBase.Reviews.ToList();
        }
    }
}
