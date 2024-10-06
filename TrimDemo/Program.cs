namespace TrimDemo
{
    using Enums;
    using Reflection;
    using Serialization;
    using System;
    using System.Collections.Generic;
    using System.Text.Json;

    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hello world!");

            // Demonstrate Json serialization and deserialization in a trim-friendly pattern:
            JsonSerializableClass objectToSerialize = new JsonSerializableClass()
            {
                IntField = 3,
                StringField = "Hi!"
            };

            // Note that we are using a serializer context here, because the compiler otherwise does not know
            // what types are required.
            string serialized = JsonSerializer.Serialize(objectToSerialize, typeof(JsonSerializableClass), JsonSerializableClass_SerializationContext.Default);
            Console.WriteLine($"Object serialized using trim-friendly methods: {serialized}");

            JsonSerializableClass? deserialized = JsonSerializer.Deserialize(serialized, typeof(JsonSerializableClass), JsonSerializableClass_SerializationContext.Default) as JsonSerializableClass;
            Console.WriteLine($"Object deserialized again: {deserialized}");


            // Demonstrate listing enum values in a trim-friendly pattern:

            // Avoid using Enum.GetValues(typeof(Enum1)) -- this causes a trim warning.
            Enum1[] values = Enum.GetValues<Enum1>();
            Console.WriteLine($"Enum values: {string.Join(',', values)}");


            // Demonstrate recursive enumeration of fields on nested types in a trim-friendly pattern:

            SelfReflectingClass1 c1 = new SelfReflectingClass1()
            {
                IntValue = 45,
                StringValue = "Another example",
                RecursiveMember = new SelfReflectingClass1()
                {
                    IntValue = 3,
                    StringValue = "Recursive example"
                },
                SelfReflectingClass2Member = new SelfReflectingClass2()
                {
                    DoubleValue = 33.0
                }
            };

            List<string> valuesAsStrings = c1.GetFieldValuesRecursively();
            Console.WriteLine($"Field values: {string.Join(',', valuesAsStrings)}");
        }
    }
}
