using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using enterprise = SalesforceConsole.SfdcReference;

namespace SalesforceConsole
{
    class Program
    {
     
        public static void Main(string[] args)
        {
            SalesforceInfo SFInfo = new SalesforceInfo();
            SFInfo.ConsumerKey = "****Consumer Key Here****";
            SFInfo.ConsumerSecret = "****Consumer Secret Here****";
            SFInfo.UserName = "****Username Here****";
            SFInfo.Password = "****Salesforce Password Here****";
            SFInfo.Token = "****Salesforce App Token Here****";
            
            List<string> dashboardIDs = new List<string>();
            dashboardIDs.Add("*******List Dashboard IDs Here********");

            Task SFLogin = SFInfo.Login();
            SFLogin.Wait();

            Task SFRefresh = SFInfo.RefreshDashboard(dashboardIDs);
            SFRefresh.Wait();

            Console.WriteLine("Done Refreshing Dashboards.");
            Console.WriteLine("Bye Bye!");
        }
        
    }
}
