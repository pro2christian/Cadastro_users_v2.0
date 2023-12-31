using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro_users_v2._0
{
    internal class BaseDados
    {
       private List<CadastroCliente> cadastroCliente;

        public void AddCliente(CadastroCliente pCliente)
        {
            cadastroCliente.Add(pCliente);
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
        public BaseDados()
        {
            cadastroCliente = new List<CadastroCliente>();
        }
    }
}
