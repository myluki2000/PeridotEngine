using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PeridotEngine.Graphics.Effects;
using PeridotEngine.Scenes.Scene3D;

namespace PeridotEngine.IO.JsonConverters
{
    public class EffectPropertiesJsonConverter : JsonConverter
    {
        private readonly Scene3D scene;

        public EffectPropertiesJsonConverter(Scene3D scene)
        {
            this.scene = scene;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value == null) return;

            EffectBase.EffectProperties effectProperties = (EffectBase.EffectProperties)value;

            JObject o = JObject.FromObject(effectProperties);
            
            writer.WriteStartObject();
            writer.WritePropertyName("Effect");
            writer.WriteValue(effectProperties.Effect.GetType().FullName);
            writer.WritePropertyName("Properties");
            o.WriteTo(writer);
            writer.WriteEndObject();
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            JToken root = JToken.ReadFrom(reader);

            string typeName = root["Effect"].Value<string>();
            Type effectType = EffectPool.GetRegisteredEffectTypes().First(x => x.FullName == typeName);

            EffectBase effect = scene.Resources.EffectPool.Effect(effectType);

            Type effectPropertiesType = effect.GetPropertiesType();

            EffectBase.EffectProperties effectProperties = (EffectBase.EffectProperties)root["Properties"].ToObject(effectPropertiesType);
            effectPropertiesType.GetProperty("Effect").SetValue(effectProperties, effect);
            return effectProperties;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableTo(typeof(EffectBase.EffectProperties));
        }
    }
}
