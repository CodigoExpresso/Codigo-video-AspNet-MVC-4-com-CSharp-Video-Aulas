using AspNetMVC_Aula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMVC_Aula.Controllers
{
    public class CadastroAlunoController : Controller
    {
        public ActionResult CadastroAluno()
        {
            ModAluno aluno = new ModAluno();
            return View(aluno);
        }

        [HttpPost]
        public ActionResult ExibeCadastroAluno(FormCollection form)
        {
            ViewBag.IdAluno = form["IdAluno"];
            ViewBag.Nome = form["Nome"];
            ViewBag.Email = form["Email"];

            return View();
        }
    }
}
