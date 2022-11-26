﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PeridotEngine.ECS.Components;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.ECS;

namespace PeridotEngine.IO.JsonConverters
{
    public class EcsJsonConverter : JsonConverter
    {
        private readonly Scene3D scene;

        public EcsJsonConverter(Scene3D scene)
        {
            this.scene = scene;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value == null) return;

            Ecs ecs = (Ecs)value;

            writer.WriteStartObject();

            writer.WritePropertyName("Archetypes");
            writer.WriteStartArray();

            foreach (Archetype archetype in ecs.Archetypes)
            {
                writer.WriteStartObject();

                writer.WritePropertyName("ComponentTypes");
                writer.WriteStartArray();
                foreach (Type componentType in archetype.ComponentTypes)
                {
                    writer.WriteValue(componentType.FullName);
                }
                writer.WriteEndArray();

                writer.WritePropertyName("Ids");
                serializer.Serialize(writer, archetype.Ids);

                writer.WritePropertyName("Names");
                serializer.Serialize(writer, archetype.Names);

                writer.WritePropertyName("Components");
                serializer.Serialize(writer, archetype.Components);

                writer.WriteEndObject();
            }

            writer.WriteEndArray();
            writer.WriteEndObject();
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            Ecs ecs = new();

            JToken root = JToken.ReadFrom(reader);

            foreach (JToken jArchetype in root["Archetypes"])
            {
                Type[] componentTypes = jArchetype["ComponentTypes"]!.Values<string>().Select(x =>
                {
                    Type? type = Assembly.GetExecutingAssembly().GetType(x!);

                    if (type == null)
                        throw new Exception("Could not find component type with name " + x);

                    return type;
                }).ToArray();

                List<uint> ids = jArchetype["Ids"].Values<uint>().ToList();
                List<string?> names = jArchetype["Names"].Values<string?>().ToList();

                List<IList> components = new();

                JArray jComponents = (jArchetype["Components"] as JArray)!;

                for (int i = 0; i < jComponents.Count; i++)
                {
                    ComponentBase[] componentsOfType = jComponents[i].Children()
                        .Select(x =>
                        {
                            object? component = serializer.Deserialize(x.CreateReader(), componentTypes[i]);
                            component.GetType().GetProperty("Scene").SetValue(component, scene);
                            return (ComponentBase)component;
                        })
                        .ToArray();

                    components.Add(new ArrayList(componentsOfType));
                }

                ecs.Archetypes.Add(new Archetype(ecs, componentTypes, ids, names, components));
            }

            return ecs;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Ecs);
        }
    }
}
