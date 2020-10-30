using System;
using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;
using Leopotam.Ecs.Ui.Systems;
using UnityEngine;

public class UiInputSystem : IEcsRunSystem
{
    readonly EcsWorld _world = null; 
    readonly EcsUiEmitter _emitter = null;
    readonly EcsFilter<EcsUiClickEvent> _clickEvents = null;

    public void Run()
    {
        foreach (var idx in _clickEvents)
        {
            ref EcsUiClickEvent data = ref _clickEvents.Get1(idx);
            if (data.WidgetName == "Play")
            {
                var entity = _world.NewEntity();
                entity.Replace(new PlayModeEvent());
            }
        }
    }
}
