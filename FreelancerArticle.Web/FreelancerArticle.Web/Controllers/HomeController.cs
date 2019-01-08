using FreelancerArticle.Web.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreelancerArticle.Web.Controllers
{
	public class HomeController : Controller
	{
		private UserServiceClient _clientUser = new UserServiceClient("BasicHttpBinding_IUserService");
		private WalletServiceClient _clientWallet = new WalletServiceClient("BasicHttpBinding_IWalletService");
		private OrderServiceClient _clientOrder = new OrderServiceClient("BasicHttpBinding_IOrderService");
		private MessengerServiceClient _clientModerator = new MessengerServiceClient("BasicHttpBinding_IMessengerService");
		private ReviewServiceClient _clientReview = new ReviewServiceClient("BasicHttpBinding_IReviewService");
		private FeedbackServiceClient _clientFeedback = new FeedbackServiceClient("BasicHttpBinding_IFeedbackService");
		private int _pageSize = 10;

		[Authorize]
		public ActionResult Index(int pageNum = 0)
		{
			var role = string.Empty;
			try
			{
				role = _clientUser.GetRoleUser(User.Identity.Name);
			}
			catch (System.ServiceModel.EndpointNotFoundException ex)
			{
				ModelState.AddModelError(String.Empty, "Ошибка подключения к серверу. Сервер недоступен");
			}
			/*try
			{
				ViewBag.ListAccounts = _clientUser.GetListFreelancers().OrderBy(b => b.Имя).Skip(_pageSize * pageNum).Take(_pageSize).ToList();
			}
			catch (System.ServiceModel.EndpointNotFoundException ex)
			{
				ModelState.AddModelError(String.Empty, "Ошибка подключения к серверу. Сервер недоступен");
			}
			ViewBag.PageNum = pageNum;
			ViewBag.ItemsCount = _clientUser.GetListFreelancers().Count();
			ViewBag.PageSize = _pageSize;
			return View();*/
			if (role == "Freelancer")
				return RedirectToAction("IndexFreelancer", "Home");
			else if (role == "Customer")
				return RedirectToAction("IndexCustomer", "Home");
			else if (role == "Moderator")
				return RedirectToAction("IndexModerator", "Home");
			return RedirectToAction("Login", "Account");
		}

		[Authorize]
		public ActionResult IndexFreelancer(int pageNum = 0)
		{
			var role = string.Empty;
			try
			{
				role = _clientUser.GetRoleUser(User.Identity.Name);
			}
			catch (System.ServiceModel.EndpointNotFoundException ex)
			{
				ModelState.AddModelError(String.Empty, "Ошибка подключения к серверу. Сервер недоступен");
			}
			if (role != "Freelancer")
				return RedirectToAction("Index", "Home");
			try
			{
				var user = _clientUser.GetListFreelancers().Where(x => x.Логин == User.Identity.Name).FirstOrDefault();
				var wallet = _clientWallet.GetListWallets().Where(x => x.C__Кошелька == user.C__Кошелька).FirstOrDefault();
				var orders = _clientOrder.GetListOrders().Where(x => x.Назначенный_фрилансер == User.Identity.Name || x.Состояние == "Нет отклика" || x.Состояние == "Есть отклик").ToList();
				var dictFeedback = new Dictionary<int, bool>();
				foreach(var order in orders)
				{
					if (_clientFeedback.GetListFeedbacks().Where(x => x.C__Заказа == order.C__Заказа && x.Фрилансер == User.Identity.Name).ToList().Count > 0)
						dictFeedback.Add(order.C__Заказа, true);
					else
						dictFeedback.Add(order.C__Заказа, false);
				}
				ViewBag.User = user;
				ViewBag.Wallet = wallet;
				ViewBag.Orders = orders;
				ViewBag.DictFeedback = dictFeedback;
			}
			catch (System.ServiceModel.EndpointNotFoundException ex)
			{
				ModelState.AddModelError(String.Empty, "Ошибка подключения к серверу. Сервер недоступен");
			}
			ViewBag.Role = role;
			return View();
		}

		[Authorize]
		public ActionResult IndexCustomer(int pageNum = 0)
		{
			var role = string.Empty;
			try
			{
				role = _clientUser.GetRoleUser(User.Identity.Name);
			}
			catch (System.ServiceModel.EndpointNotFoundException ex)
			{
				ModelState.AddModelError(String.Empty, "Ошибка подключения к серверу. Сервер недоступен");
			}
			if (role != "Customer")
				return RedirectToAction("Index", "Home");
			try
			{
				var user = _clientUser.GetListCustomers().Where(x => x.Логин == User.Identity.Name).FirstOrDefault();
				var wallet = _clientWallet.GetListWallets().Where(x => x.C__Кошелька == user.C__Кошелька).FirstOrDefault();
				var orders = _clientOrder.GetListOrders().Where(x => x.Заказчик == User.Identity.Name).ToList();
				ViewBag.User = user;
				ViewBag.Wallet = wallet;
				ViewBag.Orders = orders;
			}
			catch (System.ServiceModel.EndpointNotFoundException ex)
			{
				ModelState.AddModelError(String.Empty, "Ошибка подключения к серверу. Сервер недоступен");
			}
			ViewBag.Role = role;
			ViewBag.ClientUser = _clientUser;
			ViewBag.ClientWallet = _clientWallet;
			return View();
		}

		[Authorize]
		public ActionResult IndexModerator(int pageNum = 0)
		{			
			return View();
		}

		[HttpPost]
		public ActionResult UpdateCash(string cash)
		{
			try
			{
				var role = _clientUser.GetRoleUser(User.Identity.Name);
				if (role == "Customer")
				{
					var user = _clientUser.GetListCustomers().Where(x => x.Логин == User.Identity.Name).FirstOrDefault();
					if (Decimal.TryParse(cash, out decimal sum))
						_clientWallet.UpdateMoneyInWallet(user.C__Кошелька, sum);
				}
			}
			catch (System.ServiceModel.EndpointNotFoundException ex)
			{
				ModelState.AddModelError(String.Empty, "Ошибка подключения к серверу. Сервер недоступен");
			}
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public ActionResult SendMessage(string message)
		{
			if (message.Length == 0)
				return RedirectToAction("Index", "Home");
			try
			{
				var role = _clientUser.GetRoleUser(User.Identity.Name);
				if (role == "Freelancer" || role == "Customer")
					_clientModerator.AddMessage(User.Identity.Name, message);
			}
			catch (System.ServiceModel.EndpointNotFoundException ex)
			{
				ModelState.AddModelError(String.Empty, "Ошибка подключения к серверу. Сервер недоступен");
			}
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public ActionResult AddWork(string topic, string title, string description, string countSymbols, string money)
		{
			if (topic.Length == 0 
				|| title.Length == 0 
				|| description.Length == 0 
				|| !Int32.TryParse(countSymbols, out int totalCountSymbols) 
				|| !Decimal.TryParse(money, out decimal totalMoney))
				return RedirectToAction("Index", "Home");			
			try
			{
				var role = _clientUser.GetRoleUser(User.Identity.Name);
				if (role == "Customer")
				{
					var myWallet = _clientUser.GetListCustomers().Where(l => l.Логин == User.Identity.Name).Select(a => a.C__Кошелька).FirstOrDefault();
					var myMoney = _clientWallet.GetListWallets().Where(x => x.C__Кошелька == myWallet).Select(a => a.Сумма).FirstOrDefault();
					if(myMoney < totalMoney)
						return RedirectToAction("Index", "Home");
					_clientOrder.AddOrder(User.Identity.Name, topic, title, description, totalCountSymbols, totalMoney);
					_clientWallet.UpdateMoneyInWallet(myWallet, -totalMoney);
				}
			}
			catch (System.ServiceModel.EndpointNotFoundException ex)
			{
				ModelState.AddModelError(String.Empty, "Ошибка подключения к серверу. Сервер недоступен");
			}
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public ActionResult DeleteWork(int id)
		{
			try
			{
				var role = _clientUser.GetRoleUser(User.Identity.Name);
				if (role == "Customer")
				{
					var myWallet = _clientUser.GetListCustomers().Where(l => l.Логин == User.Identity.Name).Select(a => a.C__Кошелька).FirstOrDefault();
					var moneyArticle = _clientOrder.GetListOrders().Where(x => x.C__Заказа == id).Select(a => a.Бюджет).FirstOrDefault();
					_clientFeedback.DeleteAllFeedback(id);
					_clientOrder.DeleteOrder(id);
					_clientWallet.UpdateMoneyInWallet(myWallet, moneyArticle);
				}
			}
			catch (System.ServiceModel.EndpointNotFoundException ex)
			{
				ModelState.AddModelError(String.Empty, "Ошибка подключения к серверу. Сервер недоступен");
			}
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public ActionResult Conflict(int id)
		{
			try
			{
				var role = _clientUser.GetRoleUser(User.Identity.Name);
				if (role == "Customer")
				{
					_clientOrder.UpdateStatus(id, "Конфликт");
				}
			}
			catch (System.ServiceModel.EndpointNotFoundException ex)
			{
				ModelState.AddModelError(String.Empty, "Ошибка подключения к серверу. Сервер недоступен");
			}
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public ActionResult СonfirmWork(int id, string freelancer, string money)
		{
			try
			{
				var role = _clientUser.GetRoleUser(User.Identity.Name);
				if (role == "Customer")
				{
					_clientOrder.UpdateStatus(id, "Работа подтверждена");
					var walletFreelancer = _clientUser.GetListFreelancers().Where(x => x.Логин == freelancer).Select(x => x.C__Кошелька).FirstOrDefault();
					_clientWallet.UpdateMoneyInWallet(walletFreelancer, Decimal.Parse(money));
				}
			}
			catch (System.ServiceModel.EndpointNotFoundException ex)
			{
				ModelState.AddModelError(String.Empty, "Ошибка подключения к серверу. Сервер недоступен");
			}
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public ActionResult Review(string freelancerName, string message)
		{
			if(freelancerName.Length == 0 || message.Length == 0)
				return RedirectToAction("Index", "Home");
			try
			{
				var role = _clientUser.GetRoleUser(User.Identity.Name);
				if (role == "Customer")
				{
					_clientReview.AddComment(freelancerName, message);
				}
			}
			catch (System.ServiceModel.EndpointNotFoundException ex)
			{
				ModelState.AddModelError(String.Empty, "Ошибка подключения к серверу. Сервер недоступен");
			}
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public JsonResult FillViewWork(int id)
		{
			try
			{
				var role = _clientUser.GetRoleUser(User.Identity.Name);
				if (role == "Customer" || role == "Freelancer")
				{
					var order = _clientOrder.GetListOrders().Where(x => x.C__Заказа == id).FirstOrDefault();					
					return Json ( new { topic = order.Тема, title = order.Название, description = order.Подробное_описание, countSymbols = order.Количество_символов, money = order.Бюджет }, JsonRequestBehavior.AllowGet);
				}
			}
			catch (System.ServiceModel.EndpointNotFoundException ex)
			{
				ModelState.AddModelError(String.Empty, "Ошибка подключения к серверу. Сервер недоступен");
			}
			return null;
		}

		[HttpGet]
		public string DownloadWork(int id)
		{
			try
			{
				var role = _clientUser.GetRoleUser(User.Identity.Name);
				if (role == "Customer")
				{
					var order = _clientOrder.GetListOrders().Where(x => x.C__Заказа == id).FirstOrDefault();
					return order.Файл;
				}
			}
			catch (System.ServiceModel.EndpointNotFoundException ex)
			{
				ModelState.AddModelError(String.Empty, "Ошибка подключения к серверу. Сервер недоступен");
			}
			return String.Empty;
		}

		[HttpGet]
		public string AttachWorkDone(int id, string file)
		{
			try
			{
				var role = _clientUser.GetRoleUser(User.Identity.Name);
				if (role == "Freelancer")
				{
					var order = _clientOrder.SendFile(id, file);
					_clientOrder.UpdateStatus(id, "Работа выполнена");
				}
			}
			catch (System.ServiceModel.EndpointNotFoundException ex)
			{
				ModelState.AddModelError(String.Empty, "Ошибка подключения к серверу. Сервер недоступен");
			}
			return String.Empty;
		}

		[HttpPost]
		public JsonResult GetAllFeedback(int id)
		{
			try
			{
				var role = _clientUser.GetRoleUser(User.Identity.Name);
				if (role == "Customer")
				{
					var feedbacksFreelancers = _clientFeedback.GetListFeedbacks().Where(x => x.C__Заказа == id ).ToList();
					return Json(feedbacksFreelancers);
				}
			}
			catch (System.ServiceModel.EndpointNotFoundException ex)
			{
				ModelState.AddModelError(String.Empty, "Ошибка подключения к серверу. Сервер недоступен");
			}
			return null;
		}

		[HttpPost]
		public JsonResult GetInfoAboutFreelancer(string freelancer)
		{
			try
			{
				var role = _clientUser.GetRoleUser(User.Identity.Name);
				if (role == "Customer")
				{
					var infoAboutFreelancer = _clientUser.GetListFreelancers().Where(x => x.Логин == freelancer).FirstOrDefault();
					return Json(infoAboutFreelancer);
				}
			}
			catch (System.ServiceModel.EndpointNotFoundException ex)
			{
				ModelState.AddModelError(String.Empty, "Ошибка подключения к серверу. Сервер недоступен");
			}
			return null;
		}

		[HttpPost]
		public JsonResult GetAllReviews(string freelancer)
		{
			try
			{
				var role = _clientUser.GetRoleUser(User.Identity.Name);
				if (role == "Customer")
				{
					var allReviews = _clientReview.GetListReviews().Where(x => x.Фрилансер == freelancer).ToList();
					if (allReviews.Count == 0)
						return null;
					return Json(allReviews.Select(a => a.Комментарий).ToList());
				}
			}
			catch (System.ServiceModel.EndpointNotFoundException ex)
			{
				ModelState.AddModelError(String.Empty, "Ошибка подключения к серверу. Сервер недоступен");
			}
			return null;
		}

		[HttpGet]
		public ActionResult ConfirmFreelancer(int id, string loginFreelancer)
		{
			try
			{
				var role = _clientUser.GetRoleUser(User.Identity.Name);
				if (role == "Customer")
				{
					_clientOrder.AssignFreelancer(id, loginFreelancer);
				}
			}
			catch (System.ServiceModel.EndpointNotFoundException ex)
			{
				ModelState.AddModelError(String.Empty, "Ошибка подключения к серверу. Сервер недоступен");
			}
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public ActionResult DoFeedback(int id)
		{
			try
			{
				var role = _clientUser.GetRoleUser(User.Identity.Name);
				if (role == "Freelancer")
				{
					_clientOrder.UpdateStatus(id, "Есть отклик");
					_clientFeedback.AddFeedback(User.Identity.Name, id);
				}
			}
			catch (System.ServiceModel.EndpointNotFoundException ex)
			{
				ModelState.AddModelError(String.Empty, "Ошибка подключения к серверу. Сервер недоступен");
			}
			return RedirectToAction("Index", "Home");
		}
	}
}