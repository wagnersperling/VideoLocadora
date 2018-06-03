using System;
using System.Collections.Generic;
using System.Linq;
using RepositoryDapper;
using VideoLocadora.Models;

namespace VideoLocadora.DAO
{
    // essa classe usa Dapper
    public class LoginDAO
    {
        static QueryDapper queryDapper = new QueryDapper();

        //valida login
        public static bool ValidadeLogin(Login login)
        {
            string query = String.Format("SELECT * from LOGINTB WHERE LOGINTB.USERPASSWORD = '{0}' AND LOGINTB.USEREMAIL = '{1}'", login.Password, login.Email);

            var loginValid = queryDapper.Query(query);

            return loginValid.Any() ? true : false;
        }

        //insere novo login
        public static void Insert(string email, string password)
        {
            string query = String.Format("INSERT INTO LOGINTB(USEREMAIL, USERPASSWORD) VALUES('{0}', '{1}')", email, password);

            var loginValid = queryDapper.Query(query);
        }
    }
}