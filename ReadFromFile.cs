using CustomerAccounter.Properties;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccounter
{
    internal static class ReadFromFile
    {
        private static List<Client> clients = new List<Client>();
        //private Admin? admin;
        static string  path = @"../../../Resources/";
        public static Admin UserAdminRead()
        {
            string adminPath=path + @"Admin/admin.txt";
            if (File.Exists(adminPath))
            {
                try
                {
                    string[] lines = File.ReadAllLines(adminPath);
                    if (lines.Length >= 4)
                    {
                        Admin admin = new Admin
                        {
                            Login = lines[0],
                            Password = lines[1],
                            Name = lines[2],
                            Surname = lines[3],
                        };
                        return admin;
                    }
                    else

                    { MessageBox.Show("Недостатньо даних в файлі!"); }
                }

                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка при читанні файлу! {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Файл не знайдений!");
            }
            path = path.Replace(@"Admin/admin.txt", "");
            return null;
        }

        public static List<Fiziki> ReadFiziks()
        {
            string fiziks = path + @"Fiziki/fiziki.txt" /*@"../../../Resources/Fiziki/fiziki.txt"*/;
            List<Fiziki> tmp = new List<Fiziki>();
            if (File.Exists(fiziks))
            {
                try
                {
                    string[] lines = File.ReadAllLines(fiziks);
                    for (int i=0; i<lines.Length; i+=9) 
                    {
                        Fiziki fizik = new Fiziki
                        {
                            Login = lines[i],
                            Password = lines[i + 1],
                            Name = lines[i + 2],
                            Surname = lines[i + 3],
                            Code = int.Parse(lines[i + 4]),
                            phoneNumber = int.Parse(lines[i + 5]),
                            Email = lines[i + 6],
                            Adress = lines[i + 7]

                        };
                        tmp.Add(fizik);
                        if (string.IsNullOrWhiteSpace(lines[i]))
                        {
                            continue; 
                        }
                        clients.AddRange(tmp);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка при читанні файлу! {ex.Message}");
                }
            }
            return tmp;
        }

        public static List<FOP> ReadFOPs()
        {
            string fops = path + @"FOP/fop.txt";
            List<FOP> tmpFop = new List<FOP>();
            if (File.Exists(fops))
            {
                try
                {
                    string[] lines = File.ReadAllLines(fops);
                    for (int i = 0; i < lines.Length; i += 12)
                    {
                        //string bankString = lines[i + 8];
                        //if (Enum.TryParse<Bank>(bankString, out Bank bankEnum))
                        //{
                        //    // Успешно сконвертировано в перечисление
                        //    // bankEnum теперь содержит соответствующее значение перечисления
                        //}
                        //else
                        //{
                        //    // Обработка случая, когда строку невозможно сконвертировать в перечисление
                        //}
                        FOP tmp = new FOP
                        {
                            Login = lines[i],
                            Password = lines[i + 1],
                            Name = lines[i + 2],
                            Surname = lines[i + 3],
                            Code = int.Parse(lines[i + 4]),
                            phoneNumber = int.Parse(lines[i + 5]),
                            Email = lines[i + 6],
                            Adress= lines[i + 7],
                            BankAccount = decimal.Parse(lines[i + 8]),
                            MFO = int.Parse(lines[i + 9]),
                            bank = (Bank)Enum.Parse(typeof(Bank), lines[i + 10])
                        };
                        tmpFop.Add(tmp);
                        if (string.IsNullOrWhiteSpace(lines[i]))
                        {
                            continue;
                        }
                        clients.AddRange(tmpFop);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка при читанні файлу! {ex.Message}");
                }
            }
            return tmpFop;
        }
    }

    
    //public void ReadAllClients()
    //{
    //    ReadFiziks();
    //}
}

