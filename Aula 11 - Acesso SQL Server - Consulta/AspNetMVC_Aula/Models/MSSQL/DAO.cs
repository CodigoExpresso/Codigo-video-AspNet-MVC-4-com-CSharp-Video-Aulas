using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AspNetMVC_Aula.Models.MSSQL
{
    public class DAO : Erros
    {
        public List<ModAluno> ListaAlunos()
        {
            // Instancia nossos objetos
            List<ModAluno> lalunos = new List<ModAluno>();
          
            // Instancia nossa Conexao
            Conexao conexao = new Conexao(TipoConexao.Conexao.WebConfig);

            // Se existe erro na conexao move o erro para a classe de acesso 
            if (conexao.ExisteErro())
            {
                setMensagemErro(conexao.mErro);
                return lalunos;
            }

            try
            {
                SqlDataReader reader;

                // Nosso comando SQL
                string query = "select a.idaluno, a.nome, a.dtcadastro, a.email, a.valor, c.idcurso, descricao_curso = c.descricao " +
                               "from alunos a " +
                               "inner join cursos c on a.idcurso = c.idcurso " +
                               "order by a.nome ";

                SqlCommand cmd = new SqlCommand(query, conexao.conn);

                // define que o comando é um texto
                cmd.CommandType = System.Data.CommandType.Text;

                // Abre nossa Conexao
                if (conexao.OpenConexao() == false)
                {
                    setMensagemErro(conexao.mErro);
                    return lalunos;
                }

                reader = cmd.ExecuteReader();

                // recebe os dados de nossa consulta
                while (reader.Read())
                {
                    lalunos.Add(read_Aluno(reader));
                }
            }
            catch (SqlException e)
            {
                // Trata os erros de nossa conexão
                setMensagemErro(e.Message.ToString());
            }

            // Fecha nossa Conexao
            conexao.CloseConexao();

            // Retorna nossa lista de dados
            return lalunos;
        }

        private ModCurso read_Curso(SqlDataReader reader)
        {
            ModCurso curso = new ModCurso();
            curso.idCurso = ConverteReader.ConverteInt(reader["idcurso"]);
            curso.Descricao = (string)reader["descricao_curso"] ?? "";

            return curso;
        }

        private ModAluno read_Aluno(SqlDataReader reader)
        {
            ModAluno aluno = new ModAluno();
            aluno.IdAluno = ConverteReader.ConverteInt(reader["idaluno"]);
            aluno.Nome = (string)reader["nome"] ?? "";
            aluno.Email = (string)reader["email"] ?? "";
            aluno.DtCadastro = ConverteReader.ConverteDateTime(reader["dtcadastro"]);
            aluno.Valor = ConverteReader.ConverteDecimal(reader["valor"]);

            aluno.Curso = read_Curso(reader);

            return aluno;
        }
    }
}