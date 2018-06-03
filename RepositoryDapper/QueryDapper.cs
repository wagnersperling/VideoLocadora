using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;

namespace RepositoryDapper
{
    //Classe que executa o sql
    public class QueryDapper
    {
        private const string stringconnection = @"Data Source=WAGNER\SQLEXPRESS;Initial Catalog=DBVideoLocadora;Integrated Security=True";

        public IEnumerable<dynamic> Query(string query)
        {
            var connection = new SqlConnection(stringconnection);
            connection.Open();
            var result = connection.Query(query);
            connection.Close();

            return result;
        }

        public dynamic QueryFirstOrDefault(string query)
        {
            var connection = new SqlConnection(stringconnection);
            connection.Open();
            var result = connection.QueryFirstOrDefault(query);
            connection.Close();

            return result;
        }

    }
}