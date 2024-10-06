namespace TrimDemo.Serialization
{
    using System.Text.Json.Serialization;

    // This serialization context allows the compiler to generate code for types it expects to see when
    // serializing our example class.  This is necessary for trimmed and AOT builds.

    [JsonSourceGenerationOptions(
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault,
    PropertyNameCaseInsensitive = true,
    IncludeFields = true)]
    [JsonSerializable(typeof(JsonSerializableClass), TypeInfoPropertyName = "JsonSerializableClass")]

    public partial class JsonSerializableClass_SerializationContext : JsonSerializerContext
    {
    }
}
