using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinarySaveService : ISaveService
{
    private readonly string _filePath;

    public BinarySaveService(string path)
    {
        _filePath = Path.Combine(Application.persistentDataPath, path);
    }

    public void Save(Game.GameOptions options)
    {
        using (FileStream fs = new FileStream(_filePath, FileMode.OpenOrCreate))
        {
            new BinaryFormatter().Serialize(fs, options);
        }
    }

    public Game.GameOptions Load()
    {
        Game.GameOptions options = null;
        if (File.Exists(_filePath))
        {
            using (FileStream file = File.Open(_filePath, FileMode.Open))
            {
                object loadedData = new BinaryFormatter().Deserialize(file);
                options = (Game.GameOptions)loadedData;
            }
        }
        return options;
    }
}