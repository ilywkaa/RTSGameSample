using Leopotam.Ecs;
using Models;
using TowerDefence;
using UnityEngine;

public class LevelSetupSystem: IEcsInitSystem
{
    private readonly Game _game;
    private readonly Level _levelData;
    private readonly GameConfiguration _configuration;
    private readonly GameObject _root;
    private readonly EcsWorld _world;


    public void Init()
    {
        SpawnLevelObjects();
        SetGraphUp();
    }


    private void SetGraphUp()
    {
        if (AstarPath.active == null) throw new System.Exception("There is no AstarPath object in the scene");

        var graph = AstarPath.active.data.gridGraph;

        if (graph == null) throw new System.Exception("The AstarPath object has no GridGraph or LayeredGridGraph");

        graph.SetDimensions(_levelData.Field.Width, _levelData.Field.Depth, graph.nodeSize);
        graph.center = Vector3.zero;

        AstarPath.active.Scan();
    }

    private void SpawnLevelObjects()
    {
        foreach (var characterData in _levelData.Characters)
        {
            var character = GameObject.Instantiate(_configuration.Player, _root.transform);
            Vector3 position = new Vector3(characterData.Coords.X - _levelData.Field.Width / 2, 
                _configuration.Player.transform.position.y, characterData.Coords.Y - _levelData.Field.Depth / 2);
            character.transform.position = position;
            int characterType = characterData.ColorType;
            var entity = _world.NewEntity();
            entity
                .Replace(new AiCharacterComponent()
                {
                    Type = characterType,
                    Level = _game.CharactersLevels[characterType]
                });
            IView view = character.GetComponent<IView>();
            view.InitializeView(_configuration.PlayerTypes[characterData.ColorType - 1], ref entity);
            entity.Replace(new ViewComponent()
            {
                Value = view
            });
        }

        foreach (var obstacleData in _levelData.Obstacles)
        {
            var obstacle = GameObject.Instantiate(_configuration.Obstacle, _root.transform);
            Vector3 position = new Vector3(obstacleData.Coords.X - _levelData.Field.Width / 2,
                _configuration.Obstacle.transform.position.y, obstacleData.Coords.Y - _levelData.Field.Depth / 2);
            obstacle.transform.position = position;
        }

        foreach (var gemData in _levelData.Gems)
        {
            var gem = GameObject.Instantiate(_configuration.Gem, _root.transform);
            Vector3 position = new Vector3(gemData.Coords.X - _levelData.Field.Width / 2,
                _configuration.Gem.transform.position.y, gemData.Coords.Y - _levelData.Field.Depth / 2);
            gem.transform.position = position;
            var entity = _world.NewEntity();
            entity
                .Replace(new GemComponent() { Value = _configuration.GemValue });
            IView view = gem.GetComponent<IView>();
            view.InitializeView(_configuration.PlayerTypes[gemData.ColorType - 1], ref entity);
            entity
                .Replace(new ViewComponent()
                {
                    Value = view
                });
        }
    }
}