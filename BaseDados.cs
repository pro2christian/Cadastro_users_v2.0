using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;

namespace Cadastro_users_v2._0
{
    [DataContract]
    internal class BaseDados
    {
        [DataMember]
        private List<CadastroCliente> cadastroCliente;
        private string caminhoDados;

        public void AddCliente(CadastroCliente pCliente)
        {
            cadastroCliente.Add(pCliente);
            Serializador.Serializador_M(caminhoDados, this);
        }
        public List<CadastroCliente> BuscaCliente_Doc( string pNumeroDocumento)
        {
            List<CadastroCliente> tempCadastroClientes = cadastroCliente.Where(cliente => cliente.NumeroDocumento == pNumeroDocumento).ToList();
            if (tempCadastroClientes.Count >0)
                return tempCadastroClientes;
            else
                return null; 
        }
        public List<CadastroCliente> ExcluiCliente_Doc( string pNumeroDocumento)
        {
            List<CadastroCliente> tempCadastroClientes = cadastroCliente.Where(cliente => cliente.NumeroDocumento == pNumeroDocumento).ToList();
            if (tempCadastroClientes.Count > 0)
            {
                foreach(CadastroCliente excluiCliente in tempCadastroClientes)
                {
                    cadastroCliente.Remove(excluiCliente);
                }
                return tempCadastroClientes;
            }
            else
                return null;
        }
        
        public BaseDados( string pCaminhoDados)
        {
            caminhoDados =pCaminhoDados;
            BaseDados baseDadosTemp = Serializador.Desserializador(pCaminhoDados);
            if (baseDadosTemp != null)
                cadastroCliente = baseDadosTemp.cadastroCliente;
            else
                cadastroCliente = new List<CadastroCliente>();
        }
    }
}
