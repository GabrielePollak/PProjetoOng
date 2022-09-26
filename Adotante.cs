using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PProjetoOng
{
    internal class Adotante
    {
        public String Nome { get; set; }
        public String Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public Char Sexo { get; set; }
        public String Telefone { get; set; }
        public String End { get; set; }
        public Char Status { get; set; }


        public void CadastrarAdotante()
        {
            BancoOng db = new BancoOng();
            do
            {
                Nome = Until.ColetarString("Informe nome do adotante: ");
                if (Nome.Length > 50) Console.WriteLine("Informe um nome menor de 50 caracteres!");
                else break;
            } while (true);
            do Cpf = Until.ColetarString("Informe CPF do adotante: ").Replace("-", "").Replace(".", "");
            while (!Until.ValidarCpf(Cpf));
            do
            {
                DataNascimento = Until.ColetarData("Informe a data de nascimento do adotante: ");
                if (DataNascimento > DateTime.Now) Console.WriteLine("Informe uma data válida");
                else break;
            } while (true);
            do
            {
                Sexo = Until.ColetarChar("Informe o sexo que consta no documento do adotante: (M  - Masculino) ou (F - Feminino): ");
                if (Sexo != 'M' && Sexo != 'F') Console.WriteLine("Informe M ou F!");
                else break;
            } while (true);

            End = Until.ColetarString("Informe o endereço completo do adotante: ");
            Telefone = Until.ColetarString("Informe o DDD + número do telefone: ").Replace("(", "").Replace("-", "").Replace(")", "");
            Status = 'A';
            db.InsertTablePessoa(this);
        }

        public static void EditarCadastroAdotante()
        {
            string cpf = Console.ReadLine();

            BancoOng db = new BancoOng();
            do
            {
                Console.Write("Informe o CPF da adotante para fazer uma atualização: ");
                cpf = Console.ReadLine().Replace("-", "").Replace(".", "");
            } while (!Until.ValidarCpf(cpf));
            int op = Until.ColetarInt("Informe o campo que deseja atualizar: \n1-Nome\n2-Endereco\n3-Telefone\n0-Cancelar: ");
            switch (op)
            {
                
                case 1:
                    string nome = Until.ColetarString("Informe o nome corrigido: ");
                    string sql = $"update dbo.pessoa set nome='{nome}' where cpf='{cpf}';";
                    db.UpdateTable(sql);
                    return;
                case 2:
                    string end = Until.ColetarString("Informe o endereço corrigido: ");
                    sql = $"update dbo.pessoa set endereco = '{end}' where cpf = '{cpf}'";
                    db.UpdateTable(sql);
                    return;
                case 3:
                    string telefone = Until.ColetarString("Informe o telefone corrigido: ").Replace("(", "").Replace("-", "").Replace(")", "");
                    sql = $"update dbo.pessoa set telefone = '{telefone}' where cpf = '{cpf}'";
                    db.UpdateTable(sql);
                    return;
                default:
                    Console.WriteLine("Opção inválida!!!");
                    break;
                case 0:
                    Console.WriteLine("Operação cancelada!!!");
                    Until.Pause();
                    return;
            }
        }

        public static void DeletarAdotante()
        {
            BancoOng db = new BancoOng();
            String cpf;
            do cpf = Until.ColetarString("Informe o CPF do adotante terá seu cadastro excluido: ").Replace("-", "").Replace(".", "");
            while (!Until.ValidarCpf(cpf));
            string sql = $"Select cpf, nome, sexo, telefone, endereco, dataNascimento from pessoa where status = 'A' and cpf = {cpf}";
            if (!db.SelectTablePessoa(sql)) return;
            else
            {
                int op = Until.ColetarInt("Deseja confirmar a inativação do cadastro?\n(1 - Sim)\n(2 - Não)\nInforme opção: ");
                if (op == 1)
                {
                    sql = $"update dbo.adotante set status = 'I' where cpf = '{cpf}';";
                    if (db.UpdateTable(sql) != 0) Console.WriteLine("Inativação do cadastro efetuado com sucesso!!!");
                    else Console.WriteLine("Erro na solicitação");
                }
            }
            Until.Pause();
        }

        public override string ToString()
        {
            return $"Nome: {Nome}\nCPF: {Cpf.Substring(0, 3)}.{Cpf.Substring(3, 3)}.{Cpf.Substring(6, 3)}-{Cpf.Substring(9, 2)}\n" +
                $"Sexo: {Sexo}\nTelefone: ({Telefone.Substring(0, 2)}){Telefone.Substring(2, 5)}-{Telefone.Substring(7, 4)}\n" +
                $"Endereço: {End}\nData de nascimento: {DataNascimento.ToShortDateString()}".ToString();
        }
    }
}
