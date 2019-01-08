using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FreelancerArticle.Server
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "WalletService" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы WalletService.svc или WalletService.svc.cs в обозревателе решений и начните отладку.
    public partial class FreelancerArticleService
    {
        public bool AddWallet(string wallet, decimal sum)
        {
            try
            {
                DataBase.Wallets.Add(new Wallets
                {
                    C__Кошелька = wallet,
                    Сумма = sum                   
                });
                DataBase.SaveChanges();
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        public bool UpdateMoneyInWallet(string wallet, decimal sum)
        {
            try
            {
                var walletDatabase = DataBase.Wallets.Where(a => a.C__Кошелька == wallet).FirstOrDefault();
                if (walletDatabase == null)
                    return false;
                walletDatabase.Сумма = walletDatabase.Сумма + sum;
                DataBase.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public List<Wallets> GetListWallets()
        {
            return DataBase.Wallets.ToList();
        }
    }
}
