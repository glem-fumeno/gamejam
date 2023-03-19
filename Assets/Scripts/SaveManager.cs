using System.IO;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

public static class SaveManager
{
    private static readonly string _rootPath = Application.persistentDataPath;

    private static bool Exists(string path)
    {
        return File.Exists(Path.Combine(_rootPath, path));
    }

    [CanBeNull]
    public static T Load<T>(string filePath)
    {
        if (!Exists(filePath))
        {
            return default;
        }

        var json = File.ReadAllText(Path.Combine(_rootPath, filePath));
        return JsonConvert.DeserializeObject<T>(json);
    }

    public static void Save<T>(string filePath, T data)
    {
        var json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(Path.Combine(_rootPath, filePath), json);
    }
}
