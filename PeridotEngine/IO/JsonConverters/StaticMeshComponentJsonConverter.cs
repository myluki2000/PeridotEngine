using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PeridotWindows.ECS.Components;

namespace PeridotEngine.IO.JsonConverters
{
    internal class StaticMeshComponentJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value == null) return;
            StaticMeshComponent component = (StaticMeshComponent)value;
            
            writer.WriteStartObject();
            writer.WritePropertyName("EffectProperties");
            serializer.Serialize(writer, component.EffectProperties);
            writer.WritePropertyName("Mesh");
            writer.WriteValue(component.Mesh.Name);
            writer.WriteEndObject();
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(StaticMeshComponent);
        }
    }
}
