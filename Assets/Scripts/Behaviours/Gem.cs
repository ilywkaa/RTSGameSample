using Leopotam.Ecs;
using UnityEngine;

public class Gem : MonoBehaviour, IView
{
    [SerializeField] private LayerMask _triggerLayer;

    private Material _material;
    private EcsEntity _entity;

    void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        int triggerGoLayer = 1 << other.gameObject.layer;
        if (triggerGoLayer == _triggerLayer.value)
        {
            var triggerColor = other.gameObject.GetComponent<Character>().Material.color;
            if (triggerColor == _material.color)
            {
                var entity = GameManager.Instance.World.NewEntity();
                entity.Replace(new GemComponent()
                {
                    Value = GameManager.Instance.Configuration.GemValue
                });
                Destroy(gameObject);
            }
        }
    }


    public GameObject ViewObject => gameObject;

    public Transform Transform => transform;

    public void InitializeView(Color color, ref EcsEntity entity)
    {
        _material.color = color;
        _entity = entity;
    }
}