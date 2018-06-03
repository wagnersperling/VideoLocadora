using System.Web.Mvc;
using VideoLocadora.Models;
using RepositoryEntity;
using VideoLocadora.DAO;

namespace VideoLocadora.Controllers
{
    public class LocationController : Controller
    {
        public ActionResult Index()
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login/Index");

            return View(LocationDAO.FindAll());
        }

        public ActionResult Insert()
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login/Index");

            var location = new Location();
            location.MovieList = MovieDAO.FindAll();

            return View(GetNewLocation());
        }

        public ActionResult InsertLocation(Location location, string creationDate, int[] IdMovies)
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login/Index");

            if (IdMovies == null)
            {
                ModelState.AddModelError("", "Nenhum filme foi selecionado para cadastrar");
            }
            if (ModelState.IsValid)
            {
                LocationDAO.Insert(location, creationDate, IdMovies);

                return View("Index", LocationDAO.FindAll());
            }

            return View("Insert", GetNewLocation());
        }

        public ActionResult Delete(int[] Id)
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login/Index");

            if (Id == null)
            {
                ModelState.AddModelError("", "Nenhum item foi selecionado para deletar");
            }

            if (ModelState.IsValid)
            {
                LocationDAO.Delete(Id);
            }

            return View("Index", LocationDAO.FindAll());
        }

        [HttpGet]
        public ActionResult Update(int Id)
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login/Index");

            //retorna para a view o objeto a ser atualizado
            return View("Insert", LocationDAO.FindById(Id));
        }

        //retorna uma nova instância de locação 
        private Location GetNewLocation()
        {
            var location = new Location();
            location.MovieList = MovieDAO.FindAll();
            return location;
        }

    }
}