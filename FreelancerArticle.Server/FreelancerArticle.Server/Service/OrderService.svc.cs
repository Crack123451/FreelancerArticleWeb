using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FreelancerArticle.Server
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "OrderService" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы OrderService.svc или OrderService.svc.cs в обозревателе решений и начните отладку.
    public partial class FreelancerArticleService
    {
        public bool AddOrder(string loginCustomer, string topic, string title, string description, int countSymbol, decimal money)
        {
            try
            {
                DataBase.Order.Add(new Order
                {
                    C__Заказа = GetLastNumberOrder() + 1,
                     Заказчик = loginCustomer,
                     Тема = topic,
                     Название = title,
                     Подробное_описание = description,
                     Количество_символов = countSymbol,
                     Бюджет = money,
                     Назначенный_фрилансер = null,
                     Состояние = "Нет отклика",
                     Файл = null
                });
                DataBase.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private int GetLastNumberOrder()
        {
            return DataBase.Order.Count() > 0 ? DataBase.Order.Select(a => a.C__Заказа).Max() : 0;
        }

        public bool DeleteOrder(int numberOrder)
        {
            try
            {
                DataBase.Order.Remove(DataBase.Order.Where(a => a.C__Заказа == numberOrder).FirstOrDefault());
                DataBase.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool UpdateStatus(int numberOrder, string status)
        {
            try
            {
                var orderDatabase = DataBase.Order.Where(a => a.C__Заказа == numberOrder).FirstOrDefault();
                if (orderDatabase == null)
                    return false;
                orderDatabase.Состояние = status;
                DataBase.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public List<Order> GetListOrders()
        {
            return DataBase.Order.ToList();
        }

        public bool SendFile(int numberOrder, string file)
        {
            try
            {
                var orderDatabase = DataBase.Order.Where(a => a.C__Заказа == numberOrder).FirstOrDefault();
                if (orderDatabase == null)
                    return false;
                orderDatabase.Файл = file;
                DataBase.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool AssignFreelancer(int numberOrder, string assignedFreelancer)
        {
            try
            {
                var orderDatabase = DataBase.Order.Where(a => a.C__Заказа == numberOrder).FirstOrDefault();
                if (orderDatabase == null)
                    return false;
                orderDatabase.Назначенный_фрилансер = assignedFreelancer;
                orderDatabase.Состояние = "Фрилансер подтвержден";
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
