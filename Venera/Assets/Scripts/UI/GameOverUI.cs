using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Venera
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private Button retryButton;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button quitButton;

        private void Awake() {
            retryButton.onClick.AddListener(() => {
                Loader.Load(Loader.Scene.GameScene);
            });
            mainMenuButton.onClick.AddListener(() => {
                Loader.Load(Loader.Scene.MainMenuScene);
            });
            quitButton.onClick.AddListener(() => {
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
