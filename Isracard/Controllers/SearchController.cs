using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Isracard.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Search()
        {
            List<GitHubAPI.Entities.Item> Items = GitHubAPI.GitHubAPIManager.Search(Request.QueryString["srch-term"].ToString());
            ViewBag.SearchResults = Items;
            return View();
        }

        public ActionResult LocalRepo()
        {
            List<GitHubAPI.Entities.Item> Items = (List<GitHubAPI.Entities.Item>)Session["GithabItems"];
            if (Items == null)
            {
                Items = new List<GitHubAPI.Entities.Item>();
            }
            ViewBag.SearchResults = Items;
            return View();
        }

        public string AddBookMark(int ID, string Name, string Avatar, string Url)
        {
            string response = string.Empty;
            List<GitHubAPI.Entities.Item> Items = (List<GitHubAPI.Entities.Item>)Session["GithabItems"];
            if (Items == null)
            {
                Items = new List<GitHubAPI.Entities.Item>();
            }
            if (Items.Count == 0)
            {
                Items.Add(new GitHubAPI.Entities.Item(ID, Name, Avatar, Url));
                response = "Item added";
            }
            else
            {
                var isExist = from i in Items where i.ID == ID select i;
                if (isExist == null || !isExist.Any())
                {
                    Items.Add(new GitHubAPI.Entities.Item(ID, Name, Avatar, Url));
                    response = "Item added";
                }
                else
                {
                    response = "Item already exists in repository";
                }
            }
            if (Items.Count > 0)
            {
                Session["GithabItems"] = Items;
            }
            return response;
        }

        

    }
}
