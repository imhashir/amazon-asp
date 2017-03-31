using System;
using System.Data.SqlClient;
using System.Data;
using System.IO;
namespace myAmazon_v1.DAL
{
    public class SignUpDAL
    {
        public int signUpUser(string fName, string lName, string email, string number, string img, string username, string pwd, ref string log)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager
                        .ConnectionStrings["myAmazonConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("SignUp", conn);

            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@username", username);
            sqlCmd.Parameters.AddWithValue("@firstName", fName);
            sqlCmd.Parameters.AddWithValue("@lastName", lName);
            sqlCmd.Parameters.AddWithValue("@email", email);
            sqlCmd.Parameters.AddWithValue("@pass", pwd);
            sqlCmd.Parameters.AddWithValue("@number", number);
            sqlCmd.Parameters.AddWithValue("@img", img ?? (object)DBNull.Value);

            SqlParameter outputFlag = sqlCmd.Parameters.Add("@flag", SqlDbType.Int);
            outputFlag.Direction = ParameterDirection.Output;
            int flag = 0;
            try
            {
                conn.Open();
                sqlCmd.ExecuteNonQuery();
                flag = (int)sqlCmd.Parameters["@flag"].Value;
                if (flag != 0)
                    throw new Exception();
                log += "SignUp Successful!";
            }
            catch (Exception ex)
            {
                if (flag != 0)
                {
                    switch (flag)
                    {
                        case 1:
                            log += "Username already exists";
                            break;
                        case 2:
                            log += "Invalid Password";
                            break;
                        case 3:
                            log += "Email already registered!";
                            break;
                    }
                }
                else
                {
                    log += ex.ToString();
                }
            }
            finally
            {
                conn.Close();
            }
            return flag;
        }
    }
}