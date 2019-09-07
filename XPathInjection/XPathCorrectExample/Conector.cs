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

            XPathExpression expression = XPathExpression.Compile(
              "/Users/User[Extensions:ValidarUsuario(., $usuario) " +
              "and Extensions:ValidarSenha(., $senha)]");

            XsltArgumentList arguments = new XsltArgumentList();
            arguments.AddParam("usuario", string.Empty, usuario);
            arguments.AddParam("senha", string.Empty, senha);

            CustomContext context = new CustomContext(new NameTable(), arguments);

            expression.SetContext(context);
            var node = navigator.SelectSingleNode(expression);

            return node != null;
        }
    }
}
