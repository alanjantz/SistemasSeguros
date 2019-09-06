using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace XPathInjectionExample
{
    public static class Conector
    {
        public static bool IniciarConexao(string usuario, string senha)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../Users.xml");
            XmlNode root = doc.DocumentElement;

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("Users", "urn:users-schema");
             
            XmlNode node = root.SelectSingleNode(
                string.Format("/Users/User[UserName='{0}' and Password='{1}']", usuario, senha), nsmgr);
            
            return node != null;
        }
    }
}
