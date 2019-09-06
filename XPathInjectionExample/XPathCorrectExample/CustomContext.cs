using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace XPathCorrectExample
{
    class CustomContext : XsltContext
    {
        /// <summary>
        /// Lista com variáveis
        /// </summary>
        public XsltArgumentList ArgList { get; set; }

        public override bool Whitespace { get { return true; } }

        public CustomContext()
        {

        }

        public CustomContext(NameTable nt, XsltArgumentList args)
            : base(nt)
        {
            ArgList = args;
        }

        /// <summary>
        /// Função para resolver a função informada
        /// </summary>
        /// <param name="prefix">Prefixo</param>
        /// <param name="name">Nome da função</param>
        /// <param name="ArgTypes">Parâmetros da função</param>
        /// <returns></returns>
        public override IXsltContextFunction ResolveFunction(string prefix, string name, XPathResultType[] ArgTypes)
        {
            string strCase = name;

            switch (strCase)
            {
                case "ValidarUsuario":
                    return new XPathExtensionFunctions(2, 2, XPathResultType.Boolean, ArgTypes, "ValidarUsuario");
                case "ValidarSenha":
                    return new XPathExtensionFunctions(2, 2, XPathResultType.Boolean, ArgTypes, "ValidarSenha");
            }

            return null;
        }

        /// <summary>
        /// Função para resolver a varável informada pelo usuário, verificar se ela é válida
        /// </summary>
        /// <param name="prefix">Prefixo</param>
        /// <param name="name">Nome da variável</param>
        /// <returns></returns>
        public override IXsltContextVariable ResolveVariable(string prefix, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new XPathException(string.Format("Variável '{0}' não definida.", name));

            if (name.Equals("usuario") || name.Equals("senha"))
                return new XPathExtensionVariable(prefix, name);

            return null;
        }

        public override bool PreserveWhitespace(XPathNavigator node)
        {
            return false;
        }

        public override int CompareDocument(string baseUri, string nextbaseUri)
        {
            return 0;
        }
    }
}
