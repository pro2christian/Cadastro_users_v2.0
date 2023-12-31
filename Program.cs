using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro_users_v2._0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BaseDados baseDados = new BaseDados();
            Interface_G CadastraCliente = new Interface_G(baseDados);
            CadastraCliente.CarregaInterface();
        }
    }
}
