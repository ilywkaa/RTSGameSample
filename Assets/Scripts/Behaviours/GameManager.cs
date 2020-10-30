using System.Collections;
using System.Collections.Generic;
using System.IO;
using Leopotam.Ecs;
using Models;
using Pathfinding;
using TowerDefence;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public EcsWorld World;
    public GameConfiguration Configuration;

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
            .Add(new LevelSetupSystem())
            .Inject(_currentLevel)
            .Inject(Configuration)
            .Inject(_root);

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
