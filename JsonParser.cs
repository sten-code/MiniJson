namespace JsonLib
{
    public static class JsonParser
    {
        public static T FromJson<T>(this string json)
        {
            return typeof(T).IsGenericType ? (T)ParseValue(json.UnformatJson()).Item2 : (T)ParseObject(typeof(T), json.UnformatJson());
        }
        
        public static string UnformatJson(this string json)
        {
            string unformatted = "";
            for (int i = 0; i < json.Length; i++)
            {
                if (json[i] == '"')
                {
                    unformatted += json[i];
                    for (int i2 = i + 1; i2 < json.Length; i2++)
                    {
                        unformatted += json[i2];
                        if (json[i2] == '\\')
                        {
                            unformatted += json[i2 + 1];
                            i2++;
                        }
                        else if (json[i2] == '"')
                        {
                            i = i2;
                            break;
                        }
                    };
                    continue;
                }
                if (char.IsWhiteSpace(json[i])) continue;
                unformatted += json[i];
            }
            return unformatted;
        }

        public static List<string> Split(string json)
        {
            List<string> splitArray = new List<string>();
            if (json.Length == 2) return splitArray;
            int depth = 0;
            string text = "";
            for (int i = 1; i < json.Length - 1; i++)
            {
                if (json[i] == '{' || json[i] == '[') depth++;
                else if (json[i] == '}' || json[i] == ']') depth--;
                else if ((json[i] == ':' || json[i] == ',') && depth == 0)
                {
                    splitArray.Add(text);
                    text = "";
                    continue;
                } else if (json[i] == '\"')
                {
                    text += json[i];
                    for (int i2 = i + 1; i2 < json.Length; i2++)
                    {
                        text += json[i2];
                        if (json[i2] == '\\')
                        {
                            text += json[i2 + 1];
                            i2++;
                        }
                        else if (json[i2] == '"')
                        {
                            i = i2;
                            break;
                        }
                    };
                    continue;
                }
                text += json[i];
            }
            splitArray.Add(text);
            return splitArray;
        }

        public static (Type, object) ParseValue(string value)
        {
            if (value.Length == 0 || value == "null") return (null, null); // Parse null value
            else if (value[0] == '\"' && value[value.Length - 1] == '\"') return (typeof(string), value.Trim('"').Replace("\\", "")); // Parse string value
            else if (value == "true" || value == "false") return (typeof(bool), value == "true" ? true : false); // Parse boolean value
            else if (char.IsDigit(value[0]) || value[0] == '-') return value.Contains(".") ? (typeof(double), double.Parse(value)) : (typeof(int), int.Parse(value)); // Parse numbers
            else if (value[0] == '[' && value[value.Length - 1] == ']') // Parse list
            {
                List<object> list = new List<object>();
                foreach (string item in Split(value)) list.Add(ParseValue(item).Item2);
                return (typeof(List<object>), list);
            }
            else if (value[0] == '{' && value[value.Length - 1] == '}') // Parse dictionary
            {
                List<string> items = Split(value);
                Dictionary<string, object> dict = new Dictionary<string, object>();
                for (int i = 0; i < items.Count; i += 2) dict.Add((string)ParseValue(items[i]).Item2, ParseValue(items[i + 1]).Item2);
                return (typeof(Dictionary<string, object>), dict);
            }
            return (null, null);
        }
        
        public static object ParseObject(Type type, string json)
        {
            object obj = Activator.CreateInstance(type);
            foreach (KeyValuePair<string, object> item in (Dictionary<string, object>)ParseValue(json).Item2) 
                type.GetProperty(item.Key).SetValue(obj, Convert.ChangeType(item.Value, type.GetProperty(item.Key).PropertyType));
            return obj;
        }
    }
}