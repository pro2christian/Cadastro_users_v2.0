using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro_users_v2._0
{
    internal class Interface_G
    {
        public enum Resultado_e
        {
            Sucesso = 0,
            Sair = 1,
            Excecao = 2
        }
        public void ImprimeMensagens(string mensagem)
        {
            Console.WriteLine(mensagem);
        }
        public Resultado_e PegaString(ref string minhaString, string mensagemNoMenu)
        {
            string temp = "";
            Resultado_e retorno;
            do
            {
                ImprimeMensagens(mensagemNoMenu);
                temp = Console.ReadLine();
                if (temp == string.Empty)
                {
                    ImprimeMensagens("Nenhum nome foi digitado!!");
                    Console.ReadKey();
                }
            } while (string.IsNullOrEmpty(temp));
            if(temp == "s" ||  temp == "S")
                retorno = Resultado_e.Sair;
            else
            {
                minhaString = temp;
                retorno = Resultado_e.Sucesso;
            }
            return retorno;
        }
        public Resultado_e PegaData( ref DateTime data, string mensagemNoMenu)
        {
            Resultado_e retorno;
            do {
                try
                {
                    ImprimeMensagens(mensagemNoMenu);
                    string temp = Console.ReadLine();
                    if (temp == "s" || temp == "S")
                        retorno = Resultado_e.Sair;
                    else
                    {
                        data = Convert.ToDateTime(temp);
                        retorno = Resultado_e.Sucesso;
                    }
                }
                catch (Exception ex)
                {
                    ImprimeMensagens("Exceção: " + ex.Message);
                    ImprimeMensagens("Presione qualquer tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                    retorno = Resultado_e.Excecao;
                }
            } 
            while (retorno == Resultado_e.Excecao);
            Console.Clear();
            return retorno;
        }    
        public Resultado_e PegaUint32(ref uint numeroCasa, string mensagemNoMenu)
        {
            Resultado_e retorno;
            do
            {
                try
                {
                    ImprimeMensagens(mensagemNoMenu);
                    string temp = Console.ReadLine();
                    if (temp == "s" || temp == "S")

                        retorno = Resultado_e.Sair;
                    else
                    {
                        numeroCasa = Convert.ToUInt32(temp);
                        retorno = Resultado_e.Sucesso;
                    }
                }
                catch (Exception ex)
                {
                    ImprimeMensagens("Exeção: " + ex.Message);
                    ImprimeMensagens("Presione qualquer tecla para continuar");
                    Console.ReadKey();
                    Console.Clear();
                    retorno = Resultado_e.Excecao;
                }
            }
            while (retorno == Resultado_e.Excecao);
            Console.Clear();
            return retorno;
        }
        
        BaseDados baseDados;
        public Interface_G(BaseDados pBaseDados)
        {
            pBaseDados = baseDados;
        }
        public void ImprimeNoConsole(CadastroCliente pCliente)
        {
            ImprimeMensagens("Nome: "+ pCliente.Nome);
            ImprimeMensagens("Número do Documento: " + pCliente.NumeroDocumento);
            ImprimeMensagens("Data de Nascimento: "+ pCliente.DataNascimento.ToString("dd/MM/yyyy"));
            ImprimeMensagens("Nome da Rua: "+ pCliente.NomeRua);
            ImprimeMensagens("Numero da Casa: "+ pCliente.NumeroCasa);
        }
        public void ImprimeNoConsole(List<CadastroCliente> pListaCliente)
        {
            foreach(CadastroCliente cliente in pListaCliente)
            {
                ImprimeNoConsole(cliente);
            }
        }
        public Resultado_e CadastraCliente()
        {
            Console.Clear();
            string Nome = "";
            string NumeroDocumento = "";
            string NomeRua = "";
            uint NumeroCasa = 0;
            DateTime DataNascimento = new DateTime();

            if(PegaString( ref Nome, "Digite o nome completo ou S para sair") == Resultado_e.Sair)
                return Resultado_e.Sair;
            if (PegaData(ref DataNascimento, "Digite a data de nascimento  no formato DD/MM/AAAA ou S para sair") == Resultado_e.Sair)
                return Resultado_e.Sair;
            if (PegaString(ref NumeroDocumento, "Digite o nome completo ou S para sair") == Resultado_e.Sair)
                return Resultado_e.Sair;
            if (PegaString(ref NomeRua, "Digite o nome completo ou S para sair") == Resultado_e.Sair)
                return Resultado_e.Sair;
            if (PegaUint32(ref NumeroCasa, "Digite o número da casa ou S para sair") == Resultado_e.Sair)
                return Resultado_e.Sair;
            
            CadastroCliente cadastroCliente = new CadastroCliente(Nome, NumeroDocumento, NomeRua, NumeroCasa, DataNascimento);
            baseDados.AddCliente(cadastroCliente);
            return Resultado_e.Sucesso;

            Console.Clear();
            ImprimeMensagens("Cliente adicionado: ");
            ImprimeNoConsole(cadastroCliente);
            ImprimeMensagens("");
        }
        public void BuscaCliente()
        {
            Console.Clear();
            ImprimeMensagens("Digite o número do documento para buscar o cliente ou S para sair");
            string temp = Console.ReadLine();
            if (temp.ToLower() =="s")
                return;
            List<CadastroCliente> tempCadastroClientes = baseDados.BuscaCliente_Doc(temp);
            Console.Clear();
            if (tempCadastroClientes != null)
            {
                ImprimeMensagens("cliente encontrado(s): ".ToUpper() + tempCadastroClientes.Count + "\ndocumento: ".ToUpper() + temp);
            }
            else
                ImprimeMensagens("nenhum cliente com o document: ".ToUpper() + temp+ "foi encontrado".ToUpper());
        }
        public void ExcluiCliente()
        {
            Console.Clear();
            ImprimeMensagens("Digite o número do documento para excluir um cliente ou S para sair");
            string temp = Console.ReadLine();
            if (temp.ToLower() == "s")
                return;
            List<CadastroCliente> tempCadastroClientes = baseDados.ExcluiCliente_Doc(temp).Where(cliente => cliente.NumeroDocumento == temp).ToList();
            Console.Clear();
            if (tempCadastroClientes != null)
            {
                foreach(CadastroCliente clienteExcluir in tempCadastroClientes)
                {
                    ImprimeMensagens("cliente encontrado(s): ".ToUpper() + tempCadastroClientes.Count + "\ndocumento: ".ToUpper() + temp);
                    baseDados.ExcluiCliente_Doc(temp).Remove(clienteExcluir);
                }
            }
            else
                ImprimeMensagens("nenhum cliente com o document: ".ToUpper() + temp + "foi encontrado".ToUpper());
            ImprimeMensagens("");
        }
        public void Sair()
        {
            Console.Clear();
            ImprimeMensagens("Programa Encerrado...");
            Console.ReadKey();
        }
        public void opcaoDesconhecida()
        {
            Console.Clear();
            ImprimeMensagens("Opção desconhecida...");
            Console.ReadKey();
        }
        public void CarregaInterface()
        {
            string temp = "";
            do
            {
                ImprimeMensagens("Digite C para cadastrar um cliente");
                ImprimeMensagens("Digite B para buscar um cliente pelo número do documento");
                ImprimeMensagens("Digite E para excluir um cliente pelo número do documento");
                ImprimeMensagens("Digite S para sair");
                temp = Console.ReadKey(true).KeyChar.ToString().ToLower();
                switch (temp)
                {
                    case "c":
                        CadastraCliente();
                        break;
                    case "b":
                        BuscaCliente();
                        break;
                    case "e":
                        ExcluiCliente();
                        break;
                    case "s":
                        Sair();
                        break;
                    default:
                        opcaoDesconhecida();
                        break;
                }
            }
            while (temp !="s");
        }
    }
}
