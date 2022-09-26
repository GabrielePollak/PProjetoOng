using System;

namespace PProjetoOng
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("                                  <<<<<Bem-vindo a central  ADOÇÃO DE PET >>>>>\n <<<< BEM VINDO>>>>>                                 ");
                int op = Until.ColetarInt("Informe a operação que deseja realizar:\n1-Menu de pessoas adotantes\n2- Cadastrar nova adoção\n3-Menu Pets\n4-Registro de adoções\n0-Sair\nOpção: ");
                switch (op)
                {
                    case 0:
                        Console.WriteLine("Encerrando.");
                        Environment.Exit(0);
                        break;
                    case 1:
                        MenuAdotantes();
                        break;
                    case 2:
                        NovaAdocao();
                        break;
                    case 3:
                        MenuPet();
                        break;
                    case 4:
                        ListarRegAdocoes();
                        break;
                    default:
                        break;
                }
            } while (true);
        }

        static void MenuAdotantes()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("                                                <<<<<Menu Adotantes>>>>>                                       ");
                int op = Until.ColetarInt("Informe a opção desejada: \n1-Cadastrar novo(a) adotante \n2-Atualizar cadastro\n3-Excluir Cadastro\n4-Lista de cadastro de adotantes\n5-Buscar CPF\n0-Voltar\nOpção: ");
                switch (op)
                {
                    case 0:
                        return;
                    case 1:
                        Console.WriteLine("                                       <<<<CADASTRAR novo adotante >>>>                                       ");
                        Adotante pessoa = new Adotante();
                        pessoa.CadastrarAdotante();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("                                       <<<<<ATUALIZAR cadastro adotante >>>>>                                               ");
                        Adotante.EditarCadastroAdotante();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("                                      <<<<<<DELETAR Cadastro adotante >>>>>                                                 ");
                        Adotante.DeletarAdotante();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("                                    <<<<<LISTAR todos os adotantes cadastrados>>>>>");
                        BancoOng db = new BancoOng();
                        string sql = "Select cpf, nome, sexo, telefone, endereco, dataNascimento from pessoa where status = 'A'";
                        db.SelectTablePessoa(sql);
                        Until.Pause();
                        break;
                    case 5:
                        Console.Clear();
                        string cpf;
                        do cpf = Until.ColetarString("Informe o CPF para busca: ");
                        while (!Until.ValidarCpf(cpf));
                        BuscarCadastro(cpf);
                        Until.Pause();
                        break;
                    default:
                        break;
                }
            } while (true);
        }

        static void MenuPet()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("                                                      <<<<<Menu PETs>>>>>                                   ");
                int op = Until.ColetarInt("Informe a opção desejada\n1-Cadastrar Novo Pet para adoção)\n2-Editar Dados do Pet)\n3-Listar Pets disponiveis para adoção)\n0-Voltar\nOpção: ");
                switch (op)
                {
                    case 0:
                        return;
                    case 1:
                        Console.WriteLine("                                      <<<<<< CADASTRAR novo Pet para adoção >>>>                            ");
                        Pet pet = new Pet();
                        pet.CadastrarPet();
                        break;
                    case 2:
                        Console.WriteLine("                                      <<<< ATUALIZAR dados do Pet >>>                                     ");
                        Pet.EditarPet();
                        break;
                    case 3:
                        Console.WriteLine("                                     <<<<< LISTA de Pets disponiveis para a adoção:  >>>>                     ");
                        BancoOng db = new BancoOng();
                        string sql = "select nChipPet, familiaPet, racaPet, sexoPet, nomePet from dbo.pet where disponivel = 'A'";
                        db.SelectTablePet(sql);
                        Until.Pause();
                        break;
                    default:
                        break;
                }
            } while (true);
        }

        static void NovaAdocao()
        {
            int confirmacao;
            BancoOng db = new BancoOng();

            Console.Clear();
            Console.WriteLine("                                   <<<<<NOVA ADOÇÃO>>>>                        ");
            int pet = BuscarPet(db);
            if (pet == 0) return;
            string cpf = BuscarPessoa(db);
            if (cpf == "0") return;
            confirmacao = Until.ColetarInt("Deseja confirmar adoção?\n1- Sim\n2-Não\nOpção: ");
            if (confirmacao != 1) return;
            else ConfirmarAdocao(cpf, pet, db);
            Until.Pause();
        }

        static void ConfirmarAdocao(string cpf, int IDpet, BancoOng db)
        {
            string sql = $"insert into dbo.regAdocao (cpf, nChipPet, dataAdocao) values('{cpf}','{IDpet}', '{DateTime.Now}')";
            if (db.InsertRegAdocao(sql) != 0)
            {
                sql = $"update dbo.pet set disponivel = 'I' where nChipPet = {IDpet}";
                db.UpdateTable(sql);
                Console.WriteLine("Adoção efetuada com sucesso!!!");
            }
            else Console.WriteLine("Houve um problema na solicitação");
        }

        static int BuscarPet(BancoOng db)
        {
            int id, confirmacao;

            do
            {
                Console.Clear();
                Console.WriteLine("                   <<<<<   Seleção de Pets - Nova adoção >>>>>");
                string sqlPet = "select nChipPet, familiaPet, racaPet, sexoPet, nomePet from dbo.pet where disponivel = 'A'";
                if (!db.SelectTablePet(sqlPet)) return 0;
                id = Until.ColetarInt("Informe o numero do chip de identificação do Pet desejado informado na listagem acima: ");
                sqlPet = $"select nChipPet, familiaPet, racaPet, sexoPet, nomePet from dbo.pet where nChipPet = {id} and disponivel = 'A'";
                if (db.SelectTablePet(sqlPet))
                {
                    do
                    {
                        confirmacao = Until.ColetarInt("Confirmar seleção de Pet\n(0 - Cancelar)\n(1 - Sim)\nInforme opção: ");
                        if (confirmacao == 0) return 0;
                        else if (confirmacao == 1) return id;
                        else
                        {
                            Console.WriteLine("Opção Inválida...");
                            Until.Pause();
                        }
                    } while (true);
                }
            } while (true);
        }

        static String BuscarPessoa(BancoOng db)
        {
            do
            {
                int confirmacao;
                string cpf, sql;
                do cpf = Until.ColetarString("Informe o CPF do adotante que deseja encontrar: ");
                while (!Until.ValidarCpf(cpf));
                sql = $"Select cpf, nome, sexo, telefone, endereco, dataNascimento from pessoa where cpf ='{cpf}' and status = 'A';";
                if (!db.SelectTablePessoa(sql)) return "0";
                confirmacao = Until.ColetarInt("Confirmar adotante:\n1-Sim\n2-Não\nInforme opção:");
                if (confirmacao == 1) return cpf;
            } while (true);
        }

        static void ListarRegAdocoes()
        {
            BancoOng db = new BancoOng();
            do
            {
                Console.Clear();
                Console.WriteLine("                            <<<<< REGISTROS DE ADOÇÕES >>>>                             ");
                int op = Until.ColetarInt("\n1- Consultar registro geral de adoções\n2-Consultar pelo chip de identificação\n3-Consultar por CPF \n0-Sair\nInforme opção: ");
                switch (op)
                {
                    case 0:
                        return;
                    case 1:
                        Console.Clear();
                        Console.WriteLine("                  <<<<< CONSULTAR registro geral de adoções >>>>>");
                        string sql = "select ra.cpf, p.nome, ra.nChipPet, pt.familiaPet, pt.racaPet, ra.dataAdocao, pt.nomePet from pessoa p, pet pt, regAdocao ra where p.cpf = ra.cpf and pt.nChipPet = ra.nChipPet;";
                        db.SelectRegAdocao(sql);
                        Until.Pause();
                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("              <<<<< CONSULTA do registro de adoção pelo chip do PET >>>>            ");
                        int id = Until.ColetarInt("Informe o número do Chip do PET: ");
                        sql = $"select ra.cpf, p.nome, ra.nChipPet, pt.familiaPet, pt.racaPet, ra.dataAdocao, pt.nomePet from pessoa p, pet pt, regAdocao ra where ra.nChipPet = {id} and p.cpf = ra.cpf and pt.nChipPet = ra.nChipPet;";
                        db.SelectRegAdocao(sql);
                        Until.Pause();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("                 <<<<< CONSULTA A REGISTROS DE ADOÇÕES POR CPF >>>>>");
                        string cpf = Until.ColetarString("Informe o número do CPF do tutor adotante: ");
                        sql = $"select ra.cpf, p.nome, ra.nChipPet, pt.familiaPet, pt.racaPet, ra.dataAdocao, pt.nomePet from pessoa p, pet pt, regAdocao ra where ra.cpf = {cpf} and p.cpf = ra.cpf and pt.nChipPet = ra.nChipPet;";
                        db.SelectRegAdocao(sql);
                        Until.Pause();
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            } while (true);
        }

        static bool BuscarCadastro(string cpf)
        {

            string sql = $"select cpf, nome, sexo, telefone, endereco, dataNascimento, status from dbo.pessoa where cpf = '{cpf}';";
            BancoOng db = new BancoOng();
            if (!db.SelectTablePessoaInativa(sql)) return false;
            else return true;
        }

     
    
    }
}
