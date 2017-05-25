/* 
 * Classe acesso MySql
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
using MySql.Data.MySqlClient;

public class ConexaoMySql
{

    public string mErro = "";


    // Variavel de definição para acesso a connectionStrings do Web.Config
    private string ConexaoWebConfig = "MySql";

    // Variaveis de configuração de acesso ao banco de dados
    private string Server = "localhost";
    private string Database = "escola";
    private string Usuario = "root";
    private string Senha = "123456";


    public MySqlConnection conn;

    public ConexaoMySql(TipoConexao.Conexao TConexao)
    {
        GetConexao(TConexao);
    }
    public ConexaoMySql()
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
                connectionStrings = string.Format("server={0} ;user id={2};pwd='{3}';database={1};Connect Timeout=28800;Command Timeout=2880", this.Server, this.Database, this.Usuario, this.Senha);
            }
            else
            {
                connectionStrings = getWebConfig(this.ConexaoWebConfig);
            }

            this.conn = new MySqlConnection(connectionStrings);
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

