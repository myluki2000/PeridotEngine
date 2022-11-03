using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PeridotEngine.Graphics.Effects;
using PeridotEngine.Scenes.Scene3D;

namespace PeridotEngine.IO.JsonConverters
{
    public class SceneResourcesJsonConverter : JsonConverter
    {
        private readonly Scene3D scene;

        public SceneResourcesJsonConverter(Scene3D scene)
        {
            this.scene = scene;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value == null) return;

            SceneResources sceneResources = (SceneResources)value;

            writer.WriteStartObject();

            writer.WritePropertyName("TextureResources");
            serializer.Serialize(writer, sceneResources.TextureResources);

            writer.WritePropertyName("MeshResources");
            serializer.Serialize(writer, sceneResources.MeshResources);

            writer.WriteEndObject();
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            JToken root = JToken.ReadFrom(reader);

            SceneResources sceneResources = new SceneResources(
                root["TextureResources"].ToObject<TextureResources>(),
                root["MeshResources"].ToObject<MeshResources>(),
                new EffectPool(scene)
            );

            foreach (MeshResources.MeshInfo meshInfo in sceneResources.MeshResources.GetAllMeshes())
            {
                if (meshInfo.FilePath != null && meshInfo.Mesh == null)
                {
                    sceneResources.MeshResources.LoadModel(meshInfo);
                }
            }

            sceneResources.TextureResources.GenerateAtlas();

            return sceneResources;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(SceneResources);
        }
    }
}
