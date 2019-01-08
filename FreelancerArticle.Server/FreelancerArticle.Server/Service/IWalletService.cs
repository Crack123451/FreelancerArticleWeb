using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FreelancerArticle.Server
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IWalletService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IWalletService
    {
        [OperationContract]
        bool AddWallet(string wallet, decimal sum);

        [OperationContract]
        bool UpdateMoneyInWallet(string wallet, decimal sum);

        [OperationContract]
        List<Wallets> GetListWallets();
    }
}
