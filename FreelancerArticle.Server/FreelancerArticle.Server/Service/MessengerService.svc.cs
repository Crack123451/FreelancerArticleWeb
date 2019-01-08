using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FreelancerArticle.Server
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "ModeratorService" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы ModeratorService.svc или ModeratorService.svc.cs в обозревателе решений и начните отладку.
    public partial class FreelancerArticleService
    {
        public bool AddMessage(string loginUser, string message)
        {
            try
            {
                DataBase.Messenger.Add(new Messenger
                {
                    C__Мессенджера = GetLastNumberMessanger() + 1,
                    Время = DateTime.Now,
                    Логин_пользователя = loginUser,
                    Сообщение = message
                });
                DataBase.SaveChanges();
			}
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private int GetLastNumberMessanger()
        {
            return DataBase.Messenger.Count() > 0 ? DataBase.Messenger.Select(a => a.C__Мессенджера).Max() : 0;
        }

        public List<Messenger> GetListMessages()
        {
            return DataBase.Messenger.ToList();
        }
    }
}
