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

            return View(laluno).Mensagem(mErro);
        }

     

    }
}
