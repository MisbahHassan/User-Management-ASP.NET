using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS_Ass5.Models
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public string name { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public int age { get; set; }
        public string nic { get; set; }
        public DateTime dob { get; set; }
        public bool cricket { get; set; }
        public bool hockey { get; set; }
        public bool chess { get; set; }
        public string imageName { get; set; }
        public DateTime createdOn { get; set; }
        public string email { get; set; }
    }
}