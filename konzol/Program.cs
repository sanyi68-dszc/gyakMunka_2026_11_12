using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace konzol
{
    internal class Program
    {
        static string connectionString = "server=localhost;user=root;password=;database=diak_gyak;";
        static void Main(string[] args)
        {
            bool kilepes = false;
            int menupont = 0;
            int gyoker = 0;
            do
            {
                Menu(menupont, gyoker);
                int.TryParse(Console.ReadLine(), out menupont);
                if (menupont <= 5 && menupont >= 1)
                {
                    switch (menupont)
                    {
                        case 1:
                            DiakLista();
                            break;
                        case 2:
                            UjDiak();
                            break;
                        case 3:
                            DiakModosit();
                            break;
                        case 4:
                            DiakTorol();
                            break;
                        case 5:
                            kilepes = true;
                            break;
                        default:
                            break;
                    }
                }
                else if ((menupont > 5 || menupont < 1) && gyoker == 30)
                {
                    Console.WriteLine("...tudod mit? Hagyd a francba ezt az egészet!");
                    kilepes = true;
                }
                else
                {
                    Console.WriteLine("Kérem a fenti 5 menüpont egyikéből válasszon!");
                    gyoker++;
                }
            } while (!kilepes);
        }
        
        static void Menu(int menupont, int gyoker)
        {
            menupont = 0;
            gyoker = 0;
            Console.WriteLine("==== Diáknyílvántartó ====");
            Console.WriteLine();
            Console.WriteLine("1 - Diákok listázása\n2 - Új diák felvétele\n3 - Diák adatainak módosítása\n4 - Diák törlése\n5 - Kilépés");
        }

        static void DiakLista()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string sql = "SELECT * FROM diakok";

                    MySqlCommand cmd = new MySqlCommand(sql, connection);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["nev"]} - {reader["eletkor"]} éves - {reader["osztaly"]}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Hiba, {e.Message}");
                }
            }
        }

        static void UjDiak()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    Console.WriteLine("Kérem adja meg az új diák következő adatait:");
                    Console.Write("Név: ");
                    string nev = Console.ReadLine();
                    Console.Write("Életkor: ");
                    int.TryParse(Console.ReadLine(), out int eletkor);
                    Console.Write("Osztály: ");
                    string osztaly = Console.ReadLine();

                    string sql = $"INSERT INTO diakok (nev, eletkor, osztaly) VALUES (@nev, @eletkor, @osztaly)";

                    MySqlCommand cmd = new MySqlCommand(sql, connection);

                    cmd.Parameters.AddWithValue("@nev", nev);
                    cmd.Parameters.AddWithValue("@eletkor", eletkor);
                    cmd.Parameters.AddWithValue("@osztaly", osztaly);

                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Új diák hozzáadva!");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Hiba, {e.Message}");
                }
            }
        }

        static void DiakModosit()
        {
            bool ervenyes = false;
            int id = 0;
            do
            {
                Console.Write("Kérem adja meg a módosítandó diák azonosítószámát: ");
                int.TryParse(Console.ReadLine(), out id);
                if (id <= 0)
                {
                    Console.WriteLine("Kérem adjon meg érvényes, azaz pozitív egész számot az azonosítószámnak!");
                }
                else
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();

                            string sql = $"SELECT * FROM diakok WHERE id = {id}";

                            MySqlCommand cmd = new MySqlCommand(sql, connection);

                            MySqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                ervenyes = true;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Hiba, {e.Message}");
                        }
                    }
                    if (!ervenyes)
                    {
                        Console.WriteLine("Nincs diák ezzel az azonosítószámmal!");
                    }
                }
            } while (!ervenyes);
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                
                    Console.WriteLine("Kérem adja meg a módosítandó diák következő adatait:");
                    Console.Write("Név: ");
                    string nev = Console.ReadLine();
                    Console.Write("Életkor: ");
                    int.TryParse(Console.ReadLine(), out int eletkor);
                    Console.Write("Osztály: ");
                    string osztaly = Console.ReadLine();
                
                    string sql = $"UPDATE diakok SET nev = @nev, eletkor = @eletkor, osztaly = @osztaly WHERE id = {id}";

                    MySqlCommand cmd = new MySqlCommand(sql, connection);

                    cmd.Parameters.AddWithValue("@nev", nev);
                    cmd.Parameters.AddWithValue("@eletkor", eletkor);
                    cmd.Parameters.AddWithValue("@osztaly", osztaly);

                    cmd.ExecuteNonQuery();
                
                    Console.WriteLine("Diák módosítva!");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Hiba, {e.Message}");
                }
            }
        }

        static void DiakTorol()
        {
            bool ervenyes = false;
            int id = 0;
            do
            {
                Console.Write("Kérem adja meg a tölöni kívánt diák azonosítószámát: ");
                int.TryParse(Console.ReadLine(), out id);
                if (id <= 0)
                {
                    Console.WriteLine("Kérem adjon meg érvényes, azaz pozitív egész számot az azonosítószámnak!");
                }
                else
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();

                            string sql = $"SELECT * FROM diakok WHERE id = {id}";

                            MySqlCommand cmd = new MySqlCommand(sql, connection);

                            MySqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                ervenyes = true;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Hiba, {e.Message}");
                        }
                    }
                    if (!ervenyes)
                    {
                        Console.WriteLine("Nincs diák ezzel az azonosítószámmal!");
                    }
                }
            } while (!ervenyes);
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string sql = $"DELETE FROM diakok WHERE id = {id}";

                    MySqlCommand cmd = new MySqlCommand(sql, connection);

                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Diák törölve!");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Hiba, {e.Message}");
                }
            }
        }
    }
}
