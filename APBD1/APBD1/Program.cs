using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace APBD1
{
    class Program
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        static async Task Main(string[] args)
        {
            Console.Write("URL: ");
            var address = Console.ReadLine();

            try
            {
                var response = await _httpClient.GetAsync(address);
                var content = await response.Content.ReadAsStringAsync();

                var matches = Regex.Matches(content, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);

                Console.WriteLine("Found addresses:");
                foreach(var email in matches)
                {
                    Console.WriteLine(email);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
