using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FreelancerArticle.Server
{
	// ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "UserService" в коде, SVC-файле и файле конфигурации.
	// ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы UserService.svc или UserService.svc.cs в обозревателе решений и начните отладку.
	public partial class FreelancerArticleService : IUserService, IFeedbackService, IWalletService, IMessengerService, IOrderService, IReviewService
	{
		private FreelancerArticleBaseEntities DataBase = new FreelancerArticleBaseEntities();

		public bool AddUser(string role, string login, string password, string name, string lastName, string patronymic, string numberWallet)
		{
			if (CheckUserLogin(login) == false)
			{
				//var hashPassword = Hashing.HashPassword(password);
				if (role == "Freelancer")
					DataBase.Freelancer.Add(new Freelancer
					{
						Логин = login,
						Пароль = password,
						Имя = name,
						Фамилия = lastName,
						Отчество = patronymic,
						C__Кошелька = numberWallet
					});
				if (role == "Customer")
					DataBase.Customer.Add(new Customer
					{
						Логин = login,
						Пароль = password,
						Имя = name,
						Фамилия = lastName,
						Отчество = patronymic,
						C__Кошелька = numberWallet
					});
				DataBase.SaveChanges();
				return true;
			}
			return false;
		}

		public bool CheckUserLogin(string login)
		{
			return (DataBase.Freelancer.Where(a => a.Логин == login).Count() > 0
				|| DataBase.Customer.Where(a => a.Логин == login).Count() > 0);
		}

		public string GetRoleUser(string login)
		{
			if (DataBase.Freelancer.Where(a => a.Логин == login).Count() > 0)
				return "Freelancer";
			else if (DataBase.Customer.Where(a => a.Логин == login).Count() > 0)
				return "Customer";
			else if (DataBase.Moderator.Where(a => a.Логин == login).Count() > 0)
				return "Moderator";
			return String.Empty;
		}

		/*public bool CheckUser(string affiliation, string login, string password)
        {
            IQueryable<string> hashPassword;
            if (affiliation == "Freelancer")
                hashPassword = DataBase.Freelancer.Where(a => a.Логин == login)?.Select(a => a.Пароль);
            else if (affiliation == "Customer")
                hashPassword = DataBase.Customer.Where(a => a.Логин == login)?.Select(a => a.Пароль);
            else if (affiliation == "Moderator")
                hashPassword = DataBase.Moderator.Where(a => a.Логин == login)?.Select(a => a.Пароль);
            else
                return false;
            if (hashPassword != null && hashPassword.Count() > 0)
                return Hashing.VerifyHashedPassword(hashPassword.First(), password);
            return false;
        }*/

		public bool CheckUser(string role, string login, string password)
		{
			if (role == "Freelancer")
				return DataBase.Freelancer.Where(a => a.Логин == login && a.Пароль == password).Count() > 0;
			else if (role == "Customer")
				return DataBase.Customer.Where(a => a.Логин == login && a.Пароль == password).Count() > 0;
			else if (role == "Moderator")
				return DataBase.Moderator.Where(a => a.Логин == login && a.Пароль == password).Count() > 0;
			else
				return false;
		}

		public List<Freelancer> GetListFreelancers()
		{
			return DataBase.Freelancer.ToList();
		}

		public List<Customer> GetListCustomers()
		{
			return DataBase.Customer.ToList();
		}
	}
}
