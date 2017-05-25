using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMVC_Aula.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sobre()
        {
            return View();
        }

        public ActionResult Menu()
        {
            return PartialView();
        }
        public ActionResult Confirma(int id)
        { 

            if (id==1)
            {
                return RedirectToAction("CadastroAluno", "CadastroAluno").Mensagem("Registro Apagado com Sucesso !!!");
            }
            else
            {
                return RedirectToAction("index", "Home").Mensagem("Erro ao apagar o registro !!!","Erro");
            }
        
        }
    }

    
}
