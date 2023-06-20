using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Venera
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _quitButton;

        private void Awake() {
            _retryButton.onClick.AddListener(() => {
                Loader.Load(Loader.Scene.GameScene);
            });
            _mainMenuButton.onClick.AddListener(() => {
                Loader.Load(Loader.Scene.MainMenuScene);
            });
            _quitButton.onClick.AddListener(() => {
                Application.Quit();
            });
        }
        private void Start() {
            GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

            Hide();
        }

        private void GameManager_OnStateChanged(object sender, System.EventArgs e){
            if(GameManager.Instance.IsGameOver()){
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
