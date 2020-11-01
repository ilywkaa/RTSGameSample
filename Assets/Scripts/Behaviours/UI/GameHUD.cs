using Leopotam.Ecs;
using System;
using UniRx;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHud : Screen
{
    public Button PlayButton;
    public Button ExitButton;
    public Text Gold;


    void Awake()
    {
        PlayButton?.onClick.AddListener(OnPlayButtonClick);
        ExitButton?.onClick.AddListener(OnExitButtonClick);
    }
    

    void Start()
    {
        GameManager.Instance.Game.State.Where(state => state == Game.GameState.Play)
            .Subscribe(_ =>
            {
                PlayButton.interactable = false;
            });
        GameManager.Instance.Game.State.Where(state => state == Game.GameState.Idle)
            .Subscribe(_ =>
            {
                PlayButton.interactable = true;
            });

        GameManager.Instance.Game.Gold.SubscribeToText(Gold);
    }
    
    void OnDestroy()
    {
        PlayButton?.onClick.RemoveListener(OnPlayButtonClick);
    }


    private void OnPlayButtonClick()
    {
        GameManager.Instance.World.NewEntity().Get<PlayModeEvent>();
    }
    private void OnExitButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
