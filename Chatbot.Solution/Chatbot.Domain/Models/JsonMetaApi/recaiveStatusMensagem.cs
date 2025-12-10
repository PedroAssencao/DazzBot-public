using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chatbot.Domain.Models.JsonMetaApi
{
    public class recaiveStatusMensagem
    {
        // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
        public class Change
        {
            [JsonPropertyName("value")]
            public Value value { get; set; }

            [JsonPropertyName("field")]
            public string field { get; set; }
        }

        public class Conversation
        {
            [JsonPropertyName("id")]
            public string id { get; set; }

            [JsonPropertyName("expiration_timestamp")]
            public string expiration_timestamp { get; set; }

            [JsonPropertyName("origin")]
            public Origin origin { get; set; }
        }

        public class Entry
        {
            [JsonPropertyName("id")]
            public string id { get; set; }

            [JsonPropertyName("changes")]
            public List<Change> changes { get; set; }
        }

        public class Metadata
        {
            [JsonPropertyName("display_phone_number")]
            public string display_phone_number { get; set; }

            [JsonPropertyName("phone_number_id")]
            public string phone_number_id { get; set; }
        }

        public class Origin
        {
            [JsonPropertyName("type")]
            public string type { get; set; }
        }

        public class Pricing
        {
            [JsonPropertyName("billable")]
            public bool billable { get; set; }

            [JsonPropertyName("pricing_model")]
            public string pricing_model { get; set; }

            [JsonPropertyName("category")]
            public string category { get; set; }
        }

        public class Root
        {
            [JsonPropertyName("object")]
            public string @object { get; set; }

            [JsonPropertyName("entry")]
            public List<Entry> entry { get; set; }
        }

        public class Status
        {
            [JsonPropertyName("id")]
            public string id { get; set; }

            [JsonPropertyName("status")]
            public string status { get; set; }

            [JsonPropertyName("timestamp")]
            public string timestamp { get; set; }

            [JsonPropertyName("recipient_id")]
            public string recipient_id { get; set; }

            [JsonPropertyName("conversation")]
            public Conversation conversation { get; set; }

            [JsonPropertyName("pricing")]
            public Pricing pricing { get; set; }
        }

        public class Value
        {
            [JsonPropertyName("messaging_product")]
            public string messaging_product { get; set; }

            [JsonPropertyName("metadata")]
            public Metadata metadata { get; set; }

            [JsonPropertyName("statuses")]
            public List<Status> statuses { get; set; }
        }


    }
}
