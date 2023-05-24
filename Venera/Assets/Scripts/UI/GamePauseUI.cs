using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Venera {
    public class GamePauseUI : MonoBehaviour
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button quitButton;

        private void Awake() {
            resumeButton.onClick.AddListener(() => {
                GameManager.Instance.TogglePause();
            });
            mainMenuButton.onClick.AddListener(() => {
                Loader.Load(Loader.Scene.MainMenuScene);
            });
            quitButton.onClick.AddListener(() => {
                Application.Quit();
            });
        }
        
        private void Start() {
            GameManager.Instance.OnTogglePause += GameManager_OnTogglePause;

            Hide();
        }

        private void GameManager_OnTogglePause(object sender, System.EventArgs e){
            if(GameManager.Instance.IsGamePaused()){
                Show();
            } else {
                Hide();
            }
        }

        private void Show(){
            gameObject.SetActive(true);
            GameInput.Instance.SetCursorLockState(false);
        }

        private void Hide(){
            gameObject.SetActive(false);
            GameInput.Instance.SetCursorLockState(true);
        }
    }
}