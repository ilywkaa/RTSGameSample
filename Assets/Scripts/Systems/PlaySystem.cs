using Leopotam.Ecs;

public class PlaySystem : IEcsRunSystem
{
    readonly Game _gameState = null;
    readonly EcsFilter<PlayModeEvent> _playGameEventFilter = null;
    readonly EcsFilter<AiCharacterComponent> _charactersFilter = null;

    public void Run()
    {
        if (!_playGameEventFilter.IsEmpty())
        {
            _gameState.State.Value = Game.GameState.Play;
            foreach (var idx in _charactersFilter)
            {
                ref var characterEntity = ref _charactersFilter.GetEntity(idx);
                ref var view = ref characterEntity.Get<ViewComponent>();
                var agent = view.Value.GameObject.GetComponent<INavAgent>();
                agent.StartPath();
            }
        }
    }
}