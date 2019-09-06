using System.Xml;

namespace XPathInjectionExample
{
    public static class Conector
    {
        public static bool IniciarConexao(string usuario, string senha)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../Users.xml");
            XmlNode root = doc.DocumentElement;
            
            XmlNode node = root.SelectSingleNode(
                string.Format("/Users/User[UserName='{0}' and Password='{1}']", 
                              usuario, 
                              senha));
            
            return node != null;
        }
    }
}
