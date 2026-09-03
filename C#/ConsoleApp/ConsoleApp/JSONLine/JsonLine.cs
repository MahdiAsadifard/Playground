using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ConsoleApp.JSONLine
{
    public class JsonLine
    {
        private static readonly string filePath = Path.Combine(AppContext.BaseDirectory, "JSONLine\\jsonl.jsonl");
        public static void Build()
        {
            var r = ReadJsonLines(filePath);
            foreach (var line in r)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("----");
            var converted = ConvertJsonlToJson();
            Console.WriteLine(converted);
        }

        public static void WriteJsonLines(string filePath, List<string> jsonLines)
        {
            try
            {
                using (var writer = new System.IO.StreamWriter(filePath))
                {
                    foreach (var line in jsonLines)
                    {
                        writer.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing to the file: {ex.Message}");
            }
        }

        public static IEnumerable<string> ReadJsonLines(string filePath)
        {
            using (var reader = new System.IO.StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }

        }

        public static string ConvertJsonlToJson()
        {
            var fileStream = ReadJsonLines(filePath);

            var jsonLines = new List<object>();

            foreach (var line in fileStream)
            {
                using var doc = JsonDocument.Parse(line);
                var serialized = JsonSerializer.Deserialize<object>(doc.RootElement.GetRawText());
                jsonLines.Add(serialized!);
            }

            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var jsonArraystrringObject = JsonSerializer.Serialize(jsonLines, jsonOptions);
            return jsonArraystrringObject;
        }

    }
}
