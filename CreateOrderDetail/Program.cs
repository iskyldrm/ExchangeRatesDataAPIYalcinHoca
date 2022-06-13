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
            #region SelectEmployee
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
            var employEeID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(employEeID + " nolu sipariş");

            #endregion

            #region SelectCustomer
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
            var customerId = Console.ReadLine();
            Console.WriteLine("\n" + employEeID + " nolu çalışan " + customerId + " nolu müşteriye");
            #endregion

            #region CreatingOrder
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
            List<string> yeni = new List<string>();
            foreach (var item in Urunler)
            {
                var dizi = item.Split("*");
                yeni.Add(dizi[0]);
                yeni.Add(dizi[1]);

            }
            List<int> urun = new List<int>();
            List<int> adet = new List<int>();
            for (int i = 0; i < yeni.Count; i += 2)
            {
                int gelen = Convert.ToInt32(yeni[i]);
                int gelen1 = Convert.ToInt32(yeni[i + 1]);
                urun.Add(gelen);
                adet.Add(gelen1);
            }
            Console.WriteLine("\n" + employEeID + " nolu çalışan " + customerId + " nolu müşteriye aşağıdaki ürünleri");
            foreach (var item in urun)
            {
                Console.Write(item.ToString() + ",   ");
            }
            Console.WriteLine("\nAşağıdaki adet sayısı kadar");
            foreach (var item in adet)
            {
                Console.Write(item.ToString() + ",   ");
            }




            #endregion

            #region SelectShipper
            HttpWebRequest httpWebRequest3 = (HttpWebRequest)HttpWebRequest.Create($"https://localhost:44349/Values/GetShipper");
            httpWebRequest.Method = "GET";

            string Shippers = string.Empty;
            using (HttpWebResponse response1 = (HttpWebResponse)httpWebRequest3.GetResponse())
            {
                Stream stream = response1.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                Shippers = reader.ReadToEnd();
                reader.Close();
                stream.Close();

            }
            var shipper = JsonSerializer.Deserialize<List<ShipperDTO>>(Shippers, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Console.WriteLine("\nKargocular | Id | İsim ");
            foreach (ShipperDTO item in shipper)
            {
                Console.WriteLine("Çalışanlar:  " + item.shipperId + "    " + item.shipperName);
            }
            Console.Write("Lütfen hangi kargocu olduğunuzu belirtiniz(ex=>Id=1):");
            var shipVia = Convert.ToInt32(Console.ReadLine());
            var shipName = " ";
            foreach (var item in shipper)
            {
                if (item.shipperId == shipVia)
                {
                    shipName = item.shipperName;
                }
            }
            Console.WriteLine("\n" + employEeID + " nolu çalışan " + customerId + " nolu müşteriye aşağıdaki ürünleri");
            foreach (var item in urun)
            {
                Console.Write(item.ToString() + ",   ");
            }
            Console.WriteLine("\nAşağıdaki adet sayısı kadar");
            foreach (var item in adet)
            {
                Console.Write(item.ToString() + ",   ");
            }
            Console.WriteLine("\n " + shipVia + "nolu kargocu ile gönderecektir.");

            #endregion

            #region POSTCreatOrderDATABASE
            HttpWebRequest httpWebRequest4 = (HttpWebRequest)HttpWebRequest.Create($"https://localhost:44349/Values/CreatOrder?employEeID={employEeID}&customerId={customerId}&shipVia={shipVia}&shipName={shipName}");
            httpWebRequest.Method = "GET";

            int effected = 0;
            using (HttpWebResponse responseCreatOrder = (HttpWebResponse)httpWebRequest4.GetResponse())
            {
                Stream stream = responseCreatOrder.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                effected = Convert.ToInt32(reader.ReadToEnd());
                reader.Close();
                stream.Close();

            }
            Console.WriteLine(effected.ToString());
            #endregion

        }
    }
}
