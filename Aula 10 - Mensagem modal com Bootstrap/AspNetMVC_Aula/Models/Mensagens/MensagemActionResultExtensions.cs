/* 
 * Atributo para Extensao ActionResult gerando uma Mensagem em uma View 
 * 
 * Necessario Framework Bootstrap para exibição das Mensagens (Importante)
 * 
 * Chamada em seu Controller : 
 *   -- > View().Mensagem("Mensagem","Titulo");
 * 
 * Visite nossa página http://www.codigoexpresso.com.br
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


public static class ActionResultExtensions
{
    /// <summary>
    /// Redireciona para uma ActionResult retornando uma mensagem de confirmação para a View
    /// </summary>
    /// <param name="actionResult"></param>
    /// <param name="mensagem">Mensagem a ser exibida</param>
    /// <param name="titulo">titulo a ser exibido, sendo omitido apresenta defaut 'Atenção'</param>
    /// <returns></returns>
    public static ActionResult Mensagem(this ActionResult actionResult, string mensagem, string titulo = "Atenção")
    {
        return new TempDataActionResult(actionResult, mensagem, titulo);
    }
}

public class TempDataActionResult : ActionResult
{
    private readonly ActionResult _actionResult;
    private readonly string _mensagem;
    private readonly string _titulo;

    public TempDataActionResult(ActionResult actionResult, string Mensagem, string Titulo)
    {
        _actionResult = actionResult;
        _mensagem = Mensagem;
        _titulo = Titulo;
    }

    public override void ExecuteResult(ControllerContext context)
    {
        context.Controller.TempData["Mensagem"] = _mensagem;
        context.Controller.TempData["Titulo"] = _titulo;
        _actionResult.ExecuteResult(context);
    }
}