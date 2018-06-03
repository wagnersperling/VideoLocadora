using System;
using System.Collections.Generic;
using System.Linq;
using RepositoryEntity;
using VideoLocadora.Models;

namespace VideoLocadora.DAO
{
    public class LocationDAO
    {
        //instancia do Entity
        static DBVideoLocadoraEntities db = new DBVideoLocadoraEntities();

        public static void Delete(int[] Id)
        {
            foreach (int i in Id)
            {
                //Deleta o locação do DB
                db.MOVIEXLOCATION.RemoveRange(db.MOVIEXLOCATION.Where(x => x.IDLOCATION == i));
                db.SaveChanges();

                var location = db.LOCATIONTB.Find(i);
                db.LOCATIONTB.Remove(location);
                db.SaveChanges();
            }
        }

        public static Location FindById(int Id)
        {
            var locationTb = db.LOCATIONTB.Find(Id);

            var location = new Location
            {
                Id = locationTb.IDLOCATION,
                CPF = locationTb.CPF,
                CreationDate = (DateTime)locationTb.CREATIONDATE
            };

            location.MovieList = MovieDAO.FindAll();

            return location;
        }

        public static List<Location> FindAll()
        {
            // recebe o conteúdo da tabela
            var locationListTb = db.LOCATIONTB;

            List<Location> locationList = new List<Location>();

            if (locationListTb != null)
            {
                //converte para model
                foreach (var location in locationListTb)
                {
                    locationList.Add(new Location
                    {
                        Id = location.IDLOCATION,
                        CPF = location.CPF,
                        CreationDate = (DateTime)location.CREATIONDATE,
                        MovieList = MovieDAO.FindAll()
                    }
                        );
                }
            }

            return locationList;
        }

        public static void Insert(Location location, string creationDate, int[] IdMovies)
        {
            if (location.Id == 0)
            {
                //adiciona locação no DB
                LOCATIONTB locationTb = new LOCATIONTB();
                locationTb.CPF = location.CPF;
                locationTb.CREATIONDATE = DateTime.Parse(creationDate);
                db.LOCATIONTB.Add(locationTb);
                db.SaveChanges();

                //busca o último Id da tabela LOCATIONTB
                int lastId = db.LOCATIONTB.Max(x => x.IDLOCATION);

                //grava dados na tabele MOVIEXLOCATION
                foreach (int id in IdMovies)
                {
                    MOVIEXLOCATION movieXlocation = new MOVIEXLOCATION();
                    movieXlocation.IDLOCATION = lastId;
                    movieXlocation.IDMOVIE = id;
                    db.MOVIEXLOCATION.Add(movieXlocation);
                    db.SaveChanges();
                }
            }
            else
            {
                db.MOVIEXLOCATION.RemoveRange(db.MOVIEXLOCATION.Where(x => x.IDLOCATION == location.Id));
                db.SaveChanges();
                //aqui atualiza o locação
                var locationTb = db.LOCATIONTB.Find(location.Id);
                locationTb.CPF = location.CPF;
                locationTb.CREATIONDATE = DateTime.Parse(creationDate);
                db.SaveChanges();

                foreach (int id in IdMovies)
                {
                    MOVIEXLOCATION movieXlocation = new MOVIEXLOCATION();
                    movieXlocation.IDLOCATION = location.Id;
                    movieXlocation.IDMOVIE = id;
                    db.MOVIEXLOCATION.Add(movieXlocation);
                    db.SaveChanges();
                }
            }
        }
    }
}