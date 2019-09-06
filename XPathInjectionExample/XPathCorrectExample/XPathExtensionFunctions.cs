using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace XPathCorrectExample
{
    public class XPathExtensionFunctions : IXsltContextFunction
    {
        public XPathExtensionFunctions(int minArgs, int maxArgs,
            XPathResultType returnType, XPathResultType[] argTypes, string functionName)
        {
            this.Minargs = minArgs;
            this.Maxargs = maxArgs;
            this.ReturnType = returnType;
            this.ArgTypes = argTypes;
            this.FunctionName = functionName;
        }

        // Parâmetros
        public XPathResultType[] ArgTypes { get; private set; }

        // Número máximo de parâmetros para a função
        public int Maxargs { get; private set; }

        // Numéro mínimo de parâmetros para a função
        public int Minargs { get; private set; }

        // Tipo de retorno da função
        public XPathResultType ReturnType { get; private set; }
        
        // Nome da função a ser executada
        public string FunctionName { get; private set; }

        private bool ValidarUsuario(XPathNodeIterator node, string usuario)
        {
            return node.Current.SelectSingleNode("UserName").Value == usuario;
        }

        private bool ValidarSenha(XPathNodeIterator node, string senha)
        {
            return node.Current.SelectSingleNode("Password").Value == senha;
        }

        /// <summary>
        /// Rotina que executa a função em tempo de execução
        /// </summary>
        /// <param name="xsltContext">Contexto</param>
        /// <param name="args">Parâmetros</param>
        /// <param name="docContext"></param>
        /// <returns></returns>
        public object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
        {
            if (FunctionName == "ValidarUsuario")
                return ValidarUsuario((XPathNodeIterator)args[0], args[1].ToString());
            else if (FunctionName == "ValidarSenha")
                return ValidarSenha((XPathNodeIterator)args[0], args[1].ToString());

            return null;
        }
    }
}
