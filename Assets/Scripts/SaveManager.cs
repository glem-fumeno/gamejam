using System.IO;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

public class SaveManager
{
    private static readonly string RootPath = Application.persistentDataPath;

    private static bool Exists(string path)
    {
        return File.Exists(Path.Combine(RootPath, path));
    }

    [CanBeNull]
    public static T Load<T>(string filePath)
    {
        if (!Exists(filePath))
        {
            return default(T);
        }

        var json = File.ReadAllText(Path.Combine(RootPath, filePath));
        return JsonConvert.DeserializeObject<T>(json);
    }

    public static void Save<T>(string filePath, T data)
    {
        var json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(Path.Combine(RootPath, filePath), json);
    }
}
