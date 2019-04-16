using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.Mvc;
using UMS_Ass5.Models;

namespace UMS_Ass5.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Main()
        {
            return View();
        }
        public ActionResult Exit()
        {
            return View("Main");
        }
        public ActionResult NewUser()
        {
            Models.UserDTO user = new Models.UserDTO();
            return View("NewUser", user);
        }
        public ActionResult ExistingUser()
        {
            return View("ExistingUser");
        }
        public ActionResult CancelUser()
        {
            //return Redirect("~/User/Main");
            return View("Main");
        }

        [HttpPost]
        public ActionResult CreateUser(Models.UserDTO user)
        {
            UMS_Ass5.Models.BAL b = new UMS_Ass5.Models.BAL();
            if (user.name == "" || user.login == "" || user.imageName == "" || user.nic == "" || user.password == "" || user.email == "" || user.address == "" || Convert.ToInt32(user.age) < 1 || user.nic == "")
            {
                Response.Write("<script>alert('Please Fill All Fields')</script>");
                return View("NewUser",user);
            }
                        user.age = Convert.ToInt32(Request["age"]);

            if(user.age < 1)
            {
                Response.Write("<script>alert('Age Must Be greater than 1')</script>");
                return View("NewUser", user);

            }
            user.gender = Request["gender"];

            if (Request["hockey"] == "on")
            {
                user.hockey = true;
            }
            if (Request["cricket"] == "on")
            {
                user.cricket = true;
            }
            if (Request["chess"] == "on")
            {
                user.chess = true;
            }

            var image = Request.Files["imageName"];
            var uniqueName = "";

            if (Request.Files["imageName"] != null)
            {
                if (image.FileName != null)
                {
                    var extension = System.IO.Path.GetExtension(image.FileName);
                    uniqueName = Guid.NewGuid().ToString() + extension;
                    var rootPath = Server.MapPath("~/uploadedFiles");
                    var saveFilePath = System.IO.Path.Combine(rootPath, uniqueName);
                    image.SaveAs(saveFilePath);
                }
            }
            bool value = false;
            user.imageName = uniqueName;
            if (Session["EditAdmin"] != null)
            {
                value = (bool)Session["EditAdmin"];
            }

            if (value == true)
            {
                UMS_Ass5.Models.BAL obj = new UMS_Ass5.Models.BAL();
                UserDTO us = new UserDTO();
                user.UserID = Convert.ToInt32(Session["id"]);
                bool isUpdate = obj.UpdateUser(user);
               
                if (isUpdate==true)
                {
                    Response.Write("<script>alert('Updated Successfully')</script>");
                    return View("Home", user);
                }
                else
                {
                    Response.Write("<script>alert('Some Problem Occurred')</script>");
                    return View("NewUser", user);
                }
            }
            else
            {
                if (b.isUserExist(user.login, user.nic, user.email))
                {
                    Response.Write("<script>alert('User Already Exists')</script>");
                    return View("NewUser", user);
                }
                else
                {
                    user.dob = DateTime.Parse(Request["dob"]);
                    /* ViewBag.name = user.name;
                     ViewBag.image = user.imageName;
                     value="@String.Format("{0:yyyy-MM-dd}",Model.dob)"*/
                    if (b.saveUser(user))
                    {
                        Response.Write("<script>alert('Succesfully Saved')</script>");
                        UserDTO NewUSer = new UserDTO();
                        NewUSer = b.getUserByLogin(user.login);
                        return View("Home", NewUSer);
                    }
                    else
                    {
                        Response.Write("<script>alert('Some Problem has Occured')</script>");

                        return View("NewUser", user);
                    }
                }
               
            }
        }
     [HttpPost]
        public ActionResult Login(String login,String password)
        {
            if (login == "" || password == "")
            {
                Response.Write("<script>alert('Please Fill All Fields')</script>");
                return View("ExistingUser");
            }
            else
            {
                UMS_Ass5.Models.BAL b = new UMS_Ass5.Models.BAL();
                Models.UserDTO user = new Models.UserDTO();
                user = b.getUserByLogin(login);
                TempData["UserID"] = user.UserID; ;
                return View("Home", user);
            }
        }
        [HttpPost]
        public ActionResult ForgetPassword(string email)
        {
            UMS_Ass5.Models.BAL bal = new UMS_Ass5.Models.BAL();
            Random rnd = new Random();
            int num = rnd.Next(1, 1000);
            string code = Convert.ToString(num);
            if (bal.sendEmail(email, "Reset Password Code", code))
            {
                Session["ResetCode"] = code;
                Session["Email"] = email;
                return View("ResetCode");
            }
            else
            {
                Response.Write("<script>alert('Email Not Exists')</script>");
                return View("ExistingUser");
            }
        }
        public ActionResult ResetCode(string code)
        {
            if (code == "")
            {
                Response.Write("<script>alert('Fill The Field First')</script>");
                return View("ResetCode");
            }
            else
            {
                var c = Session["ResetCode"];
                if (c.Equals(code))
                {
                    return View("NewPassword");
                }
                else
                {
                    Response.Write("<script>alert('Invalid Code')</script>");
                    return View("ResetCode");
                }
            }

        }
        public ActionResult UpdatePassword(string password)
        {
            if (password == "")
            {
                Response.Write("<script>alert('Fill The Field First')</script>");
                return View("NewPassword");
            }
            else
            {
                UMS_Ass5.Models.BAL bal = new UMS_Ass5.Models.BAL();
                var e = (String)Session["Email"];
                UMS_Ass5.Models.UserDTO u = bal.getUserByEmail(e);
                u.password = password;
                if (bal.UpdateUser(u))
                {
                    return View("Home", u);
                }
                else
                {
                    Response.Write("<script>alert('Password Not Reset')</script>");
                    return View("NewPassword");
                }
            }

        }
    }
}