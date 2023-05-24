using TMPro;
using UnityEngine;

namespace Venera
{
    public class GameStartCountdownUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countdownText;

        private void Start() {
            GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        }

        private void GameManager_OnStateChanged(object sender, System.EventArgs e){
            if(GameManager.Instance.IsCountdownToStartActive()){
                Show();
            } else {
                Hide();
            }
        }

        private void Update() {
            countdownText.text = GameManager.Instance.CountdownToStartTimer.ToString("#");
        }

        private void Show(){
            gameObject.SetActive(true);
        }

        private void Hide(){
            gameObject.SetActive(false);
        }

    }
}