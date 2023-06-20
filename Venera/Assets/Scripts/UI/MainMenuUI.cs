using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Venera
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _creditsButton;
        [SerializeField] private Button _quitButton;

        private void Awake()
        {
            Time.timeScale = 1f;
            _playButton.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.GameScene);
            });

            _quitButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }

    }
}
