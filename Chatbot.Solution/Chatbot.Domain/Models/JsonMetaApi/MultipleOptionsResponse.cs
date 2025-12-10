using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Domain.Models.JsonMetaApi
{
    internal class MultipleOptionsResponse
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Action
        {
            [JsonProperty("button")]
            public string? button { get; set; }

            [JsonProperty("sections")]
            public List<Section>? sections { get; set; }
        }

        public class Body
        {
            [JsonProperty("text")]
            public string? text { get; set; }
        }

        public class Footer
        {
            [JsonProperty("text")]
            public string? text { get; set; }
        }

        public class Header
        {
            [JsonProperty("type")]
            public string?   type { get; set; }

            [JsonProperty("text")]
            public string? text { get; set; }
        }

        public class Interactive
        {
            [JsonProperty("type")]
            public string? type { get; set; }

            [JsonProperty("header")]
            public Header? header { get; set; }

            [JsonProperty("body")]
            public Body? body { get; set; }

            [JsonProperty("footer")]
            public Footer? footer { get; set; }

            [JsonProperty("action")]
            public Action? action { get; set; }
        }

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

            [JsonProperty("interactive")]
            public Interactive? interactive { get; set; }
        }

        public class Row
        {
            [JsonProperty("id")]
            public string? id { get; set; }

            [JsonProperty("title")]
            public string? title { get; set; }

            [JsonProperty("description")]
            public string? description { get; set; }
        }

        public class Section
        {
            [JsonProperty("title")]
            public string? title { get; set; }

            [JsonProperty("rows")]
            public List<Row>? rows { get; set; }
        }


    }
}
