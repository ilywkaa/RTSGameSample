using Leopotam.Ecs;

public class UpgradeSystem : IEcsRunSystem
{
    readonly EcsFilter<UpgradeCharacterEvent> _upgradeEventFilter = null;
    readonly EcsFilter<AiCharacterComponent> _charactersFilter = null;

    public void Run()
    {
        foreach (var idx in _upgradeEventFilter)
        {
            ref var upgradeCharacter = ref _upgradeEventFilter.Get1(idx);

            if (_charactersFilter.IsEmpty())
            {
                return;
            }

            foreach (var idx1 in _charactersFilter)
            {
                ref var character = ref _charactersFilter.Get1(idx1);
                if (character.Type == upgradeCharacter.Type)
                {
                    character.Level = upgradeCharacter.NewLevelValue;
                    var go = _charactersFilter.GetEntity(idx1).Get<ViewComponent>().Value.GameObject;
                    go.GetComponent<INavAgent>().Upgrade();
                }
            }
        }
    }
}