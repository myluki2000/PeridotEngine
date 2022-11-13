using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PeridotEngine.ECS.Components;
using PeridotWindows.ECS;
using PeridotWindows.ECS.Components;

namespace PeridotEngine.IO.JsonConverters
{
    public class ArchetypeJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value == null) return;

            Archetype archetype = (Archetype)value;

            writer.WriteStartObject();

            writer.WritePropertyName("ComponentTypes");
            writer.WriteStartArray();
            foreach (Type componentType in archetype.ComponentTypes)
            {
                writer.WriteValue(componentType.FullName);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("Names");
            serializer.Serialize(writer, archetype.Names);

            writer.WritePropertyName("Components");
            serializer.Serialize(writer, archetype.Components);

            writer.WriteEndObject();
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            JToken root = JToken.ReadFrom(reader);
            Type[] componentTypes = root["ComponentTypes"]!.Values<string>().Select(x =>
            {
                Type? type = Assembly.GetExecutingAssembly().GetType(x!);

                if (type == null)
                    throw new Exception("Could not find component type with name " + x);

                return type;
            }).ToArray();

            List<string?> names = root["Names"].Values<string?>().ToList();

            List<IList> components = new();

            JArray jComponents = (root["Components"] as JArray)!;

            for (int i = 0; i < jComponents.Count; i++)
            {
                ComponentBase[] componentsOfType = jComponents[i].Children()
                    .Select(x =>
                    {
                        object? component = serializer.Deserialize(x.CreateReader(), componentTypes[i]);
                        return (ComponentBase)component;
                    })
                    .ToArray();

                components.Add(new ArrayList(componentsOfType));
            }


            return new Archetype(componentTypes, names, components);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Archetype);
        }
    }
}
