using Leopotam.Ecs;
using Models;
using Pathfinding;
using UnityEngine;
using UnityEngine.EventSystems;

public class Character: AIPath, IView, IPointerClickHandler, IPointerUpHandler, INavAgent
{
    [SerializeField] private LayerMask _groundLayerMask;


    [HideInInspector]
    public Material Material;
    

    private AIPath _ai;
    private Seeker _seeker;
    private Level _levelData;
    private Game _gameState;
    private IUpgradeService _upgradeService;
    private EcsEntity _entity;
    private Vector3 _target;
    private bool _isTargeted;


    private new void Awake()
    {
        base.Awake();

        Material = GetComponentInChildren<Renderer>().material;
        _seeker = GetComponent<Seeker>();
        _levelData = GameManager.Instance.LevelData;
        _gameState = GameManager.Instance.Game;
        _upgradeService = GameManager.Instance.UpgradeService;
    }
    

    public override void OnTargetReached()
    {
        base.OnTargetReached();
        _entity.Del<IsMovingComponent>();
    }


    public GameObject GameObject => gameObject;

    public Transform Transform => transform;

    public void InitializeView(Color color, ref EcsEntity entity)
    {
        Material.color = color;
        _entity = entity;
        Upgrade();
        GetComponent<CharacterController>().enabled = true;
    }
    

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_entity.Has<AiCharacterComponent>())
        {
            Debug.LogError($"Entity of character {gameObject.name} doesn't have an {nameof(AiCharacterComponent)}.");
            return;
        }

        var character = _entity.Get<AiCharacterComponent>();
        var upgradeEvent = new UpgradeCharacterEvent()
        {
            Type = character.Type,
            NewLevelValue = _upgradeService.UpgradeCharacter(character.Level)
        };

        GameManager.Instance.World.NewEntity()
            .Replace(upgradeEvent);
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
            _entity.Get<IsMovingComponent>();
        }
    }


    public void Upgrade()
    {
        maxSpeed = _entity.Get<AiCharacterComponent>().Level;
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