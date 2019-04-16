using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace UMS_Ass5.Models
{
    public class BAL
    {


        public bool isUserExist(string login, string nic, string email)
        {
            string connString = @"Data Source=.\SQLEXPRESS; Initial Catalog=Assignment4; User Id=sa; Password=12345;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "select * from dbo.Users where Login='" + login + "' OR NIC='" + nic + "' OR Email='" + email + "' ";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    return true;
                return false;
            }

        }

        public bool isAdminExist(string login, string pass)
        {
            try
            {
                string connString = @"Data Source=.\SQLEXPRESS; Initial Catalog=Assignment4; User Id=sa; Password=12345;";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "select * from dbo.Admin where Login='" + login + "' AND Password='" + pass + "'";
                    SqlCommand command = new SqlCommand(query, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
                return false;
        }

        public bool saveUser(UserDTO u)
        {
            try
            {
                string connstring = @"Data Source=.\SQLEXPRESS; Initial Catalog=Assignment4; User Id=sa; Password=12345";
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();
                    DateTime d = DateTime.Today.Date;
                    u.createdOn = d;
                    string query = String.Format(@"insert into dbo.Users (Name,Login,Password,Gender,Address,Age,NIC,DOB,Cricket,Hockey,Chess,ImageName,CreatedOn,Email) Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')", u.name, u.login, u.password, u.gender, u.address, u.age, u.nic, u.dob, u.cricket, u.hockey, u.chess, u.imageName, u.createdOn, u.email);
                    SqlCommand command = new SqlCommand(query, conn);
           
                    int rec = command.ExecuteNonQuery();
                    if (rec > 0)
                        return true;
                }
               
           }
           catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }


        public UserDTO getUserByLogin(string login)
        {
            UserDTO u = new UserDTO();
            string connstring = @"Data Source=.\SQLEXPRESS; Initial Catalog=Assignment4; User Id=sa; Password=12345";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                string query = "select * from dbo.Users where login='" + login + "'";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                u.login= login;
                u.name = reader.GetString(reader.GetOrdinal("Name"));
                u.password = reader.GetString(reader.GetOrdinal("Password"));
                u.gender = reader.GetString(reader.GetOrdinal("Gender"));
                u.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                u.address = reader.GetString(reader.GetOrdinal("Address"));
                u.age = reader.GetInt32(reader.GetOrdinal("Age"));
                u.nic = reader.GetString(reader.GetOrdinal("NIC"));
                u.dob = reader.GetDateTime(reader.GetOrdinal("DOB"));
                u.email = reader.GetString(reader.GetOrdinal("Email"));
                u.cricket = reader.GetBoolean(reader.GetOrdinal("Cricket"));
                u.hockey = reader.GetBoolean(reader.GetOrdinal("Hockey"));
                u.chess = reader.GetBoolean(reader.GetOrdinal("Chess"));
                u.imageName = reader.GetString(reader.GetOrdinal("ImageName"));
                u.createdOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn"));
            }
            return u;
        }
        public UserDTO getUserByID(int id)
        {
            UserDTO u = new UserDTO();
            string connstring = @"Data Source=.\SQLEXPRESS; Initial Catalog=Assignment4; User Id=sa; Password=12345";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                string query = "select * from dbo.Users where UserID='" + id + "'";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                u.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                u.login = reader.GetString(reader.GetOrdinal("Login"));
                u.name = reader.GetString(reader.GetOrdinal("Name"));
                u.password = reader.GetString(reader.GetOrdinal("Password"));
                u.gender = reader.GetString(reader.GetOrdinal("Gender"));
                u.UserID=reader.GetInt32(reader.GetOrdinal("UserID"));
                u.address = reader.GetString(reader.GetOrdinal("Address"));
                u.age = reader.GetInt32(reader.GetOrdinal("Age"));
                u.nic = reader.GetString(reader.GetOrdinal("NIC"));
                u.dob = reader.GetDateTime(reader.GetOrdinal("DOB"));
                u.email = reader.GetString(reader.GetOrdinal("Email"));
                u.cricket = reader.GetBoolean(reader.GetOrdinal("Cricket"));
                u.hockey = reader.GetBoolean(reader.GetOrdinal("Hockey"));
                u.chess = reader.GetBoolean(reader.GetOrdinal("Chess"));
                u.imageName = reader.GetString(reader.GetOrdinal("ImageName"));
                u.createdOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn"));
            }
            return u;
        }

        public bool UpdateUser(UserDTO u)
        {
            try
            {
                string connstring = @"Data Source=.\SQLEXPRESS; Initial Catalog=Assignment4; User Id=sa; Password=12345";
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();
                    DateTime d = DateTime.Today.Date;
                    u.createdOn = d;
                    string query = String.Format(@"Update dbo.Users set Name='{0}',Password='{1}',Gender='{2}',Address='{3}',Age='{4}',NIC='{5}',DOB='{6}',Cricket='{7}',Hockey='{8}',Chess='{9}',ImageName='{10}',CreatedOn='{11}',Email='{12}',Login='{13}' where UserID='" + u.UserID + "'", u.name, u.password, u.gender, u.address, u.age, u.nic, u.dob, u.cricket, u.hockey, u.chess, u.imageName, u.createdOn, u.email, u.login);
                    SqlCommand command = new SqlCommand(query, conn);

                    int rec = command.ExecuteNonQuery();
                    if (rec > 0)
                        return true;
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
            return false;
        }

        public UserDTO getUserByEmail(string email)
        {
           
                UserDTO u = new UserDTO();
                string connstring = @"Data Source=.\SQLEXPRESS; Initial Catalog=Assignment4; User Id=sa; Password=12345";
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();
                    string query = "select * from dbo.Users where Email='" + email + "'";
                    SqlCommand command = new SqlCommand(query, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    u.login = reader.GetString(reader.GetOrdinal("Login"));
                    u.name = reader.GetString(reader.GetOrdinal("Name"));
                    u.password = reader.GetString(reader.GetOrdinal("Password"));
                    u.UserID= reader.GetInt32(reader.GetOrdinal("UserID"));
                    u.gender = reader.GetString(reader.GetOrdinal("Gender"));
                    u.address = reader.GetString(reader.GetOrdinal("Address"));
                    u.age = reader.GetInt32(reader.GetOrdinal("Age"));
                    u.nic = reader.GetString(reader.GetOrdinal("NIC"));
                    u.dob = reader.GetDateTime(reader.GetOrdinal("DOB"));
                    u.email = email;
                    u.cricket = reader.GetBoolean(reader.GetOrdinal("Cricket")); ;
                    u.hockey = reader.GetBoolean(reader.GetOrdinal("Hockey")); ;
                    u.chess = reader.GetBoolean(reader.GetOrdinal("Chess")); ;
                    u.imageName = reader.GetString(reader.GetOrdinal("ImageName"));
                    u.createdOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn"));
                }
                return u;
           
           
        }


        public bool sendEmail(String toEmailAddress, String subject, String body)
        {
            try
            {

                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

                MailAddress to = new MailAddress(toEmailAddress);
                mail.To.Add(to);

                MailAddress from = new MailAddress("eadsef15morning@gmail.com", "Admin");
                mail.From = from;

                mail.Subject = subject;
                mail.Body = body;

                var sc = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new System.Net.NetworkCredential("eadsef15morning@gmail.com", "EAD_sef15"),
                    EnableSsl = true
                };

                sc.Send(mail);
                return true;
      
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public List<UserDTO> getUsersList()
        {
            string connString = @"Data Source=.\SQLEXPRESS; Initial Catalog=Assignment4; User Id=sa; Password=12345";
            using (SqlConnection con = new SqlConnection(connString))
            {
                try
                {
                    con.Open();
                    List<UserDTO> userList = new List<UserDTO>();
                    string query = "Select * from dbo.Users";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UserDTO u = new UserDTO();
                        u.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                        u.login = reader.GetString(reader.GetOrdinal("Login"));
                        u.name = reader.GetString(reader.GetOrdinal("Name"));
                        u.address = reader.GetString(reader.GetOrdinal("Address"));
                        u.age = reader.GetInt32(reader.GetOrdinal("Age"));
                        u.password = reader.GetString(reader.GetOrdinal("Password"));

                        u.gender = reader.GetString(reader.GetOrdinal("Gender"));
                        u.dob = reader.GetDateTime(reader.GetOrdinal("DOB"));
                        u.email = reader.GetString(reader.GetOrdinal("Email"));
                        u.cricket = reader.GetBoolean(reader.GetOrdinal("Cricket")); ;
                        u.hockey = reader.GetBoolean(reader.GetOrdinal("Hockey")); ;
                        u.chess = reader.GetBoolean(reader.GetOrdinal("Chess")); ;
                        u.imageName = reader.GetString(reader.GetOrdinal("ImageName"));
                        u.createdOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn"));
                        userList.Add(u);
                    }
                    return userList;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

    }
}