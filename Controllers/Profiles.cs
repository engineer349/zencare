using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Zencareservice.Repository;
using Zencareservice.Models;
using System.Drawing;

namespace Zencareservice.Controllers
{
    public class Profiles : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Profile()
        {
            DataAccess Obj_DataAccess = new DataAccess();
            DataSet ds = new DataSet();
            ds = Obj_DataAccess.GetProfile();
            string fname = ds.Tables[0].Rows[0]["Fname"].ToString();
            string lname = ds.Tables[0].Rows[0]["Lname"].ToString();
            //DateTime dob = ds.Tables[0].Rows[2]["Dob"].ToDateTime();
            string phoneno = ds.Tables[0].Rows[0]["Phoneno"].ToString();
            string email = ds.Tables[0].Rows[0]["Email"].ToString();

            Personaldetails pers = new Personaldetails();
            {
                pers.Firstname = fname;
                pers.Lastname = lname;
                pers.Phoneno = phoneno;
                pers.Email = email;
            }

            return View(pers);
        }
        protected void Page_Load(object sender, EventArgs e,Personaldetails Obj)
        {
           

            try

            {


                string a = "ram";


               

            }

            catch (Exception ex)

            {

                throw ex;

            }

            
        }
    }
}
