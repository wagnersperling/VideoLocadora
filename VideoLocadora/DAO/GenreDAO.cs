using System;
using System.Collections.Generic;
using RepositoryEntity;
using VideoLocadora.Models;

namespace VideoLocadora.DAO
{
    public class GenreDAO
    {
        //instancia do Entity
        static DBVideoLocadoraEntities db = new DBVideoLocadoraEntities();

        //método que busca generos no DB
        public static List<Genre> FindAll()
        {
            // recebe o conteúdo da tabela
            var genres = db.GENRETB;

            List<Genre> genreList = new List<Genre>();

            if (genres != null)
            {
                //converte para model
                foreach (var genre in genres)
                {
                    genreList.Add(new Genre
                    {
                        Id = genre.IDGENRE,
                        Name = genre.NAMEGENRE,
                        Active = (bool)genre.ACTIVO,
                        CreationDate = (DateTime)genre.CREATIONDATE
                    }
                        );
                }
            }

            return genreList;
        }

        // retorna um objeto consultado pelo Id
        public static Genre FindById(int Id)
        {
            var genreTb = db.GENRETB.Find(Id);

            var genre = new Genre
            {
                Id = genreTb.IDGENRE,
                Name = genreTb.NAMEGENRE,
                Active = (bool)genreTb.ACTIVO,
                CreationDate = (DateTime)genreTb.CREATIONDATE
            };

            return genre;
        }

        public static void Insert(Genre genre)
        {
            //Aqui é adicionado o novo genero no DB
            if (genre.Id == 0)
            {
                GENRETB genreTb = new GENRETB();
                genreTb.NAMEGENRE = genre.Name;
                genreTb.ACTIVO = genre.Active;
                genreTb.CREATIONDATE = genre.CreationDate;

                db.GENRETB.Add(genreTb);
            }

            db.SaveChanges();
        }

		public static void Edit(Genre genre)
		{
			//aqui atualiza o genero
			var genreTb = db.GENRETB.Find(genre.Id);

			// se não achar no bd, não vai fazer nada
			if (genreTb == null)
				return;
			
			genreTb.NAMEGENRE = genre.Name;
			genreTb.ACTIVO = genre.Active;
			genreTb.CREATIONDATE = genre.CreationDate;

			db.SaveChanges();
		}

		public static void Delete(int[] Id)
        {
            foreach (int i in Id)
            {
                //Deleta genero do DB
                var genre = db.GENRETB.Find(i);
                db.GENRETB.Remove(genre);
                db.SaveChanges();
            }
        }
    }
}