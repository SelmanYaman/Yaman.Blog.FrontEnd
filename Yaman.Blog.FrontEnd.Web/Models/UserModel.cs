using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yaman.Blog.FrontEnd.Web.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
    }
}
