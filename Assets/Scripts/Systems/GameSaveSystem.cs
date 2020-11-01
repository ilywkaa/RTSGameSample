using System.Collections.Generic;
using Leopotam.Ecs;

public class GameSaveSystem : IEcsDestroySystem
{
    private readonly Game _game;
    private readonly ISaveService _saveService = null;
    private readonly EcsFilter<AiCharacterComponent> _charactersFilter = null;
    

    public void Destroy()
    {
        var infos = new List<Game.CharacterInfo>();
        foreach (var idx in _charactersFilter)
        {
            ref var character = ref _charactersFilter.Get1(idx);
            infos.Add(new Game.CharacterInfo(character.Type, character.Level));
        }

        var options = new Game.GameOptions(_game.Gold.Value, infos.ToArray());

        _saveService.Save(options);
    }
}