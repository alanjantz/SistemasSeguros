using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace XPathCorrectExample
{
    public static class Conector
    {
        public static bool IniciarConexao(string usuario, string senha)
        {
            XPathDocument document = new XPathDocument("../../../Users.xml");

            XPathNavigator navigator = document.CreateNavigator();

            XsltArgumentList arguments = new XsltArgumentList();

            arguments.AddParam("usuario", string.Empty, usuario);
            arguments.AddParam("senha", string.Empty, senha);

            // Cria uma instância de contexto passando os parâmetros       
            CustomContext context = new CustomContext(new NameTable(), arguments);

            // Cria uma expressão XPath que valida o usuário que está tentando logar
            // Se não passa na primeira condição, não executa a segunda em diante
            XPathExpression expression = XPathExpression.Compile("/Users/User[Extensions:ValidarUsuario(., $usuario) and Extensions:ValidarSenha(., $senha)]");

            expression.SetContext(context);
            var node = navigator.SelectSingleNode(expression);

            return node != null;
        }
    }
}
