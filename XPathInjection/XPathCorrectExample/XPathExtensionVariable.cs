using System.Xml.XPath;
using System.Xml.Xsl;

namespace XPathCorrectExample
{
    /// <summary>
    /// Interface responsável por recuperar os valores informados por parâmetro do usuário
    /// </summary>
    public class XPathExtensionVariable : IXsltContextVariable
    {
        // Namespace da variável
        private string Prefix { get; set; }
        // Nome da variável
        private string VarName { get; set; }

        public XPathExtensionVariable(string prefix, string varName)
        {
            this.Prefix = prefix;
            this.VarName = varName;
        }

        /// <summary>
        /// Recupera o valor da variável do usuário
        /// </summary>
        /// <param name="xsltContext">Contexto</param>
        /// <returns></returns>
        public object Evaluate(XsltContext xsltContext)
        {
            XsltArgumentList vars = ((CustomContext)xsltContext).ArgList;
            // O método GetParam é do contexto atual e é responsável por recuperar o valor da variável.
            return vars.GetParam(VarName, Prefix);
        }

        public bool IsLocal
        {
            get
            {
                return false;
            }
        }

        public bool IsParam
        {
            get
            {
                return false;
            }
        }

        public XPathResultType VariableType
        {
            get
            {
                return XPathResultType.Any;
            }
        }
    }
}
