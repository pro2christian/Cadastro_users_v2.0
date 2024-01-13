using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;


namespace Cadastro_users_v2._0
{
    static class Serializador
    {
        static private DataContractSerializer serializadorContract = new DataContractSerializer(typeof(BaseDados));
        public static void Serializador_M( string pCaminhoArquivoXml, BaseDados pBaseDados)
        {
            XmlWriterSettings xmlConfig = new XmlWriterSettings { Indent = true };
            StringBuilder StringBuilder = new StringBuilder();
            XmlWriter escritorXml = XmlWriter.Create(StringBuilder, xmlConfig);
            serializadorContract.WriteObject(escritorXml, pBaseDados);
            escritorXml.Flush();
            string objSerializadoStr = StringBuilder.ToString();
            FileStream arquivoXml =  File.Create(pCaminhoArquivoXml);
            arquivoXml.Close();
            File.WriteAllText(pCaminhoArquivoXml, objSerializadoStr);
            escritorXml.Close();
        }
        public static BaseDados Desserializador(string pCaminhoArquivoXml)
        {
            try
            {
               if(File.Exists(pCaminhoArquivoXml))
                {
                    string conteudoObj = File.ReadAllText(pCaminhoArquivoXml);
                    StringReader leitorString = new StringReader(conteudoObj);
                    XmlReader leitorXml = XmlReader.Create(leitorString);
                    BaseDados baseDadosTemp;
                    baseDadosTemp = (BaseDados)serializadorContract.ReadObject(leitorXml);
                    return baseDadosTemp;
                }
               else
                    return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
