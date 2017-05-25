using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AspNetMVC_Aula.Models
{
    public class ModAluno
    {
        [Display(Name="Código do Aluno")]
     //   [Range(minimum:1, maximum:999999999, ErrorMessage="Código do Aluno de ser entre 1 e 999999999.")]
        [DisplayFormat(DataFormatString="{0:N0}", ApplyFormatInEditMode=true)]
        [NumeroBrasil(PontoMilharPermitido=true, DecimalRequerido=false)]
        public int IdAluno { get; set; }

        [Display(Name = "Nome do Aluno")]
        [Required(ErrorMessage="Campo Nome deve ser preenchido.")]
       // [StringLength(50, MinimumLength=4, ErrorMessage="Campo Nome deve ter entre 4 e 50 caracters.")]
        [Remote("ValidarNomes", "CadastroAluno", ErrorMessage = "Nome já Cadastrado.", AdditionalFields = "IdAluno")]
        public string Nome { get; set; }

        [Display(Name = "E-mail do Aluno")]
        //[RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "Formato do E-mail Inválido.")]
        [EmailBrasil(ErrorMessage="E-mail inválido.", EmailRequerido=false)]
        public string Email { get; set; }

        [Display(Name="Data de Cadastro")]
        [DisplayFormat(DataFormatString= "{0:dd/MM/yyyy}", ApplyFormatInEditMode=true)]
        [DataBrasil(ErrorMessage="Data inválida.", DataRequerida=false)]
        public DateTime? DtCadastro { get; set; }

        [Display(Name="Valor do Curso")]
        [Range(minimum:0.0,maximum:5000.00,ErrorMessage="Valor do Curso deve ser entre R$ 1,00 e R$ 5.000,00")]
        [DisplayFormat(DataFormatString="{0:N}",ApplyFormatInEditMode=true)]
        [NumeroBrasil(ErrorMessage="Valor Inválido.", PontoMilharPermitido=true, DecimalRequerido=true)]
        public double? ValorCurso { get; set; }

        [Display(Name="Senha")]
        [SenhaBrasil(SenhaTamanhoMinimo=6,SenhaTamanhoMaximo=8,SenhaForteRequerida=false,CaracterEspecialRequerido=false)]
        public string Senha { get; set; }

        [Display(Name="Confirme sua Senha")]
        [Compare("Senha",ErrorMessage="Senhas digitadas não conferem.")]
        public string ConfirmaSenha { get; set; }
    }
}