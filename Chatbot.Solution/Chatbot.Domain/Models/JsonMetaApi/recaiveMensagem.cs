using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chatbot.Domain.Models.JsonMetaApi
{
    public class recaiveMensagem
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Change
        {
            [JsonProperty("value")]
            public Value value { get; set; }

            [JsonProperty("field")]
            public string field { get; set; }
        }

        public class Contact
        {
            [JsonProperty("profile")]
            public Profile profile { get; set; }

            [JsonProperty("wa_id")]
            public string wa_id { get; set; }
        }

        public class Entry
        {
            [JsonProperty("id")]
            public string id { get; set; }

            [JsonProperty("changes")]
            public List<Change> changes { get; set; }
        }

        public class Message
        {
            [JsonProperty("from")]
            public string from { get; set; }

            [JsonProperty("id")]
            public string id { get; set; }

            [JsonProperty("timestamp")]
            public string timestamp { get; set; }

            [JsonProperty("text")]
            public Text text { get; set; }

            [JsonProperty("type")]
            public string type { get; set; }
        }

        public class Metadata
        {
            [JsonProperty("display_phone_number")]
            public string display_phone_number { get; set; }

            [JsonProperty("phone_number_id")]
            public string phone_number_id { get; set; }
        }

        public class Profile
        {
            [JsonProperty("name")]
            public string name { get; set; }
        }

        public class Root
        {
            [JsonProperty("object")]
            public string @object { get; set; }

            [JsonProperty("entry")]
            public List<Entry> entry { get; set; }
        }

        public class Text
        {
            [JsonProperty("body")]
            public string body { get; set; }
        }

        public class Value
        {
            [JsonProperty("messaging_product")]
            public string messaging_product { get; set; }

            [JsonProperty("metadata")]
            public Metadata metadata { get; set; }

            [JsonProperty("contacts")]
            public List<Contact> contacts { get; set; }

            [JsonProperty("messages")]
            public List<Message> messages { get; set; }
        }


    }
}
