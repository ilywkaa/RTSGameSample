
using Leopotam.Ecs;
using UnityEngine;

public interface IView
{
    GameObject GameObject { get; }
    Transform Transform { get; }
    void InitializeView(Color color, ref EcsEntity entity);
}