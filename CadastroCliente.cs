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
    internal class CadastroCliente
    {
        private string nome;
        private string numeroDocumento;
        private DateTime dataNascimento;
        private string nomeRua;
        private uint numeroCasa;
        [DataMember]
        public string Nome {  get; set; }
        [DataMember]
        public string NumeroDocumento { get; set; }
        [DataMember]
        public string NomeRua { get; set; }
        [DataMember]
        public uint NumeroCasa { get; set; }
         [DataMember]
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
