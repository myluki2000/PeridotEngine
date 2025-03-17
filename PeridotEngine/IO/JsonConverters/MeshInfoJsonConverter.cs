using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PeridotEngine.Scenes.Scene3D;

namespace PeridotEngine.IO.JsonConverters
{
    public class MeshInfoJsonConverter(Scene3D scene) : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is MeshResources.MeshInfo mi)
            {
                writer.WriteValue(mi.Name);
            }
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            return scene.Resources.MeshResources.GetAllMeshes().First(x => x.Name == reader.Value.ToString());
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(MeshResources.MeshInfo);
        }
    }
}
