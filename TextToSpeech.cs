using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TikTokTTS
{
    public static class TextToSpeech
    {
        public static async Task<byte[]> CallTikTokAPIAsync(TextToSpeechOperation textToSpeechOperation)
        {
            string voiceCode = textToSpeechOperation.Voice;
            string textToSpeech = FormatInputText(textToSpeechOperation.Text);
            System.Diagnostics.Trace.WriteLine(textToSpeech.Length);

            string URL = $"https://api16-normal-useast5.us.tiktokv.com/media/api/text/speech/invoke/?text_speaker={voiceCode}&req_text={textToSpeech}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync(URL, new FormUrlEncodedContent(new Dictionary<string, string>()));
                string responseContentAsString = await response.Content.ReadAsStringAsync();

                System.Diagnostics.Trace.WriteLine(response.StatusCode + " | " + responseContentAsString.Length);

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
}
