using GitHubAPI.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GitHubAPI
{
    public static class GitHubAPIManager
    {
        private const string SearchURL = "https://api.github.com/search/repositories?q=";
        public static List<Item> Search(string QueryString)
        {
            string data = GetData(QueryString);
            if (!string.IsNullOrEmpty(data))
            {
                List<Item> Items = MapData(data);
                return Items;
            }
            return null;
        }

        #region Private methods
        private static List<Item> MapData(string data)
        {
            List<Item> Items = new List<Item>();
            JObject jObj = JObject.Parse(data);
            var arrItems = jObj["items"].ToArray();
            foreach (var item in arrItems)
            {
                Item i = new Item();
                i.ID = int.Parse(item["id"].ToString());
                i.Name = item["name"].ToString();
                i.Avatar = item["owner"]["avatar_url"].ToString();
                i.Url = string.Format("{0}/{1}", item["owner"]["html_url"].ToString(),i.Name);
                Items.Add(i);
            }
            return Items;
        }

        private static string GetData(string QueryString)
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string fullSearchURL = string.Format("{0}{1}", SearchURL, QueryString);
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(fullSearchURL);
                request.UserAgent =
                   "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0;" +
                   ".NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; InfoPath.2;" +
                   ".NET CLR 3.5.21022; .NET CLR 3.5.30729; .NET4.0C; .NET4.0E)";
                request.UseDefaultCredentials = true;
                request.PreAuthenticate = true;
                request.AllowAutoRedirect = false;


                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream s = response.GetResponseStream();
                StreamReader reader = new StreamReader(s, true);
                string data = reader.ReadToEnd();
                return data;
            }
            catch (Exception ex)
            {
                //Handle Execption in some way
                return string.Empty;
            }
        }
        #endregion
    }
}
