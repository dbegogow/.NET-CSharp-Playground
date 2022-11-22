using System.Text;

var sb = new StringBuilder();

var xmlPrologue = """<?xml version="1.0" encoding="UTF-8"?>""";
var xmlPrologue1 = """"<?xml version="""1.0" encoding="UTF-8"?>"""";
var xmlPrologue2 = $"""<?xml version="{1.0m}" encoding="UTF-8"?>""";

var name = "Dzhulio Begogov";

var jsonText = $$""""
        {
            "name": "{{name}}"
        }
        """";

sb.AppendLine(xmlPrologue);
sb.AppendLine(xmlPrologue1);
sb.AppendLine(xmlPrologue2);
sb.AppendLine(jsonText);

Console.WriteLine(sb.ToString());