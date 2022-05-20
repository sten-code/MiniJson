# MiniJson
A really small json parser and writer for C#

Loading json into object
```csharp
string json = @"{
  ""TestInt"": 1,
  ""TestString"": ""Test"",
  ""TestBool"": true,
  ""TestList"": [
    ""test1"",
    ""test2""
  ],
  ""TestDictionary"": {
    ""test1"": ""test2""
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
    public Dictionary<string, object> TestDictionary { get; set; } = new Dictionary<string, object> { { "test1", "test2" } };
    public float TestFloat { get; set; } = 1.1f;
    public double TestDouble { get; set; } = 1.1;
}
```

Loading json into a dictionary
```csharp
string json = @"{
  ""TestInt"": 1,
  ""TestString"": ""Test"",
  ""TestBool"": true,
  ""TestList"": [
    ""test1"",
    ""test2""
  ],
  ""TestDictionary"": {
    ""test1"": ""test2""
  },
  ""TestFloat"": 1.1,
  ""TestDouble"": 1.1,
}";

Dictionary<string, object> test = json.FromJson<Dictionary<string, object>>();
```

Writing to json from an object
```csharp
Test test = new Test();
string json = test.ToJson();

public class Test
{
    public int TestInt { get; set; } = 1;
    public string TestString { get; set; } = "Test";
    public bool TestBool { get; set; } = true;
    public List<string> TestList { get; set; } = new List<string> { "test1", "test2" };
    public Dictionary<string, object> TestDictionary { get; set; } = new Dictionary<string, object> { { "test1", "test2" } };
    public float TestFloat { get; set; } = 1.1f;
    public double TestDouble { get; set; } = 1.1;
}
```

Writing to json from a dictionary
```csharp
Dictionary<string, object> test = new Dictionary<string, object>
{
    { "TestInt", 1 },
    { "TestString", "Test" },
    { "TestBool", true },
    { "TestList", new List<string> { "test1", "test2" } },
    { "TestDictionary", new Dictionary<string, object> { { "test1", "test2" } } },
    { "TestFloat", 1.1 },
    { "TestDouble", 1.1 }
};

string json = test.ToJson();
```

Unformatting json
```csharp
string json = @"{
  ""TestInt"": 1,
  ""TestString"": ""Test"",
  ""TestBool"": true,
  ""TestList"": [
    ""test1"",
    ""test2""
  ],
  ""TestDictionary"": {
    ""test1"": ""test2""
  },
  ""TestFloat"": 1.1,
  ""TestDouble"": 1.1,
}";

string unformatted = json.UnformatJson();
```

Formatting json
```csharp
string json = "{""TestInt"":1,""TestString"":""Test"",""TestBool"":true,""TestList"":[""test1"",""test2""],""TestDictionary"":{""test1"":""test2""},""TestFloat"":1.1,""TestDouble"":1.1}";

string formatted = json.FormatJson();
```
