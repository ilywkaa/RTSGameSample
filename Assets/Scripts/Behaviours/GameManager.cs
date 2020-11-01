using Leopotam.Ecs;
using Models;
using TowerDefence;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public EcsWorld World;

    [SerializeField] private GameConfiguration _configuration;
    [SerializeField] private GameObject _root;
    [SerializeField] private Ui _ui;

    private Game _game; 
    private ILevelService _levelService;
    private IUpgradeService _upgradeService;
    private ISaveService _saveService;

    private Level _currentLevel;

    private EcsSystems _systems;


    void Awake()
    {
        ConfigureServices();
        LevelInitialize();
    }

    void Start()
    {
        World = new EcsWorld();
        _systems = new EcsSystems(World)
            //InitSystems
            .Add(new LevelSetupSystem())
            //RunSystems
            .Add(new PlaySystem())
            .Add(new GameStateProcessSystem())
            .Add(new UpgradeSystem())
            .Add(new IncomeProcessSystem())
            .Add(new GameStateProcessSystem())
            .Add(new GameSaveSystem())
            .Add(new EndGameSystem(0)) //TODO: manage scene idx in convenient readable form

            .Inject(_currentLevel)
            .Inject(_root)
            .Inject(_configuration)
            .Inject(_game)
            .Inject(_saveService)
            
            .OneFrame<PlayModeEvent>()
            .OneFrame<IncomeEvent>()
            .OneFrame<UpgradeCharacterEvent>();


#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (World);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
        _systems?.Init();
    }

    void Update()
    {
        _systems?.Run();
    }

    void OnDestroy()
    {
        if (_systems != null)
        {
            _systems.Destroy();
            _systems = null;
            World.Destroy();
            World = null;
        }
    }


    private void LevelInitialize()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            string level = PlayerPrefs.GetString("Level");
            _currentLevel = _levelService.Load(level);
            _game = new Game(_saveService);
        }
    }
    
    private void ConfigureServices()
    {
        _levelService = new JsonLevelService(_configuration.LevelFolder);
        _upgradeService = new UpgradeService(_configuration.UpgradeCharacterValue);
        _saveService = new BinarySaveService(_configuration.SaveGameDataFilename);
    }


    public Game Game => _game;
    public Level LevelData => _currentLevel;
    public IUpgradeService UpgradeService => _upgradeService;
    public GameConfiguration Configuration => _configuration;
}
