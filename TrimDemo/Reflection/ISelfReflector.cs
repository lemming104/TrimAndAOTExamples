namespace TrimDemo.Reflection
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;

    // These classes show a way to do bounded reflection on well-defined types, for purposes like getting all of the values
    // out of a "settings" object, or recursively setting its values from a file.

    // This interface just ensures we've got something we can call without having to unwrap a bunch of generic types.
    public interface ISelfReflector
    {
        List<string> GetFieldValuesRecursively();
    }

    // All of our implementation classes will inherit from this generic class.  Since the type T is known at compile time,
    // and all reflection is done with respect to T, this code is both trimmable and AOT-friendly.
    public abstract class SelfReflector<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)] T> : ISelfReflector
        where T : SelfReflector<T>
    {
        public List<string> GetFieldValuesRecursively()
        {
            List<string> fieldValues = new();
            FieldInfo[] fields = typeof(T).GetFields();

            foreach (FieldInfo f in fields)
            {
                object? value = f.GetValue(this);
                if (value is ISelfReflector)
                {
                    // Recursive step
                    fieldValues.AddRange((value as ISelfReflector)!.GetFieldValuesRecursively());
                }
                else if (value != null)
                {
                    fieldValues.Add(value?.ToString() ?? "Unknown value");
                }
            }

            return fieldValues;
        }
    }
}
