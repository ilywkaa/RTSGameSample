using Leopotam.Ecs;

public class GameStateProcessSystem : IEcsRunSystem
{
    readonly Game _gameState = null;
    readonly EcsFilter<AiCharacterComponent, IsMovingComponent> _movingFilter = null;


    public void Run()
    {
        if (_movingFilter.IsEmpty())
        {
            _gameState.State.Value = Game.GameState.Idle;
        }
    }
}