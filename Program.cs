using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Namespace: ");
            string name_space = Console.ReadLine();

            Console.WriteLine("FilePrefix: ");
            string tela = Console.ReadLine();
            string parentfolder = "nova-tela-" + tela;

            Directory.CreateDirectory(parentfolder);
            Console.WriteLine(parentfolder);

            foreach (string fileLine in File.ReadAllLines("nova-estrutura.txt"))
            {
                string[] lines = fileLine.Split('|');
                string line = lines[0];
                string templateLocation = string.Empty;

                if (lines.Length > 1) {
                    templateLocation = lines[1];
                }

                string path = parentfolder + "\\" + line.Replace("{nova-tela}", tela);
                string[] split = path.Split('\\');
                string tab = "";

                split.Skip(1).ToList().ForEach(s => tab += "---");

                if (!line.Contains(".cs") && !line.Contains(".html") && !line.Contains(".js"))
                {
                    Directory.CreateDirectory(path);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }
                else
                {
                    using (var fileStream = File.Create(path))
                    {
                        fileStream.Close();
                        
                        if (!string.IsNullOrEmpty(templateLocation))
                        {
                            List<string> templateText = File.ReadAllLines(templateLocation).ToList();

                            List<string> strResult = new List<string>();

                            templateText.ForEach(t => strResult.Add(t.Replace("{namespace}", name_space).Replace("{nova-tela}", tela)));
                            
                            File.WriteAllLines(path, strResult.ToArray());
                        }   
                    }

                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }

                Console.WriteLine(tab + split[split.Length - 1]);
            }

            Console.ReadLine();
        }
    }
}
