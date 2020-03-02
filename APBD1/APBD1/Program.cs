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
            if (args.Length == 0)
            {
                throw new ArgumentNullException();
            }

            if (!Uri.IsWellFormedUriString(args[0], UriKind.Absolute))
            {
                throw new ArgumentException();
            }

            try
            {
                var response = await _httpClient.GetAsync(args[0]);
                var content = await response.Content.ReadAsStringAsync();

                var matches = Regex.Matches(content, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);

                if (matches.Count == 0)
                {
                    Console.WriteLine("Nie znaleziono adresów email.");
                }
                else
                {
                    Console.WriteLine("Znalezione adresy:");
                    foreach (var email in matches)
                    {
                        Console.WriteLine(email);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Błąd w czasie pobierania strony.");
            }
            finally
            {
                _httpClient.Dispose();
            }
        }
    }
}
