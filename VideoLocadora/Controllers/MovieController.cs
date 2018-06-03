using System.Web.Mvc;
using VideoLocadora.Models;
using VideoLocadora.DAO;

namespace VideoLocadora.Controllers
{
    public class MovieController : Controller
    {

        public ActionResult Index()
        {
            //se não tiver alguém logado retorna pra tela de login
            if (Session["username"] == null)
                return RedirectToAction("../Login/Index");

            return View(MovieDAO.FindAll());
        }

        public ActionResult Insert()
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login/Index");

            ViewBag.Genre = GenreDAO.FindAll();

            return View(new Movie());
        }

        [HttpGet]
        public ActionResult Update(int Id)
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login/Index");

            ViewBag.Genre = GenreDAO.FindAll();

            //retorna para a view o objeto a ser atualizado
            return View("Insert", MovieDAO.FindById(Id));
        }

        [HttpPost]
        public ActionResult Insert(Movie movie)
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login/Index");

            if (ModelState.IsValid)
            {
                //insere novo ou edita filme no DB
                MovieDAO.Insert(movie);

                return View("Index", MovieDAO.FindAll());
            }
            ViewBag.Genre = GenreDAO.FindAll();
            return View("Insert", movie);
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
                MovieDAO.Delete(Id);
            }

            return View("Index", MovieDAO.FindAll());
        }

    }
}
