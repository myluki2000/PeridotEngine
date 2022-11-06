using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PeridotEngine.Graphics.Effects;
using PeridotEngine.Graphics.Geometry;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.ECS.Components;

namespace PeridotEngine.IO.JsonConverters
{
    public class StaticMeshComponentJsonConverter : JsonConverter
    {
        private readonly Scene3D scene;

        public StaticMeshComponentJsonConverter(Scene3D scene)
        {
            this.scene = scene;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value == null) return;
            StaticMeshComponent component = (StaticMeshComponent)value;
            
            writer.WriteStartObject();
            writer.WritePropertyName("EffectProperties");
            serializer.Serialize(writer, component.EffectProperties);
            writer.WritePropertyName("Mesh");
            writer.WriteValue(component.Mesh.Name);
            writer.WritePropertyName("CastShadows");
            writer.WriteValue(component.CastShadows);
            writer.WriteEndObject();
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            JObject root = JToken.ReadFrom(reader) as JObject;

            JToken ep = root["EffectProperties"];

            EffectBase.EffectProperties effectProperties =
                serializer.Deserialize<EffectBase.EffectProperties>(ep.CreateReader());

            string meshName = root["Mesh"].Value<string>();

            MeshResources.MeshInfo? mesh = scene.Resources.MeshResources.GetAllMeshes().First(x => x.Name == meshName);

            if (mesh == null) throw new Exception("Could not find mesh with name " + meshName);

            if (root.ContainsKey("CastShadows"))
            {
                return new StaticMeshComponent(scene, mesh, effectProperties, root.Value<bool>("CastShadows"));
            }
            else
            {
                return new StaticMeshComponent(scene, mesh, effectProperties);
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(StaticMeshComponent);
        }
    }
}
