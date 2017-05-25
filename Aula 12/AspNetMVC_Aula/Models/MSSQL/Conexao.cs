/* 
 * Classe acesso MS SqlServer
 * 
 * Visite nossa página http://www.codigoexpresso.com.br
 * 
 * by Antonio Azevedo
 *  
 * Instanciando a Classe
 *    Conexao(TipoConexao.Conexao.WebConfig) -> Lendo string conexao do arquivo Web.Config
 *    Conexao(TipoConexao.Conexao.Classe)    -> Lendo dados da conexao de nossa classe
 *
 * Configurando nossa Classe
 *    ConexaoWebConfig - > Nome da connectionStrings de nosso arquivo Web.Config
 *    Server           - > Nome do servidor, se estiver usando conexao local utilize ´localhost´
 *    Database         - > Nome do Banco de Dados
 *    Usuario          - > Nome do usuário de Acesso ao Banco de Dados
 *    Senha            - > Senha de Acesso ao Banco de Dados
 * 
 */

using System;
using System.Data.SqlClient;

public class Conexao
{

    public string mErro = "";


    // Variavel de definição para acesso a connectionStrings do Web.Config
    private string ConexaoWebConfig = "SqlServer";

    // Variaveis de configuração de acesso ao banco de dados
    private string Server = "localhost";
    private string Database = "Escola";
    private string Usuario = "sa";
    private string Senha = "123456";


    public SqlConnection conn;

    public Conexao(TipoConexao.Conexao TConexao)
    {
        GetConexao(TConexao);
    }
    public Conexao()
    {
        GetConexao(TipoConexao.Conexao.Classe);
    }

    // Verifica se existe erro
    public Boolean ExisteErro()
    {
        if (mErro.Length > 0)
        {
            return true;
        }
        return false;
    }

    // Faz a Conexao com o Banco de Dados
    private void GetConexao(TipoConexao.Conexao TConexao)
    {
        try
        {
            string connectionStrings = "";
            if (TConexao == TipoConexao.Conexao.Classe)
            {
                connectionStrings = string.Format("Server={0},1433;Database={1};User ID={2};Password={3}", this.Server, this.Database, this.Usuario, this.Senha);
            }
            else
            {
                connectionStrings = getWebConfig(this.ConexaoWebConfig);
            }

            this.conn = new SqlConnection(connectionStrings);
        }
        catch (Exception erro)
        {
            this.mErro = erro.Message;
            this.conn = null;
        }
    }

    // Abre conexao com o Banco de Dados
    public Boolean OpenConexao()
    {
        Boolean _return = true;
        try
        {
            conn.Open();
        }
        catch (Exception erro)
        {
            this.mErro = erro.Message;
            _return = false;
        }

        return _return;
    }

    // Fecha conexao com o Banco de Dados
    public void CloseConexao()
    {
        conn.Close();
        conn.Dispose();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Variavel"></param>
    /// <returns></returns>
    public string getWebConfig(string Variavel)
    {
        string strValue = "";
        System.Configuration.Configuration rootWebConfig =
            System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/");
        System.Configuration.ConnectionStringSettings connString;
        if (0 < rootWebConfig.ConnectionStrings.ConnectionStrings.Count)
        {
            connString = rootWebConfig.ConnectionStrings.ConnectionStrings[Variavel];
            if (null != connString)
                strValue = connString.ConnectionString;
            else
                strValue = "erro";
        }

        return strValue;
    }
}

/// <summary>
/// Definição de tipos de Conexão 
/// </summary>
public class TipoConexao
{
    public enum Conexao { WebConfig = 1, Classe = 2 };
}