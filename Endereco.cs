using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PProjetoOng
{
    internal class Endereco
    {
        public String Rua { get; set; }
        public int Numero { get; set; }
        public String Bairro { get; set; }
        public String Complemento { get; set; }
        public String Cidade { get; set; }
        public String UF { get; set; }
        public String Cep { get; set; }

        public Endereco()
        {

        }

        public void CadastrarEndereco()
        {
            Rua = Until.ColetarString("Informe o nome da rua: ");
            Numero = Until.ColetarInt("Informe número da moradia: ");
            Bairro = Until.ColetarString("Informe o nome do bairro: ");
            Console.Write("Informe complento (Opcional): ");
            Complemento = Console.ReadLine();
            Cidade = Until.ColetarString("Informe o nome da cidade em que reside: ");
            UF = Until.ColetarString("Informe o nome do estado em que reside: ");
            do
            {
                Cep = Until.ColetarString("Informe o CEP [xxxxx-xxx]: ");
                if (Cep.Length != 9) Console.WriteLine("Cep inválido! Utilize o formato requisitado!");
                else break;
            } while (true);
        }
    }
}
