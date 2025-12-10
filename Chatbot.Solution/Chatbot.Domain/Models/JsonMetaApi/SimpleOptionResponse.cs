using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Domain.Models.JsonMetaApi
{
    internal class SimpleOptionResponse
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Root
        {
            [JsonProperty("messaging_product")]
            public string? messaging_product { get; set; }

            [JsonProperty("recipient_type")]
            public string? recipient_type { get; set; }

            [JsonProperty("to")]
            public string? to { get; set; }

            [JsonProperty("type")]
            public string? type { get; set; }

            [JsonProperty("text")]
            public Text? text { get; set; }
        }

        public class Text
        {
            [JsonProperty("preview_url")]
            public bool? preview_url { get; set; }

            [JsonProperty("body")]
            public string? body { get; set; }
        }


    }
}
