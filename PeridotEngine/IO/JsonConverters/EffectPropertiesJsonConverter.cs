using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PeridotEngine.Graphics.Effects;

namespace PeridotEngine.IO.JsonConverters
{
    public class EffectPropertiesJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value == null) return;

            EffectBase.EffectProperties effectProperties = (EffectBase.EffectProperties)value;

            JObject o = JObject.FromObject(effectProperties);

            o.Add("Effect", effectProperties.Effect.GetType().ToString());

            o.WriteTo(writer);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableTo(typeof(EffectBase.EffectProperties));
        }
    }
}
