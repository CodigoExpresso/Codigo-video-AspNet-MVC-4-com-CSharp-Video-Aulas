using AspNetMVC_Aula.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        [IgnoreModelErrors("ConfirmaSenha")]
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

        public ActionResult ValidarNomes(string Nome, int IdAluno)
        {
            if (IdAluno==0)
            {
                return Json("Primeiro preencha o campo Código do Aluno.", JsonRequestBehavior.AllowGet);
            }

            if(Nome.Length < 4 || Nome.Length > 50)
            {
                return Json("Campo Nome deve ter entre 4 e 50 caracteres.", JsonRequestBehavior.AllowGet);
            }

            var NomesDB = new Collection<string>
            {
              "carlos",
              "humberto",
              "roberto",
              "marcelo"
            };

            if(NomesDB.IndexOf(Nome.ToLower())>=0)
            {
                return Json(string.Format("Nome '{0}' já esta cadastrado.", Nome), JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);

        }
        
    }
}
