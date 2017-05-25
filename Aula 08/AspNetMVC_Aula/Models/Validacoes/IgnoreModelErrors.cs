using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

public class IgnoreModelErrorsAttribute : ActionFilterAttribute
{
    private string keysString;

    public IgnoreModelErrorsAttribute(string keys)
        : base()
    {
        this.keysString = keys;
    }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        ModelStateDictionary modelState = filterContext.Controller.ViewData.ModelState;
        string[] keyPatterns = keysString.Split(new char[] { ',' },
                 StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < keyPatterns.Length; i++)
        {
            string keyPattern = keyPatterns[i]
                .Trim()
                .Replace(@".", @"\.")
                .Replace(@"[", @"\[")
                .Replace(@"]", @"\]")
                .Replace(@"\[\]", @"\[[0-9]+\]")
                .Replace(@"*", @"[A-Za-z0-9]+");
            IEnumerable<string> matchingKeys = modelState.Keys.Where(x => Regex.IsMatch(x, keyPattern));
            foreach (string matchingKey in matchingKeys)
                modelState[matchingKey].Errors.Clear();
        }
    }
}