using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiStart : MonoBehaviour
{
    [SerializeField] private GameObject _scrollViewContent;
    [SerializeField] private GameObject _levelButton;


    void Start()
    {
        foreach (var levelButton in _scrollViewContent.GetComponentsInChildren<LevelButton>())
        {
            levelButton.Button.onClick.AddListener(delegate
            {
                SceneManager.LoadScene(1); //TODO: Manage index scene dependency
            });
        }
    }


    public void AddLevelItem(string levelName)
    {
        var item = GameObject.Instantiate(_levelButton, _scrollViewContent.transform);
        item.GetComponent<LevelButton>()?.SetText(levelName);
    }
}