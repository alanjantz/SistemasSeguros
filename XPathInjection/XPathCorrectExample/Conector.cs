using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace XPathCorrectExample
{
    public static class Conector
    {
        public static bool IniciarConexao(string usuario, string senha)
        {
            XPathDocument xmldoc = new XPathDocument("../../../Users.xml");

            XPathNavigator nav = xmldoc.CreateNavigator();

            XsltArgumentList varList = new XsltArgumentList();

            varList.AddParam("usuario", string.Empty, usuario);
            varList.AddParam("senha", string.Empty, senha);

            // Cria uma instância de contexto passando os parâmetros       
            CustomContext context = new CustomContext(new NameTable(), varList);

            // Cria uma expressão XPath que valida o usuário que está tentando logar
            // Se não passa na primeira condição, não executa a segunda em diante
            XPathExpression xpath = XPathExpression.Compile("/Users/User[Extensions:ValidarUsuario(., $usuario) and Extensions:ValidarSenha(., $senha)]");

            xpath.SetContext(context);
            var iter = nav.SelectSingleNode(xpath);

            return iter != null;
        }
    }
}
