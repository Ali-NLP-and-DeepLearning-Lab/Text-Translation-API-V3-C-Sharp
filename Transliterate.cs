// This sample requires C# 7.1 or later for async/await.

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
// Install Newtonsoft.Json with NuGet
using Newtonsoft.Json;

namespace TransliterateTextSample
{
    /// <summary>
    /// The C# classes that represents the JSON returned by the Translator Text API.
    /// </summary>
    public class TransliterationResult
    {
        public string Text { get; set; }
        public string Script { get; set; }
    }

    class Program
    {
        // Async call to the Translator Text API
        static public async Task TransliterateTextRequest(string subscriptionKey, string host, string route, string inputText)
        {
            System.Object[] body = new System.Object[] { new { Text = inputText } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(host + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

                // Send the request and get response.
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                // Read response as a string.
                string result = await response.Content.ReadAsStringAsync();
                TransliterationResult[] deserializedOutput = JsonConvert.DeserializeObject<TransliterationResult[]>(result);
                // Iterate over the deserialized results.
                foreach (TransliterationResult o in deserializedOutput)
                {
                       Console.WriteLine("Transliterated to {0} script: {1}", o.Script, o.Text);
                }
            }
        }

        static async Task Main(string[] args)
        {
            // This is our main function.
            // Output languages are defined in the route.
            // For a complete list of options, see API reference.
            // https://docs.microsoft.com/azure/cognitive-services/translator/reference/v3-0-transliterate
            string subscriptionKey = "YOUR_TRANSLATOR_TEXT_KEY_GOES_HERE";
            string host = "https://api.cognitive.microsofttranslator.com";
            string route = "/transliterate?api-version=3.0&language=ja&fromScript=jpan&toScript=latn";
            string textToTransliterate = @"こんにちは";
            await TransliterateTextRequest(subscriptionKey, host, route, textToTransliterate);
        }
    }
}
