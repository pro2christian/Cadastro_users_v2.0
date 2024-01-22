using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Threading;

namespace Cadastro_users_v2._0
{
    [DataContract]
    internal class BaseDados
    {
        [DataMember]
        private List<CadastroCliente> cadastroCliente;
        private string caminhoDados;
        private Mutex mutexArquivo;
        private Mutex mutexLista;
        private bool baseDadosDisponivel;

        public void AddCliente(CadastroCliente pCliente)
        {
            mutexLista.WaitOne();
            cadastroCliente.Add(pCliente);
            mutexLista.ReleaseMutex();
            new Thread(() =>
            {
                baseDadosDisponivel = false;
                mutexArquivo.WaitOne();
                Serializador.Serializador_M(caminhoDados, this);
                mutexArquivo.ReleaseMutex();
                baseDadosDisponivel= true;
            }).Start();
        }
        public List<CadastroCliente> BuscaCliente_Doc( string pNumeroDocumento)
        {
            mutexLista.WaitOne();
            List<CadastroCliente> cadastroClientesTemp = cadastroCliente.Where(cliente => cliente.NumeroDocumento == pNumeroDocumento).ToList();
            mutexLista.ReleaseMutex();
            if (cadastroClientesTemp.Count >0)
                return cadastroClientesTemp;
            else
                return null; 
        }
        public List<CadastroCliente> ExcluiCliente_Doc( string pNumeroDocumento)
        {
            mutexLista.WaitOne();   
            List<CadastroCliente> cadastroClientesTemp = cadastroCliente.Where(cliente => cliente.NumeroDocumento == pNumeroDocumento).ToList();
            mutexLista.ReleaseMutex();
            if (cadastroClientesTemp.Count > 0)
            {
                foreach(CadastroCliente excluiCliente in cadastroClientesTemp)
                {
                    mutexLista.WaitOne();
                    cadastroCliente.Remove(excluiCliente);
                    mutexLista.ReleaseMutex();
                }
                new Thread(() =>
                {
                    baseDadosDisponivel = false;
                    mutexArquivo.WaitOne();
                    Serializador.Serializador_M(caminhoDados, this);
                    mutexArquivo.ReleaseMutex();
                    baseDadosDisponivel = true;
                }).Start();
                return cadastroClientesTemp;
            }
            else
                return null;
        }
        
        public bool BaseDisponivel()
        {
            return baseDadosDisponivel;
        }
        public BaseDados( string pCaminhoDados)
        {
            caminhoDados =pCaminhoDados;
            mutexLista = new Mutex();
            mutexArquivo = new Mutex();
            baseDadosDisponivel = true;
            new Thread(() =>
            {
                baseDadosDisponivel = false;
                mutexArquivo.WaitOne();
                BaseDados baseDadosTemp = Serializador.Desserializador(pCaminhoDados);
                mutexArquivo.ReleaseMutex();

                mutexLista.WaitOne();
                if (baseDadosTemp != null)
                    cadastroCliente = baseDadosTemp.cadastroCliente;
                else
                    cadastroCliente = new List<CadastroCliente>();
                mutexLista.ReleaseMutex();
                baseDadosDisponivel = true;
            }).Start();
        }
    }
}
