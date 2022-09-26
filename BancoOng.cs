using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32.SafeHandles;

namespace PProjetoOng
{
    internal class BancoOng
    {

        string conexao = "Data Source=localhost; Initial Catalog=BancoOng, User id = sa; Password = scoobypolly;";
        SqlConnection conn;

        public BancoOng()
        {
            conn = new SqlConnection(conexao);
        }

        public string Caminho()
        {
            return conexao;
        }


        public int InsertTablePet(Pet pet)
        {
            int row = 0;
            try
            {
                conn.Open();
                string sql = $"insert into dbo.pet (familiaPet, racaPet, sexoPet, nomePet, disponivel) " +
                    $"values ('{pet.EspeciePet}', '{pet.Raca}', '{pet.SexoPet}', '{pet.NomePet}', '{pet.PetDisponivel}');";
                SqlCommand cmd = new SqlCommand(sql, conn);
                row = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Erro código " + e.Number + "Contate o administrador");
            }
            conn.Close();
            return row;
        }

        public bool SelectTablePet(string sql)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.HasRows)
                    {
                        Console.WriteLine("Não há éts cadastrados compativeis com a solicitação");
                        Until.Pause();
                        conn.Close();
                        return false;
                    }
                    while (r.Read())
                    {
                        Console.WriteLine($"Chip de Identificação: {r.GetInt32(0)}");
                        Console.WriteLine($"Familia: {r.GetString(1)}");
                        Console.WriteLine($"Raça: {r.GetString(2)}");
                        Console.WriteLine($"Sexo: {r.GetString(3)}");
                        Console.WriteLine($"Nome: {r.GetString(4)}\n");
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Erro código " + e.Number + "Contate o administrador");
            }
            conn.Close();
            return true;
        }
        public int UpdateTable(String sql)
        {
            int row = 0;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                row = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Erro código " + e.Number + "Contate o administrador");
            }
            conn.Close();
            return row;
        }
        public void InsertTableAdotante(Adotante adotante)
        {
            int row;
            try
            {
                conn.Open();
                string sql = $"insert into dbo.pessoa (cpf, nome, sexo, telefone, endereco, dataNascimento, status) values ('{adotante.Cpf}' , " +
                    $"'{adotante.Nome}', '{adotante.Sexo}', '{adotante.Telefone}', '{adotante.End}', '{adotante.DataNascimento}', '{adotante.Status}');";
                SqlCommand cmd = new SqlCommand(sql, conn);
                row = cmd.ExecuteNonQuery();
                Console.WriteLine(row);
                Console.ReadKey();
            }
            catch (SqlException e)
            {
                switch (e.Number)
                {
                    case 2627:
                        Console.WriteLine("Já existe um usuário cadastrado com esse CPF!!!");
                        break;
                    case 2628:
                        Console.WriteLine("Caracteres acima do permitido!!!");
                        break;
                    default:
                        break;
                }
                Until.Pause();
            }
            conn.Close();
        }

        public bool SelectTableAdotante(string sql)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader r = cmd.ExecuteReader();

                if (!r.HasRows)
                {
                    Console.WriteLine("Usuário não localizado!\nOperação cancelada!");
                    conn.Close();
                    Until.Pause();
                    return false;
                }

                while (r.Read())
                {
                    Console.WriteLine($"Nome: {r.GetString(1)}");
                    Console.WriteLine($"CPF: {r.GetString(0)}");
                    Console.WriteLine($"Sexo: {r.GetString(2)}");
                    Console.WriteLine($"Telefone: {r.GetString(3)}");
                    Console.WriteLine($"Endereço: {r.GetString(4)}");
                    Console.WriteLine($"Data de nascimento: {r.GetDateTime(5).ToShortDateString()}");
                    Console.WriteLine();
                }
                conn.Close();
                return true;
            }
            catch (SqlException e)
            {
                Console.WriteLine("Erro de código " + e.Number + "Contate o profissional responsável");
            }
            conn.Close();
            return false;
        }

        public bool SelectTableAdotanteInativa(string sql)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader r = cmd.ExecuteReader();

                if (!r.HasRows)
                {
                    Console.WriteLine("Usuário não localizado!!!\nOperação cancelada...");
                    conn.Close();
                    Until.Pause();
                    return false;
                }

                while (r.Read())
                {
                    Console.WriteLine($"Nome: {r.GetString(1)}");
                    Console.WriteLine($"CPF: {r.GetString(0)}");
                    Console.WriteLine($"Sexo: {r.GetString(2)}");
                    Console.WriteLine($"Tel: {r.GetString(3)}");
                    Console.WriteLine($"Endereço: {r.GetString(4)}");
                    Console.WriteLine($"Data de nascimento: {r.GetDateTime(5).ToShortDateString()}");
                    if (r.GetString(6)[0] == 'I') Console.WriteLine($"Status Cadastro: Inativo");
                    else Console.WriteLine($"Status Cadastro: Ativo");
                    Console.WriteLine();
                }
                conn.Close();
                return true;
            }
            catch (SqlException e)
            {
                Console.WriteLine("Erro código " + e.Number + "Contate o administrador");
            }
            conn.Close();
            return false;
        }
        public int InsertRegAdocao(string sql)
        {
            int row = 0;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                row = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException e)
            {
                if (e.Number == 2627) Console.WriteLine("Cadastro já realizado!");
                else Console.WriteLine("Erro de código " + e.Number + "Contate o profissional responsável");
            }
            conn.Close();
            return row;
        }

        public int SelectRegAdocao(string sql)
        {
            int row = 0;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.HasRows)
                    {
                        Console.WriteLine("Registro não encontrado!");
                        conn.Close();
                        return row;
                    }
                    else
                    {
                        while (r.Read())
                        {
                            Console.WriteLine($"CPF do adotante: {r.GetString(0)}");
                            Console.WriteLine($"Nome do adotante: {r.GetString(1)}");
                            Console.WriteLine($"Número do chip do pet: {r.GetInt32(2)}");
                            Console.WriteLine($"Nome do pet: {r.GetString(6)}");
                            Console.WriteLine($"Espécie do pet: {r.GetString(3)}");
                            Console.WriteLine($"Raça do pet: {r.GetString(4)}");
                            Console.WriteLine($"Data da adoção: {r.GetDateTime(5).ToShortDateString()}");
                            Console.WriteLine();
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Erro código " + e.Number + "Contate o administrador");
            }
            conn.Close();
            return row;
        }
    }
}
