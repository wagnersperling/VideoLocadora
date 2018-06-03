using System.Collections.Generic;
using System.Web.Mvc;
using VideoLocadora.Models;
using VideoLocadora.DAO;

namespace VideoLocadora.Controllers
{

    public class GenreController : Controller
    {
        static List<Genre> genres = new List<Genre>();

        public ActionResult Index()
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login/Index");

            return View(GenreDAO.FindAll());
        }

        public ActionResult Insert()
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login/Index");

            return View(new Genre());
        }

		[HttpPost]
        public ActionResult Insert(Genre genre)
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login/Index");

			if (ModelState.IsValid)
			{
				//insere novo ou edita genero no DB
				if (genre.Id == 0)
					GenreDAO.Insert(genre);
				else
					GenreDAO.Edit(genre);

				return View("Index", GenreDAO.FindAll());
			}

			return View(genre);			
        }

        [HttpGet]
        public ActionResult Update(int Id)
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login/Index");

            //retorna para a view o objeto a ser atualizado
            return View("Insert", GenreDAO.FindById(Id));
        }

		[HttpPost]
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
                GenreDAO.Delete(Id);
            }

            return View("Index", GenreDAO.FindAll());
        }

    }
}
