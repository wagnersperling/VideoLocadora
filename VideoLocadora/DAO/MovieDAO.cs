using System;
using System.Collections.Generic;
using VideoLocadora.Models;
using RepositoryDapper;

namespace VideoLocadora.DAO
{
    // essa classe usa Dapper
    public class MovieDAO
    {
        static QueryDapper queryDapper = new QueryDapper();

        //método que busca dados no DB
        public static List<Movie> FindAll()
        {
            string query = "SELECT * from MOVIETB";

            var movies = queryDapper.Query(query);

            List<Movie> movieList = new List<Movie>();

            if (movies != null)
            {
                //converte para model
                foreach (var movie in movies)
                {
                    movieList.Add(new Movie
                    {
                        Id = movie.IDMOVIE,
                        Name = movie.NAMEMOVIE,
                        Active = (bool)movie.ACTIVO,
                        CreationDate = (DateTime)movie.CREATIONDATE,
                        Genre = GenreDAO.FindById(movie.IDGENRE)
                    }
                        );
                }
            }

            return movieList;
        }

        //retorna um objeto 
        public static Movie FindById(int Id)
        {
            string query = String.Format("SELECT * FROM MOVIETB WHERE IDMOVIE = {0}", Id);

            var movie = queryDapper.QueryFirstOrDefault(query);

            return new Movie
            {
                Id = movie.IDMOVIE,
                Name = movie.NAMEMOVIE,
                Active = (bool)movie.ACTIVO,
                IdGenre = movie.IDGENRE,
                CreationDate = (DateTime)movie.CREATIONDATE
            };
        }

        public static void Insert(Movie movie)
        {
            if (movie.Id == 0)
            {
                string query = String.Format("INSERT INTO MOVIETB(NAMEMOVIE, ACTIVO,IDGENRE,CREATIONDATE) VALUES('{0}',{1},{2},'{3}')",
                    movie.Name, Convert.ToInt16(movie.Active), movie.IdGenre, movie.CreationDate.ToString("yyyy-MM-dd"));

                queryDapper.Query(query);
            }
            else
            {
                string query = String.Format("UPDATE MOVIETB SET NAMEMOVIE = '{0}', ACTIVO = {1}, IDGENRE = {2}, CREATIONDATE = '{3}'WHERE IDMOVIE = {4}",
                    movie.Name, Convert.ToInt16(movie.Active), movie.IdGenre, movie.CreationDate.ToString("yyyy-MM-dd"), movie.Id);

                var movies = queryDapper.Query(query);
            }
        }

        public static void Delete(int[] Id)
        {
            foreach (int i in Id)
            {
                string query = String.Format("DELETE FROM MOVIETB WHERE IDMOVIE = {0}", i);

                var movies = queryDapper.Query(query);
            }
        }
    }
}