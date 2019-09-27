using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ParserBanco
{
    class Program
    {
        // https://stackoverflow.com/questions/46330864/steam-api-all-games
        // http://api.steampowered.com/ISteamApps/GetAppList/v0002/?key=STEAMKEY&format=json
        // http://store.steampowered.com/api/appdetails?appids=1093800

        public const string ArquivoGeral = "C:\\Users\\ZELIA.PORTO\\Desktop\\2º semestre\\phantom games\\ParserBanco\\all_games.json";
        public const string CaminhoArquivos = "C:\\Users\\ZELIA.PORTO\\Desktop\\2º semestre\\phantom games\\ParserBanco\\Arquivos\\";

        private static DetalhesAplicativo LerAplicativo(string json)
        {
            int i = json.IndexOf('{', 2);
            int f = json.LastIndexOf('}');
            json = json.Substring(i, f - i);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<DetalhesAplicativo>(json);
        }

        static void Main(string[] args)
        {
            ListaGeral listaGeral = Newtonsoft.Json.JsonConvert.DeserializeObject<ListaGeral>(System.IO.File.ReadAllText(ArquivoGeral, Encoding.UTF8));
            Array.Sort(listaGeral.applist.apps, (a, b) =>
            {
                return (a.appid < b.appid ? -1 : 1);
            });

            for (int i = 0; i < listaGeral.applist.apps.Length; i++)
            {
                if ((i % 1000) == 0)
                {
                    Console.WriteLine(i + " - " + DateTime.Now);
                }
                long id = listaGeral.applist.apps[i].appid;
                try {
                    HttpWebRequest request = WebRequest.CreateHttp("http://store.steampowered.com/api/appdetails?appids=" + id);
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        using (System.IO.TextReader reader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            string json = reader.ReadToEnd();
                            System.IO.File.WriteAllText(CaminhoArquivos + id + ".json", json, Encoding.UTF8);
                        }
                    }
                }
                catch
                {
                    // Apenas ignora e pula para o próximo!
                    Console.WriteLine("Erro " + id);
                }
                System.Threading.Thread.Sleep(1000);
            }

            Console.WriteLine("Fim!");
            Console.ReadLine();
        }
    }
}
