using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class SalesforceInfo
{
    public string ConsumerKey;
    public string ConsumerSecret;
    public string UserName;
    public string Password;
    public string Token;
    public string oauthToken;
    public string sessionID;
    public string serviceURL;
    public string loginPassword;
    public async Task<string> Login(){
        loginPassword =  Password + Token;
        
        Console.WriteLine("Logging in to Salesforce...");

        HttpClient authClient = new HttpClient();
        HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string>
              {
                 {"grant_type","password"},
                 {"client_id",ConsumerKey},
                 {"client_secret",ConsumerSecret},
                 {"username",UserName},
                 {"password",loginPassword}
               }
        );
        HttpResponseMessage message = await authClient.PostAsync("https://login.salesforce.com/services/oauth2/token", content);
        string responseString = await message.Content.ReadAsStringAsync();
        JObject obj = JObject.Parse(responseString);
        oauthToken = (string)obj["access_token"];
        serviceURL = (string)obj["instance_url"];

        Console.WriteLine("Logged In.");

        return "Finished Login";
    }
    public async Task<string> RefreshDashboard(List<string> dashboardIDs) {
        foreach (string dashboard in dashboardIDs)
        {
            Console.WriteLine("Refreshing Dashboard - "+dashboard);
            HttpClient queryClient = new HttpClient();
            string restQuery = serviceURL + "/services/data/v31.0/analytics/dashboards/" + dashboard;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, restQuery);
            request.Headers.Add("Authorization", "Bearer " + oauthToken);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await queryClient.SendAsync(request);
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Dashboard " + dashboard + " Refreshed");
        }
        return "Refreshed!";
    }
    
}
