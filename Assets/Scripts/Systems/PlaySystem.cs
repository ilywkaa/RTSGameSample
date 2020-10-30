using Leopotam.Ecs;

public class PlaySystem : IEcsRunSystem
{
    readonly EcsFilter<PlayModeEvent> _playGameEventFilter = null;
    readonly EcsFilter<AICharacterComponent> _charactersFilter = null;

    public void Run()
    {
        if (!_playGameEventFilter.IsEmpty())
        {
            foreach (var idx in _charactersFilter)
            {
                ref var characterEntity = ref _charactersFilter.GetEntity(idx);
                ref var character = ref characterEntity.Get<AICharacterComponent>();
                ref var view = ref characterEntity.Get<ViewComponent>();
                var agent = view.Value.GameObject.GetComponent<INavAgent>();
                agent.StartPath();
            }
        }
    }
}