using System.Reflection;
using System.Text.Json;

namespace Utils.HelperFuncs;

public class ObjectMapper
{
    private static string CapitalizeFirstCharacter(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input; // Return unchanged if the string is empty or null
        }

        // Convert the first character to uppercase and concatenate the rest of the string
        return char.ToUpper(input[0]) + input[1..];
    }
    
    private static PropertyInfo? HasProperty<T>(string key)
    {
        return typeof(T).GetProperty(CapitalizeFirstCharacter(key));
    }

    private static void MappingKeyValuePairToObject<T>(PropertyInfo property, T obj, JsonElement? value)
    {
        var propertyType = property.PropertyType;

        if (value.HasValue)
        {
            var val = JsonSerializer.Deserialize(value.Value, propertyType);
            property.SetValue(obj, val, null);
        }
        else
        {
            property.SetValue(obj, null, null);
        }
    }

    public static void Mapping<T>(Dictionary<string, dynamic> src, T dest)
    {
        foreach (var key in src.Keys)
        {
            var property = HasProperty<T>(key);
            if (property != null)
            {
                MappingKeyValuePairToObject(property, dest, src[key]);
            }
        }
    }
}