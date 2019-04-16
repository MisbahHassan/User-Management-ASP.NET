using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UMS_Ass5.Models;

namespace UMS_Ass5.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Admin()
        {
            return View();
        }
        public ActionResult CancelAdmin()
        {
            UMS_Ass5.Models.BAL b = new UMS_Ass5.Models.BAL();
            UserDTO u = new UserDTO();
            List<UserDTO> list = b.getUsersList();
            return View("AdminHome", list);
        }
        [HttpPost]
        public ActionResult LoginAdmin(Models.AdminDTO Admin)
        {
            if (Admin.login == "" || Admin.password == "")
            {
                Response.Write("<script>alert('Please Fill All Fields')</script>");
                return View("Admin");
            }
            else
            {
              
                UMS_Ass5.Models.BAL b = new UMS_Ass5.Models.BAL();
                if (b.isAdminExist(Admin.login, Admin.password))
                {
                    return Redirect("~/Admin/AdminHome");
                }
                else
                {
                    Response.Write("<script>alert('Wrong User or Password')</script>");
                    return View("Admin");
                }
            }
           
        }
        public ActionResult AdminHome()
        {
            UMS_Ass5.Models.BAL b = new UMS_Ass5.Models.BAL();
            UserDTO u= new UserDTO();
            List<UserDTO> list = b.getUsersList();
            return View("AdminHome",list);
        }
        public ActionResult Logout()
        {
            return View("Main");
        }
    
        public ActionResult Edit(int id)
        {
            UMS_Ass5.Models.BAL b = new UMS_Ass5.Models.BAL();
            UserDTO u = new UserDTO();
            u = b.getUserByID(id);
            Session["id"] =u.UserID;
            Session["EditAdmin"] = true;
            return View("NewUser", u);
        }


    }

}