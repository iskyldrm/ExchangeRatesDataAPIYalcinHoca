using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;

namespace CreateOrderDetail
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Employee
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create($"Https://localhost:44349/Values/GetEmployee");
            httpWebRequest.Method = "GET";

            string Employee = string.Empty;
            using (HttpWebResponse response1 = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                Stream stream = response1.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                Employee = reader.ReadToEnd();
                reader.Close();
                stream.Close();

            }
            var employees = JsonSerializer.Deserialize<List<EmployeeDTO>>(Employee, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Console.WriteLine("Çalışanlar | Id | İsim ");
            foreach (EmployeeDTO item in employees)
            {
                Console.WriteLine("Çalışanlar:  " + item.id + "    " + item.firstLastName);
            }
            Console.Write("Lütfen hangi çalışan olduğunuzu belirtiniz(ex=>Id=10):");
            var EmployeeId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(EmployeeId + " nolu sipariş");

            #endregion

            #region Customer
            HttpWebRequest httpWebRequest1 = (HttpWebRequest)HttpWebRequest.Create($"https://localhost:44349/Values/GetCustomer");
            httpWebRequest1.Method = "GET";

            string Customers = string.Empty;
            using (HttpWebResponse response2 = (HttpWebResponse)httpWebRequest1.GetResponse())
            {
                Stream stream = response2.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                Customers = reader.ReadToEnd();
                reader.Close();
                stream.Close();

            }
            var Customer = JsonSerializer.Deserialize<List<CustomerDTO>>(Customers, new JsonSerializerOptions { PropertyNameCaseInsensitive = false });
            Console.WriteLine("Müşteriler | Id | İsim ");
            foreach (CustomerDTO item in Customer)
            {
                Console.WriteLine("Müşteriler:  " + item.customerId + "    " + item.customerName);
            }
            Console.Write("\nLütfen siparişin kime olduğunu beliritniz(ex=>Id=ALFKI):");
            var CustomerId = Console.ReadLine();
            Console.WriteLine("\n" + EmployeeId + " nolu çalışan " + CustomerId + " nolu müşteriye");
            #endregion

            #region Customer
            HttpWebRequest httpWebRequest2 = (HttpWebRequest)HttpWebRequest.Create($"https://localhost:44349/Values/GetProduct");
            httpWebRequest1.Method = "GET";

            string Products = string.Empty;
            using (HttpWebResponse response3 = (HttpWebResponse)httpWebRequest2.GetResponse())
            {
                Stream stream = response3.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                Products = reader.ReadToEnd();
                reader.Close();
                stream.Close();

            }
            var Product = JsonSerializer.Deserialize<List<ProductDTO>>(Products, new JsonSerializerOptions { PropertyNameCaseInsensitive = false });
            Console.WriteLine("| Id | İsim           | Fiyat");
            foreach (ProductDTO item in Product)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(item.productId.ToString() + " ");
                stringBuilder.Append(item.productName.ToString() + " ");
                stringBuilder.Append(item.unitprice.ToString() + " ");
                Console.WriteLine(stringBuilder);
            }
            Console.Write("\nLütfen eklemek istedğiniz ürünler ve adetleri girin(ex=>ID*ADET):");
            List<string> Urunler = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                var ProductId = Console.ReadLine();
                if (ProductId == "/")
                    break;

                Urunler.Add(ProductId);
            }
            var yeni = new string[200];
            foreach (var item in Urunler)
            {
                yeni = item.ToString().Split("*");

            }
            List<int> urun = new List<int>();
            List<int> adet = new List<int>();
            for (int i = 0; i < yeni.Length; i += 2)
            {
                int gelen = Convert.ToInt32(yeni[i]);
                int gelen1 = Convert.ToInt32(yeni[i + 1]);
                urun.Add(gelen);
                adet.Add(gelen1);
            }
            foreach (var item in urun)
            {
                Console.WriteLine(item.ToString() + ",");
            }
            foreach (var item in adet)
            {
                Console.WriteLine(item.ToString() + ",");
            }



            Console.WriteLine("\n" + EmployeeId + " nolu çalışan " + CustomerId + " nolu müşteriye");
            #endregion


        }
    }
}
