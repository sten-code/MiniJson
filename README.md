# MiniJson
A really small json parser and writer for C#

You can load json into an object like this:
```
string json = @"{
  ""TestInt"": 1,
  ""TestString"": ""Test"",
  ""TestBool"": true,
  ""TestList"": [
    ""test1"",
    ""test2""
  ],
  ""TestDictionary"": {
    ""Test"": ""Test""
  },
  ""TestFloat"": 1.1,
  ""TestDouble"": 1.1,
}";

Test test = json.FromJson<Dictionary<string, object>>();

public class Test
{
    public int TestInt { get; set; } = 1;
    public string TestString { get; set; } = "Test";
    public bool TestBool { get; set; } = true;
    public List<string> TestList { get; set; } = new List<string> { "test1", "test2" };
    public Dictionary<string, object> TestDictionary { get; set; } = new Dictionary<string, object> { { "Test", "Test" } };
    public float TestFloat { get; set; } = 1.1f;
    public double TestDouble { get; set; } = 1.1;
}
```

Or you can load it into a dictionary like this:
```
string json = @"{
  ""TestInt"": 1,
  ""TestString"": ""Test"",
  ""TestBool"": true,
  ""TestList"": [
    ""test1"",
    ""test2""
  ],
  ""TestDictionary"": {
    ""Test"": ""Test""
  },
  ""TestFloat"": 1.1,
  ""TestDouble"": 1.1,
}";

Dictionary<string, object> test = json.FromJson<Dictionary<string, object>>();
```
