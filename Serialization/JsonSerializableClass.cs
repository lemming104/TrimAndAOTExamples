namespace TrimDemo.Serialization
{
    public class JsonSerializableClass
    {
        public string? StringField;

        public int IntField;

        public override string ToString()
        {
            return $"StringField: {StringField}, IntField: {IntField}";
        }
    }
}
