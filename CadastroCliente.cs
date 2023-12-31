using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro_users_v2._0
{
    internal class CadastroCliente
    {
        private string nome;
        private string numeroDocumento;
        private DateTime dataNascimento;
        private string nomeRua;
        private uint numeroCasa;

        public string Nome {  get; set; }
        public string NumeroDocumento { get; set; }
        public string NomeRua { get; set; }
        public uint NumeroCasa { get; set; }
        public DateTime DataNascimento { get; set; }
        
        public CadastroCliente (string pNome, string pNumeroDocumento, string pNomeRua , uint pNumeroCasa, DateTime pDataNascimento)
        {
            Nome = pNome;
            NumeroDocumento = pNumeroDocumento;
            NomeRua = pNomeRua;
            DataNascimento = pDataNascimento;
            NumeroCasa = pNumeroCasa;
        }
    }
}
