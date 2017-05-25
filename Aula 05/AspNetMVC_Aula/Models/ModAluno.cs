using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AspNetMVC_Aula.Models
{
    public class ModAluno
    {
        [Display(Name="Código do Aluno")]
        [Range(minimum:1, maximum:50, ErrorMessage="Código do Aluno de ser entre 1 e 50.")]
        public int IdAluno { get; set; }

        [Display(Name = "Nome do Aluno")]
        [Required(ErrorMessage="Campo Nome deve ser preenchido.")]
        [StringLength(50, MinimumLength=4, ErrorMessage="Campo Nome deve ter entre 4 e 50 caracters.")]
        public string Nome { get; set; }

        [Display(Name = "E-mail do Aluno")]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "Formato do E-mail Inválido.")]
        public string Email { get; set; }

        [Display(Name="Data de Cadastro")]
        [DisplayFormat(DataFormatString= "{0:dd/MM/yyyy}", ApplyFormatInEditMode=true)]
        [DataBrasil(ErrorMessage="Data inválida.", DataRequerida=false)]
        public DateTime? DtCadastro { get; set; }
    }
}