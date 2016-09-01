/* 
 * Atributo NumeroBrasil
 * 
 * Data Annotations para Validar um int ou double.
 * 
 * Visite nossa página http://www.codigoexpresso.com.br
 * 
 * by Antonio Azevedo
 *  
 * Chamada em sua Classe : 
 *   [NumeroBrasil(ErrorMessage="Sua mensagem de erro", DecimalRequerido=true/false, PontoMilharPermitido=true/false)]
 *    
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

public class NumeroBrasilAttribute : ValidationAttribute, IClientValidatable
{
    public Boolean DecimalRequerido { get; set; }
    public Boolean PontoMilharPermitido { get; set; }
    public NumeroBrasilAttribute()
    {
        this.ErrorMessage = "Valor inválido.";
        this.DecimalRequerido = false;
        this.PontoMilharPermitido = false;
    }

    protected override ValidationResult IsValid(
        object value,
        ValidationContext validationContext)
    {

        // Verifica se o Valor é nulo
        if (value == null)
        {
            value = "";
        }

        string newValue = value.ToString();

        // Verifica se Ponto milhar é permitido, se sim retira os pontos para validação
        if (PontoMilharPermitido)
        {
            newValue = newValue.Replace(".", string.Empty);
        }

        // Caso o valor informado seja nulo não é requerido retorna sem validar
        if (value.ToString() == "" && DecimalRequerido == false)
        {
            return ValidationResult.Success; ;
        }

        // Verifica se decimal é requerida para fazer a validacao como double ou com integer
        // Se a conversão não for possivel retor com erro padrão
        if (DecimalRequerido == true)
        {
            try
            {
                double x = Convert.ToDouble(newValue);
            }
            catch
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
        }
        else
        {
            try
            {
                Int32 x = Convert.ToInt32(newValue);
            }
            catch
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
        }

        // Retorna com sucesso caso a converão tenha sido feita
        return ValidationResult.Success;
    }

    // Diretivas para validação do lado do Cliente, implementa com jquery.validate
    public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
        ModelMetadata metadata,
        ControllerContext context)
    {
        var Rule = new ModelClientValidationRule
        {
            ValidationType = "numerobrasil",
            ErrorMessage = this.FormatErrorMessage(metadata.PropertyName)

        };

        string[] array = new string[] { DecimalRequerido.ToString(), PontoMilharPermitido.ToString() };

        Rule.ValidationParameters["params"] = string.Join(",", Array.ConvertAll(array, x => x.ToString()));

        yield return Rule;
    }
}