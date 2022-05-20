using System.Collections;
using System.ComponentModel;

namespace JsonLib
{
    public static class JsonWriter
    {
        public static string ToJson(this object obj)
        {
            return AddValue(obj, "");
        }

        private static string AddValue(object value, string json)
        {
            Type type = value.GetType();
            if (type == typeof(string) || type == typeof(char))
            {
                json += "\"";
                foreach (char c in value.ToString())
                {
                    if (c < 32 || c == '\"' || c == '\\')
                    {
                        if ("\"\\\n\r\t\b\f".Contains(c)) json += "\\" + "\"\\nrtbf"["\"\\\n\r\t\b\f".IndexOf(c)];
                        else json += "\\u" + ((int)c).ToString("D4");
                    }
                    else json += c;
                }
                json += "\"";
            }
            else if (type == typeof(byte) || type == typeof(sbyte) || type == typeof(int) || type == typeof(uint) || type == typeof(short) || type == typeof(ushort) ||
                     type == typeof(long) || type == typeof(ulong) || type == typeof(float) || type == typeof(double) || type == typeof(decimal)) json += value.ToString();
            else if (type == typeof(bool)) json += (bool)value ? "true" : "false";
            else if (type == typeof(DateTime) || type.IsEnum) json += $"\"{value}\"";
            else if (value is IList)
            {
                json += "[";
                foreach (object item in (IList)value)
                {
                    if (json[json.Length - 1] != '[') json += ",";
                    json += AddValue(item, "");
                }
                json += "]";
            }
            else if (value is IDictionary)
            {
                if (type.GetGenericArguments()[0] != typeof(string)) return json + "{}";
                json += "{";
                foreach (DictionaryEntry item in (IDictionary)value)
                {
                    if (json[json.Length - 1] != '{') json += ",";
                    json += $"{AddValue(item.Key, "")}:{AddValue(item.Value, "")}";
                }
                json += "}";
            }
            else if (value is null) json += "null";
            else
            {
                json += "{";
                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(value))
                {
                    if (json[json.Length - 1] != '{') json += ",";
                    json += $"\"{prop.Name}\":{AddValue(prop.GetValue(value), "")}";
                }
                json += "}";
            }
            return json;
        }
    }
}