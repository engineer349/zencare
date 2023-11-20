
using System.Data.SqlClient;
using System.Data;
using Zencareservice.Data;
using System.Xml.Linq;
using Zencareservice.Models;


namespace Zencareservice.Repository
{
    public class DataAccess
    {
        SqlDataAccess Obj_SqlDataAccess = new SqlDataAccess();

        public DataSet SaveRegister(Signup Obj)
        {
            
            try
            {
                DataSet ds=new DataSet();
                string StrSPName = "SaveRegister_SP";
                
                SqlParameter[] param = new SqlParameter[10];

                param[0] = new SqlParameter("@Firstname", SqlDbType.NVarChar);
                param[0].Value = Obj.Firstname;
                param[1] = new SqlParameter("@Lastname", SqlDbType.NVarChar);
                param[1].Value = Obj.Lastname;
                param[2] = new SqlParameter("@Email", SqlDbType.NVarChar);
                param[2].Value = Obj.Email;
                param[3] = new SqlParameter("@Password", SqlDbType.NVarChar);
                param[3].Value = Obj.Password;
                param[4] = new SqlParameter("@Confirmpassword", SqlDbType.NVarChar);
                param[4].Value = Obj.Confirmpassword;       
                param[5] = new SqlParameter("@Username",SqlDbType.NVarChar);
                param[5].Value = Obj.Username;
                param[6] = new SqlParameter("@Dob", SqlDbType.DateTime);
                param[6].Value = Obj.Dob;
                param[7] = new SqlParameter("@Phone", SqlDbType.VarChar);
                param[7].Value = Obj.Phonenumber;
                param[8] = new SqlParameter("@Status", SqlDbType.VarChar);
                param[8].Value = Obj.Status;
                param[9] = new SqlParameter("@Role", SqlDbType.VarChar);
                param[9].Value = Obj.Role;
                



                ds = Obj_SqlDataAccess.GetDataWithParamStoredprocedure(StrSPName, param);
                
                return ds;
            }

            catch (SqlException ex)
            {                
                throw ex;
            }
        }

        public DataSet SaveLogin(Login Obj)
        {

            try
            {
                DataSet ds = new DataSet();
                string StrSPName = "CheckLogin_SP";

                SqlParameter[] param = new SqlParameter[2];

                param[0] = new SqlParameter("@Uname", SqlDbType.NVarChar);
                param[0].Value = Obj.Username;
                param[1] = new SqlParameter("@Pass", SqlDbType.NVarChar);
                param[1].Value = Obj.Password;
                
                ds = Obj_SqlDataAccess.GetDataWithParamStoredprocedure(StrSPName, param);

                return ds;
            }

            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
