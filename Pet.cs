using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PProjetoOng
{
    internal class Pet
    {
        
        public String NomePet { get; set; }
        public String EspeciePet { get; set; }
        public String Raca { get; set; }
        public Char SexoPet { get; set; }
        public Char PetDisponivel { get; set; }
        

        public Pet() { }

        public void CadastrarPet()
        {
            BancoOng db = new BancoOng();
            do
            {
                EspeciePet = Until.ColetarString("Informe o tipo de PET: ");
                if (EspeciePet.Length > 20) Console.WriteLine("Tamanho máximo da informação: 50 caracteres");
                else break;
            } while (true);

            do
            {
                Raca = Until.ColetarString("Informe a raça do pet: ");
                if (Raca.Length > 20) Console.WriteLine("Tamanho máximo da informação: 20 caracteres");
                else break;
            } while (true);

            SexoPet = Until.ColetarChar("Informe o sexo do PET (Opcional): ");
            Console.Write("Informe o nome do PET (Opcional): ");
            NomePet = Console.ReadLine();
            PetDisponivel = 'A';
            db.InsertTablePet(this);
        }

        public static void EditarPet()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("### EDITAR PET ###");
                int id = Until.ColetarInt("Informe o número do Chip do pet que será alterado: ");
                int op = Until.ColetarInt("Informe o que deseja alterar (1 - Tipo do Pet) (2 - Raça) (3 - Sexo) (4 - Nome): ");

                switch (op)
                {
                    case 0:
                        return;
                    case 1:
                        string tipoPet = Until.ColetarString("informe o tipo do pet: ");
                        BancoOng db = new BancoOng();
                        string sql = $"update dbo.pet set familiaPet = '{tipoPet}' where nChipPet = {id}";
                        if (db.UpdateTable(sql) != 0) Console.WriteLine("Cadastro efetuado com sucesso!!!");
                        else Console.WriteLine("Houve um erro na solicitação...");
                        Until.Pause();
                        return;

                    case 2:
                        string raca = Until.ColetarString("Informe a raça: ");
                        db = new BancoOng();
                        sql = $"update dbo.pet set racaPet = '{raca}' where nChipPet = {id};";
                        if (db.UpdateTable(sql) != 0) Console.WriteLine("Cadastro efetuado com sucesso!!!");
                        else Console.WriteLine("Houve um erro na solicitação...");
                        Until.Pause();
                        return;

                    case 3:
                        Char sexo;
                        do
                        {
                            sexo = Until.ColetarChar("Informe o sexo do pet: ");
                            if (sexo != 'M' && sexo != 'F')
                            {
                                sexo = ' ';
                                break;
                            }
                            else break;
                        } while (true);
                        db = new BancoOng();
                        sql = $"update dbo.pet set sexoPet = '{sexo}' where nChipPet = {id};";
                        if (db.UpdateTable(sql) != 0) Console.WriteLine("Cadastro efetuado com sucesso!!!");
                        else Console.WriteLine("Houve um erro na solicitação...");
                        Until.Pause();
                        return;

                    case 4:
                        db = new BancoOng();
                        string nome = Until.ColetarString("informe o nome do pet: ");
                        sql = $"update dbo.pet set nomePet = '{nome}' where nChipPet = {id};";
                        if (db.UpdateTable(sql) != 0) Console.WriteLine("Cadastro efetuado com sucesso!!!");
                        else Console.WriteLine("Houve um erro na solicitação...");
                        Until.Pause();
                        return;

                    default:
                        Console.WriteLine("Opção inválida");
                        break;
                }
                Until.Pause();
            } while (true);
        }

        public override string ToString()
        {
            return $"Tipo do PET: {EspeciePet}\nRaça: {Raca}\nSexo: {SexoPet}\nNome do Pet: {NomePet}".ToString();
        }
    }
}
