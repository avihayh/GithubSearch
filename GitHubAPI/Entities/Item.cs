using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubAPI.Entities
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Url { get; set; }

        public Item()
        {

        }

        public Item(int ID,string Name,string Avatar,string Url)
        {
            this.ID = ID;
            this.Name = Name;
            this.Avatar = Avatar;
            this.Url = Url;
        }

    }
}
