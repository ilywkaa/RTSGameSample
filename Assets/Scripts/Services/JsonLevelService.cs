using System.IO;
using Models;
using UnityEngine;

public class JsonLevelService : ILevelService
{
    private readonly string _filePath;

    public JsonLevelService(string filePath)
    {
        _filePath = filePath;
    }

    public Level Load(string levelName)
    {
        var path = Path.Combine(_filePath, levelName);

        FileInfo file = new FileInfo(Path.Combine(Application.dataPath, "Resources", _filePath, levelName + ".json"));
        if (!file.Exists)
        {
            Debug.LogError($"File {file} doesn't exist.");
            return null;
        }

        var json = Resources.Load<TextAsset>(path);
        return JsonUtility.FromJson<Level>(json.text);
    }
}