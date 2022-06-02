using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;

namespace ExchangeRatesDataAPI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("CustomerId= ex(VINET): ");
            var CustomerId = System.Console.ReadLine();
            System.Console.WriteLine("OrderId= ex(10248): ");
            var OrderId = System.Console.ReadLine();

            HttpWebRequest httpWebRequest1 = (HttpWebRequest)HttpWebRequest.Create($"https://localhost:44304/weatherforecast/1?customerId={CustomerId}&orderId={OrderId}");
            httpWebRequest1.Method = "GET";

            string Toplam = string.Empty;
            using (HttpWebResponse response1 = (HttpWebResponse)httpWebRequest1.GetResponse())
            {
                Stream stream = response1.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                Toplam = reader.ReadToEnd();
                reader.Close();
                stream.Close();

            }

            var CustomerIdList = JsonSerializer.Deserialize<List<toplamDTO>>(Toplam, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var toplam2 = 0;
            Console.WriteLine(CustomerIdList[0].CustomerName);
            Console.WriteLine(CustomerIdList[0].OrderId);
            foreach (toplamDTO item in CustomerIdList)
            {
                toplam2 += (int)item.Toplam;

            }
            Console.WriteLine("TOPLAM: " + toplam2);

            //System.Console.WriteLine("To(TRY/EUR/USD)");
            //var To = System.Console.ReadLine();
            var To = "TRY";
            //System.Console.WriteLine("From(TRY/EUR/USD)");
            //var From = System.Console.ReadLine();
            var From = "USD";
            //System.Console.WriteLine("Amount(5/10/20)");
            //var Amount = System.Console.ReadLine();

            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create($"https://api.apilayer.com/exchangerates_data/convert?to={To}&from={From}&amount={toplam2}");
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

            //ExChange exChange = new ExChange();
            //exChange.amount = toplam2.ToString();

            //var CustomerIdList1 = JsonSerializer.Deserialize<List<ExChange>>(test, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            //foreach (ExChange item in CustomerIdList1)
            //{
            //    Console.WriteLine(item.from);
            //    Console.WriteLine(item.to);
            //    Console.WriteLine(item.toplam2);
            //}
        }
    }
}
