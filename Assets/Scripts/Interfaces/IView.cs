
using Leopotam.Ecs;
using UnityEngine;

public interface IView
{
    GameObject ViewObject { get; }
    Transform Transform { get; }
    void InitializeView(Color color, ref EcsEntity entity);
}