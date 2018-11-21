using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace TransliterateText
{
    class Program
    {
        static void TransliterateText()
        {
            string host = "https://api.cognitive.microsofttranslator.com";
            // Params are appended directly to the route
            string route = "/transliterate?api-version=3.0&language=ja&fromScript=jpan&toScript=latn";
            string subscriptionKey = "YOUR_SUBSCRIPTION_KEY";
            // String to transliterate
            System.Object[] body = new System.Object[] { new { Text = @"こんにちは" } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Set the method to POST
                request.Method = HttpMethod.Post;

                // Construct the full URI
                request.RequestUri = new Uri(host + route);

                // Add the serialized JSON object to your request
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                // Set the authorization header
                request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

                // Send request, get response
                var response = client.SendAsync(request).Result;
                var jsonResponse = response.Content.ReadAsStringAsync().Result;

                // Print the response
                Console.WriteLine(jsonResponse);
                Console.WriteLine("Press any key to continue.");
            }
        }
        static void Main(string[] args)
        {
            TransliterateText();
            Console.ReadLine();
        }
    }
}
