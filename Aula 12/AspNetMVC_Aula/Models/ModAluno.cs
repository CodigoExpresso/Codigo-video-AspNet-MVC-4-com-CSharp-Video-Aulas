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
        [Key]
        public int IdAluno { get; set; }

        [Display(Name = "Nome do Aluno")]
        [Required(ErrorMessage="Campo Nome deve ser preenchido.")]
        [MaxLength(100, ErrorMessage="Nome dever ter no maxímo 100 caracteres.")]
        public string Nome { get; set; }

        [Display(Name = "E-mail do Aluno")]
        [EmailBrasil(ErrorMessage="E-mail inválido.", EmailRequerido=false)]
        [MaxLength(100, ErrorMessage = "E-mail dever ter no maxímo 100 caracteres.")]
        public string Email { get; set; }

        [Display(Name="Data de Cadastro")]
        [DisplayFormat(DataFormatString= "{0:dd/MM/yyyy}", ApplyFormatInEditMode=true)]
        [DataBrasil(ErrorMessage="Data inválida.", DataRequerida=true)]
        public DateTime? DtCadastro { get; set; }

        [Display(Name="Valor do Curso")]
        [DisplayFormat(DataFormatString="{0:N}",ApplyFormatInEditMode=true)]
        [NumeroBrasil(ErrorMessage="Valor Inválido.", PontoMilharPermitido=true, DecimalRequerido=true)]
        public decimal? Valor { get; set; }

        [Display(Name = "Cursos")]
        public ModCurso Curso { get; set; }

        public ModAluno()
        {
            Curso = new ModCurso();
        }

    }
}