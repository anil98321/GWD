using GlidewellDentalService.Model;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace GlidewellDentalService.Utility
{
    public static class JsonFileUtils
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions() { IgnoreNullValues = true };
      
        public static void SimpleWrite(object obj, string fileName)
        {
            var jsonString = JsonSerializer.Serialize(obj, _options);

            File.WriteAllText(fileName, jsonString);
        }

        // Native/JsonFileUtils.cs
        public static void PrettyWrite(object obj, string fileName)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                IgnoreNullValues = true
            };
            var jsonString = JsonSerializer.Serialize(obj, options);

            File.WriteAllText(fileName, jsonString);
        }

        //Read Json File
        public static List<T> PrettyRead<T>(string fileName)
        {
            var jsonString = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<T>>(jsonString);
        }
    }

}
