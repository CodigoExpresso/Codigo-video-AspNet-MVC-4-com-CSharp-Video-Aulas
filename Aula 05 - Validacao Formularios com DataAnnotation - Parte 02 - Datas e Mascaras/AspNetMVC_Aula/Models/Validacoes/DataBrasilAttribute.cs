/* 
 * Atributo DataBrasil
 * 
 * Data Annotations para Validar uma Data no formato dd/MM/yyyy.
 * 
 * Visite nossa página http://www.codigoexpresso.com.br
 * 
 * by Antonio Azevedo
 *  
 * Chamada em sua Classe : 
 *   [DataBrasil(ErrorMessage="Sua mensagem de erro",DataRequerida=true/false)]
 *    
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

public class DataBrasilAttribute : ValidationAttribute, IClientValidatable
{
    public Boolean DataRequerida { get; set; }
    public DataBrasilAttribute()
    {
        this.ErrorMessage = "Data inválida.";
        this.DataRequerida = false;
    }

    protected override ValidationResult IsValid(
        object value,
        ValidationContext validationContext)
    {
        // Verifica se valor é nulo
        if (value == null)
        {
            value = "";
        }

        // Se data não foi informada e não é requerida retorna sem validar 
        if (value == "" && DataRequerida == false)
        {
            return ValidationResult.Success; ;
        }

        // Tenta converter o valor para DateTime, caso não seja possível retorna com erro.
        try
        {
            DateTime new_value = Convert.ToDateTime(value.ToString());
        }
        catch
        {
            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }

        // Retorna com sucesso após a conversão;
        return ValidationResult.Success;
    }

    // Diretivas para validação do lado do Cliente, implementação com jquery.validate
    public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
        ModelMetadata metadata,
        ControllerContext context)
    {
        var Rule = new ModelClientValidationRule
        {
            ValidationType = "databrasil",
            ErrorMessage = this.FormatErrorMessage(metadata.PropertyName)

        };

        Rule.ValidationParameters["datarequerida"] = DataRequerida;
        yield return Rule;
    }
}