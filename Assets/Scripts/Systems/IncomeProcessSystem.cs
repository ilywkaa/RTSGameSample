using Leopotam.Ecs;

public class IncomeProcessSystem : IEcsRunSystem
{
    readonly Game _gameState;
    readonly EcsFilter<IncomeEvent> _incomeFilter = null;


    public void Run()
    {
        foreach (var idx in _incomeFilter)
        {
            ref var income = ref _incomeFilter.Get1(idx);
            _gameState.Gold.Value += income.Value;
        }
    }
}