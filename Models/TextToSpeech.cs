using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace TikTokTTS
{
    public static class TextToSpeech
    {
        public static async Task SaveTextAsSpeech(TextToSpeechOperation operation)
        {
            if (operation.Path == null)
                throw new NullReferenceException("Output path is null");

            byte[] result = await CallTikTokAPIAsync(operation);
            File.WriteAllBytes(operation.Path, result);
        }

        public static async Task<byte[]> CallTikTokAPIAsync(TextToSpeechOperation textToSpeechOperation)
        {
            string voiceCode = textToSpeechOperation.Voice;
            string textToSpeech = FormatInputText(textToSpeechOperation.Text);

            string URL = $"https://api16-normal-useast5.us.tiktokv.com/media/api/text/speech/invoke/?text_speaker={voiceCode}&req_text={textToSpeech}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync(URL, new FormUrlEncodedContent(new Dictionary<string, string>()));
                string responseContentAsString = await response.Content.ReadAsStringAsync();

                JToken jsonResult = JsonConvert.DeserializeObject<JToken>(responseContentAsString);
                string audioInBase64 = jsonResult["data"]["v_str"].ToString();
                return Convert.FromBase64String(audioInBase64);
            }
        }

        private static string FormatInputText(string text)
        {
            text = text.Replace("+", "plus");
            text = text.Replace(" ", "+");
            text = text.Replace("&", "and");
            return text;
        }
    }

    public class TextToSpeechOperation
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
        public string Voice { get; set; }
        public string Path { get; set; }

        public TextToSpeechOperation()
        {
            Date = System.DateTime.Now.ToString("HH:mm:ss, dd.MM.yyyy");
        }
    }
}
