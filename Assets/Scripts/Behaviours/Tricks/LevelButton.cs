    using UnityEngine;
    using UnityEngine.UI;

    public class LevelButton: MonoBehaviour
    {
        [SerializeField] private Text _text;


        private Button _button;
        private string _levelName;


        void Awake()
        {
            _button = gameObject.GetComponent<Button>();
            _button.onClick.AddListener(delegate
            {
                PlayerPrefs.SetString("Level", _levelName);
            });
        }

        public void SetText(string text)
        {
            _levelName = text;
            _text.text = text;
        }

        public Button Button => _button;
    }