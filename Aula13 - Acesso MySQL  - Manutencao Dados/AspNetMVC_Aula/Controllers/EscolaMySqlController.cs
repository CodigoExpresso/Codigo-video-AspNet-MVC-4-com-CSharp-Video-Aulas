using AspNetMVC_Aula.Models;
using AspNetMVC_Aula.Models.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMVC_Aula.Controllers
{
    public class EscolaMySqlController : Controller
    {
        public ActionResult ListaAlunos()
        {

            string mErro = "";

            DAOMySql dao = new DAOMySql();

            List<ModAluno> laluno = dao.ListaAlunos();

            if (dao.isErro())
            {
                mErro = dao.MensagemErroFormatada();
            }

            if (mErro.Length > 0)
            {
                return View(laluno).Mensagem(mErro);
            }
            else
            {
                return View(laluno);
            }
        }

        public ActionResult CadastroAluno(int idAluno)
        {
            DAOMySql dao = new DAOMySql();

            ModAluno aluno = new ModAluno();

            if (idAluno > 0)
            {
                aluno = dao.getAluno(idAluno);
            }
            else
            {
                aluno.DtCadastro = DateTime.Now;
                aluno.Valor = 0;
            }

            ViewBag.ListaCursos = dao.ListaCursos();

            return View(aluno);
        }

        [HttpPost]
        public ActionResult CadastroAluno(ModAluno aluno)
        {
            DAOMySql dao = new DAOMySql();

            if (ModelState.IsValid)
            {
                if (aluno.IdAluno == 0)
                {
                    dao.Aluno_Ins(aluno);
                }
                else
                {
                    dao.Aluno_Upd(aluno);
                }

                return RedirectToAction("ListaAlunos", "EscolaMySql").Mensagem(dao.MensagemErroFormatada());
            }

            ViewBag.ListaCursos = dao.ListaCursos();

            return View(aluno);
        }

        public ActionResult ConfirmaApagaAluno(int Id = 0)
        {
            string mErro = "";

            if (Id > 0)
            {
                DAOMySql dao = new DAOMySql();
                dao.Aluno_Del(Id);
                mErro = dao.MensagemErroFormatada();
            }

            return RedirectToAction("ListaAlunos", "EscolaMySql").Mensagem(mErro);

        }

    }
}
