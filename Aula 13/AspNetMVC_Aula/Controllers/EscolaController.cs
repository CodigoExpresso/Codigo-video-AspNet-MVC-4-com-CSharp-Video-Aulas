using AspNetMVC_Aula.Models;
using AspNetMVC_Aula.Models.MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMVC_Aula.Controllers
{
    public class EscolaController : Controller
    {
        public ActionResult ListaAlunos()
        {

            string mErro = "";

            DAO dao = new DAO();

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
            DAO dao = new DAO();

            ModAluno aluno = new ModAluno();

            if(idAluno > 0)
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
            DAO dao = new DAO();

            if (ModelState.IsValid)
            {
                if(aluno.IdAluno==0)
                {
                    dao.Aluno_Ins(aluno);
                }
                else
                {
                    dao.Aluno_Upd(aluno);
                }

                return RedirectToAction("ListaAlunos", "Escola").Mensagem(dao.MensagemErroFormatada());
            }

            ViewBag.ListaCursos = dao.ListaCursos();

            return View(aluno);
        }

        public ActionResult ConfirmaApagaAluno(int Id = 0)
        {
            string mErro = "";

            if (Id > 0)
            {
                DAO dao = new DAO();
                dao.Aluno_Del(Id);
                mErro = dao.MensagemErroFormatada();
            }

            return RedirectToAction("ListaAlunos", "Escola").Mensagem(mErro);

        }

    }
}
