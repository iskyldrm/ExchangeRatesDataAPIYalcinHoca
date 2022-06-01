using System;
using System.IO;
using System.Net;

namespace ExchangeRatesDataAPI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("To(TRY/EUR/USD)");
            var To = System.Console.ReadLine();
            System.Console.WriteLine("From(TRY/EUR/USD)");
            var From = System.Console.ReadLine();
            System.Console.WriteLine("Amount(5/10/20)");
            var Amount = System.Console.ReadLine();

            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create($"https://api.apilayer.com/exchangerates_data/convert?to={To}&from={From}&amount={Amount}");
            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add("apikey", "0ltEBEOQd0soGR6rFeEiU39P77oFVxX1");

            string test = string.Empty;

            using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                test = reader.ReadToEnd();
                reader.Close();
                stream.Close();
                Console.WriteLine(test);
            }



            //var CustomerIdList = JsonSerializer.Deserialize<List<Customer>>(test, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            //foreach (Customer item in CustomerIdList)
            //{
            //    System.Console.WriteLine(item.CompanyName);
            //}
        }
    }
}
