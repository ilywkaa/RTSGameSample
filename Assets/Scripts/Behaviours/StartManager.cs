using System.IO;
using System.Linq;
using TowerDefence;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager: MonoBehaviour
{
    [SerializeField] private UiStart _uiScreen;
    [SerializeField] private GameConfiguration _configuration;


    void Awake()
    {
        string levelsFolder = Path.Combine(Application.dataPath, "Resources", _configuration.LevelFolder);
        DirectoryInfo directory = new DirectoryInfo(levelsFolder);
        var levels = directory.GetFiles()
            .Where(file => file.Extension == ".json");
        foreach (var fileInfo in levels)
        {
            _uiScreen.AddLevelItem(Path.GetFileNameWithoutExtension(fileInfo.Name));
        }
    }
}