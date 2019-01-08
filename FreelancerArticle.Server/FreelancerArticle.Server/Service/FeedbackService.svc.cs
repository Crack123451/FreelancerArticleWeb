using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FreelancerArticle.Server
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "FeedbackService" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы FeedbackService.svc или FeedbackService.svc.cs в обозревателе решений и начните отладку.
    public partial class FreelancerArticleService
    {        
        public bool AddFeedback(string freelancer, int numberOrder)
        {
            try
            {
                DataBase.Feedback.Add(new Feedback
                {
                    C__Отклика = GetLastNumberFeedback() + 1,
                    Фрилансер = freelancer,
                    C__Заказа = numberOrder
                });
                DataBase.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private int GetLastNumberFeedback()
        {
            return DataBase.Feedback.Count() > 0 ? DataBase.Feedback.Select(a => a.C__Отклика).Max() : 0;
        }

        public List<Feedback> GetListFeedbacks()
        {
            return DataBase.Feedback.ToList();
        }

        public bool DeleteAllFeedback(int numberOrder)
        {
            try
            {
				var allfeedbacks = DataBase.Feedback.Where(a => a.C__Заказа == numberOrder).ToList();
				int counter = allfeedbacks.Count;
				for (int i = 0; i < counter; i++)
					DataBase.Feedback.Remove(allfeedbacks.First());
                DataBase.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
