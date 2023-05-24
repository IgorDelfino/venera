using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Venera
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button optionsButton;
        [SerializeField] private Button creditsButton;
        [SerializeField] private Button quitButton;

        private void Awake()
        {
            Time.timeScale = 1f;
            playButton.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.GameScene);
            });

            quitButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }

    }
}
