using UnityEngine;

namespace TowerDefence
{
    [CreateAssetMenu(fileName = "GameConfiguration", menuName = "ScriptableObjects/GameConfiguration", order = 1)]
    public class GameConfiguration : ScriptableObject
    {
        [Header("Player")]
        [Min(1f)]
        [SerializeField]
        private int _upgradeCharacterValue = 3;
        

        [Space] 
        [Header("Settings")] 
        [SerializeField]
        private string _levelDataFolder = "Levels";
        [SerializeField]
        private string _saveGameDataFilename = "options.data";


        [Space] [Header("GamePlay")] [SerializeField]
        private int _gemValue = 5;

        [SerializeField] private Color[] _aiColors;

        [Space]
        [Header("Prefabs")]
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _obstaclePrefab;
        [SerializeField] private GameObject _gemPrefab;


        public GameObject Player => _playerPrefab;
        public GameObject Obstacle => _obstaclePrefab;
        public GameObject Gem => _gemPrefab;
        public string LevelFolder => _levelDataFolder;
        public Color[] PlayerTypes => _aiColors;
        public int GemValue => _gemValue;
        public int UpgradeCharacterValue => _upgradeCharacterValue;
        public string SaveGameDataFilename => _saveGameDataFilename;
    }
}