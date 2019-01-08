using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;

namespace FreelancerArticle.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(FreelancerArticle.Server.FreelancerArticleService)))
            {
                host.Open();
                Console.WriteLine("Host started...");
                Console.ReadKey();
            }
        }
    }
}
