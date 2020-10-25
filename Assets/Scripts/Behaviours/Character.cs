using Leopotam.Ecs;
using Models;
using Pathfinding;
using UnityEngine;
using UnityEngine.EventSystems;
public class Character: MonoBehaviour, IView, IPointerClickHandler, IPointerUpHandler, INavAgent
{
    [HideInInspector]
    public Material Material;


    [SerializeField] 
    [Min(1f)]
    private float _speedDelta = 3;
    [SerializeField] 
    [Min(1f)]
    private float _maxSpeed = 15;
    [SerializeField] private LayerMask _groundLayerMask;


    private AIPath _ai;
    private Seeker _seeker;
    private Level _levelData;
    private EcsEntity _entity;
    private Vector3 _target;
    private bool _isTargeted;


    void Awake()
    {
        Material = GetComponentInChildren<MeshRenderer>().material;
        _ai = GetComponent<AIPath>();
        _seeker = GetComponent<Seeker>();
        _levelData = GameManager.Instance.LevelData();
    }


    public GameObject ViewObject => gameObject;

    public Transform Transform => transform;

    public void InitializeView(Color color, ref EcsEntity entity)
    {
        Material.color = color;
        _entity = entity;
    }
    

    public void OnPointerClick(PointerEventData eventData)
    {
        UpgradeCharacter();
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        int characterLayer = 1 << gameObject.layer;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(eventData.position), out hit, Mathf.Infinity,
            characterLayer))
        {
            return;
        }

        if (Physics.Raycast(Camera.main.ScreenPointToRay(eventData.position), out hit, Mathf.Infinity, _groundLayerMask.value))
        {
            bool isField = InsideField(hit.point.x, hit.point.z);
            if (isField)
            {
                _isTargeted = true;
                _target = hit.point;
            }
        }
    }


    public void StartPath()
    {
        if (_isTargeted)
        {
            _isTargeted = false;
            _seeker.StartPath(transform.position, _target);
        }
    }


    private void UpgradeCharacter()
    {
        float speed = _ai.maxSpeed + _speedDelta;
        _ai.maxSpeed = speed > +_maxSpeed ? _maxSpeed : speed;
    }

    private bool InsideField(float x, float z)
    {
        int xMin = -_levelData.Field.Width / 2;
        int xMax = _levelData.Field.Width / 2;
        int zMin = -_levelData.Field.Depth / 2;
        int zMax = _levelData.Field.Depth / 2;

        return x < xMax && x > xMin &&
               z < zMax && z > zMin;
    }
}