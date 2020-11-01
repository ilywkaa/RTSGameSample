using Leopotam.Ecs;
using UnityEngine.SceneManagement;

public class EndGameSystem: IEcsRunSystem
{
    private readonly EcsFilter<GemComponent> _gemsFilter = null;
    private readonly int _ind;

    public EndGameSystem(int sceneInd)
    {
        _ind = sceneInd;
    }

    public void Run()
    {
        if (_gemsFilter.IsEmpty())
        {
            SceneManager.LoadScene(_ind);
        }
    }
}