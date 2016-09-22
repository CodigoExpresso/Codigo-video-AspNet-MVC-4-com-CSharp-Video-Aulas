using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace AspNetMVC_Aula.Models.MySql
{
    public class DAOMySql : Erros
    {
        public List<ModAluno> ListaAlunos()
        {
            // Instancia nossos objetos
            List<ModAluno> lalunos = new List<ModAluno>();
          
            // Instancia nossa Conexao
            ConexaoMySql conexao = new ConexaoMySql(TipoConexao.Conexao.WebConfig);

            // Se existe erro na conexao move o erro para a classe de acesso 
            if (conexao.ExisteErro())
            {
                setMensagemErro(conexao.mErro);
                return lalunos;
            }

            try
            {
                MySqlDataReader reader;

                // Nosso comando SQL
                string query = "select a.idaluno, a.nome, a.dtcadastro, a.email, a.valor, c.idcurso, c.descricao as descricao_curso " +
                               "from alunos as a " +
                               "inner join cursos as c on a.idcurso = c.idcurso " +
                               "order by a.nome ";

                MySqlCommand cmd = new MySqlCommand(query, conexao.conn);

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
            catch (MySqlException e)
            {
                // Trata os erros de nossa conexão
                setMensagemErro(e.Message.ToString());
            }

            // Fecha nossa Conexao
            conexao.CloseConexao();

            // Retorna nossa lista de dados
            return lalunos;
        }


        public ModAluno getAluno(int idAluno)
        {

            ModAluno aluno = new ModAluno();

            // Instancia nossa Conexao
            ConexaoMySql conexao = new ConexaoMySql(TipoConexao.Conexao.WebConfig);

            // Se existe erro na conexao move o erro para a classe de acesso 
            if (conexao.ExisteErro())
            {
                setMensagemErro(conexao.mErro);
                return aluno;
            }

            try
            {
                MySqlDataReader reader;

                MySqlCommand cmd = new MySqlCommand("pcdAluno_Slc_ID", conexao.conn);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("idaluno", idAluno);

                // Abre nossa Conexao
                if (conexao.OpenConexao() == false)
                {
                    setMensagemErro(conexao.mErro);
                    return aluno;
                }

                reader = cmd.ExecuteReader();

                // recebe os dados de nossa consulta
                while (reader.Read())
                {
                    aluno = read_Aluno(reader);
                }
            }
            catch (MySqlException e)
            {
                // Trata os erros de nossa conexão
                setMensagemErro(e.Message.ToString());
            }

            // Fecha nossa Conexao
            conexao.CloseConexao();

            // Retorna nossa lista de dados
            return aluno;
        }

        public List<ModCurso> ListaCursos()
        {
            // Instancia nossos objetos
            List<ModCurso> lcursos = new List<ModCurso>();

            // Instancia nossa Conexao
            ConexaoMySql conexao = new ConexaoMySql(TipoConexao.Conexao.WebConfig);

            // Se existe erro na conexao move o erro para a classe de acesso 
            if (conexao.ExisteErro())
            {
                setMensagemErro(conexao.mErro);
                return lcursos;
            }

            try
            {
                MySqlDataReader reader;

                MySqlCommand cmd = new MySqlCommand("pcdCursos_Slc", conexao.conn);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
             

                // Abre nossa Conexao
                if (conexao.OpenConexao() == false)
                {
                    setMensagemErro(conexao.mErro);
                    return null;
                }

                reader = cmd.ExecuteReader();

                // recebe os dados de nossa consulta
                while (reader.Read())
                {                    
                    lcursos.Add(read_Curso(reader));
                }
            }
            catch (MySqlException e)
            {
                // Trata os erros de nossa conexão
                setMensagemErro(e.Message.ToString());
            }

            // Fecha nossa Conexao
            conexao.CloseConexao();

            // Retorna nossa lista de dados
            return lcursos;
        }


        public Int32 Aluno_Ins(ModAluno aluno)
        {
            Int32 _return = 0;
            // Instancia nossa Conexao
            ConexaoMySql conexao = new ConexaoMySql(TipoConexao.Conexao.WebConfig);

            // Se existe erro na conexao move o erro para a classe de acesso 
            if (conexao.ExisteErro())
            {
                setMensagemErro(conexao.mErro);
                return -1;
            }

            try
            {
                MySqlDataReader reader;

                MySqlCommand cmd = new MySqlCommand("pcdAluno_Ins", conexao.conn);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("idcurso", aluno.Curso.idCurso);
                cmd.Parameters.AddWithValue("nome", aluno.Nome ?? "");
                cmd.Parameters.AddWithValue("email", aluno.Email ?? "");
                cmd.Parameters.AddWithValue("valor", aluno.Valor);
                cmd.Parameters.AddWithValue("dtcadastro", aluno.DtCadastro);

                // Abre nossa Conexao
                if (conexao.OpenConexao() == false)
                {
                    setMensagemErro(conexao.mErro);
                    return -1;
                }

                reader = cmd.ExecuteReader();

                // recebe os dados de nossa consulta
                while (reader.Read())
                {
                    _return = Convert.ToInt32(reader["retorno"]);
                }
            }
            catch (MySqlException e)
            {
                // Trata os erros de nossa conexão
                setMensagemErro(e.Message.ToString());
                _return = -1;
            }

            // Fecha nossa Conexao
            conexao.CloseConexao();

            // Retorna nossa lista de dados
            return _return;
        }

        public Int32 Aluno_Upd(ModAluno aluno)
        {
            Int32 _return = 0;
            // Instancia nossa Conexao
            ConexaoMySql conexao = new ConexaoMySql(TipoConexao.Conexao.WebConfig);

            // Se existe erro na conexao move o erro para a classe de acesso 
            if (conexao.ExisteErro())
            {
                setMensagemErro(conexao.mErro);
                return -1;
            }

            try
            {

                MySqlCommand cmd = new MySqlCommand("pcdAluno_Upd", conexao.conn);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("idaluno", aluno.IdAluno);
                cmd.Parameters.AddWithValue("idcurso", aluno.Curso.idCurso);
                cmd.Parameters.AddWithValue("nome", aluno.Nome ?? "");
                cmd.Parameters.AddWithValue("email", aluno.Email ?? "");
                cmd.Parameters.AddWithValue("valor", aluno.Valor);
                cmd.Parameters.AddWithValue("dtcadastro", aluno.DtCadastro);

                // Abre nossa Conexao
                if (conexao.OpenConexao() == false)
                {
                    setMensagemErro(conexao.mErro);
                    return -1;
                }

                _return = cmd.ExecuteNonQuery();
              
            }
            catch (MySqlException e)
            {
                // Trata os erros de nossa conexão
                setMensagemErro(e.Message.ToString());
                _return = -1;
            }

            // Fecha nossa Conexao
            conexao.CloseConexao();

            // Retorna nossa lista de dados
            return _return;
        }

        public Int32 Aluno_Del(ModAluno aluno)
        {
            return Aluno_Del(aluno.IdAluno);
        }
        public Int32 Aluno_Del(int idAluno)
        {
            Int32 _return = 0;
            // Instancia nossa Conexao
            ConexaoMySql conexao = new ConexaoMySql(TipoConexao.Conexao.WebConfig);

            // Se existe erro na conexao move o erro para a classe de acesso 
            if (conexao.ExisteErro())
            {
                setMensagemErro(conexao.mErro);
                return -1;
            }

            try
            {

                MySqlCommand cmd = new MySqlCommand("pcdAluno_Del", conexao.conn);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("idaluno", idAluno);
               
                // Abre nossa Conexao
                if (conexao.OpenConexao() == false)
                {
                    setMensagemErro(conexao.mErro);
                    return -1;
                }

                _return = cmd.ExecuteNonQuery();

            }
            catch (MySqlException e)
            {
                // Trata os erros de nossa conexão
                setMensagemErro(e.Message.ToString());
                _return = -1;
            }

            // Fecha nossa Conexao
            conexao.CloseConexao();

            // Retorna nossa lista de dados
            return _return;
        }

        private ModCurso read_Curso(MySqlDataReader reader)
        {
            ModCurso curso = new ModCurso();
            curso.idCurso = ConverteReader.ConverteInt(reader["idcurso"]);
            curso.Descricao = (string)reader["descricao_curso"] ?? "";

            return curso;
        }

        private ModAluno read_Aluno(MySqlDataReader reader)
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