using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using Models;
using TowerDefence;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public EcsWorld World;
    public GameConfiguration Configuration;

    [SerializeField] private EcsUiEmitter _uiEmitter = null;
    [SerializeField] private GameObject _root;

    private ILevelService _levelService;

    private Level _currentLevel;

    private EcsSystems _systems;
    

    void Start()
    {
        ConfigureServices();
        LevelInitialize();
        
        World = new EcsWorld();
        _systems = new EcsSystems(World)
            //InitSystems
            .Add(new LevelSetupSystem())
            //RunSystems
            .Add(new UiInputSystem())
            .Add(new PlaySystem())

            .InjectUi(_uiEmitter)
            .Inject(_currentLevel)
            .Inject(Configuration)
            .Inject(_root);

#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (World);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
        _systems.Init();
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
        //TODO: level managing
        _currentLevel = _levelService.Load("LevelExample");
    }


    private void ConfigureServices()
    {
        _levelService = new JsonLevelService(Configuration.LevelFolder);
    }


    public Level LevelData() => _currentLevel;
}
