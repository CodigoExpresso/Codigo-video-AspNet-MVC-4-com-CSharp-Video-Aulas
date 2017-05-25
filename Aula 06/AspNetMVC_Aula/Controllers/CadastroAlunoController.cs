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
        public ActionResult ExibeCadastroAluno(ModAluno aluno)
        {
            if (aluno.IdAluno == 30)
            {
                ModelState.AddModelError("IdAluno", "Código do Aluno não pode ser igual a 30.");
            }

            //if(aluno.Nome==null || aluno.Nome.Trim().Length == 0) 
            //{
            //    ModelState.AddModelError("Nome", "Campo nome deve ser preenchido.");
            //}

            if (ModelState.IsValid == false)
            {
                return View("CadastroAluno", aluno);
            }

            return View(aluno);
        }
    }
}
