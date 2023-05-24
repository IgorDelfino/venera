using System;
using UnityEngine;

namespace Venera
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public event EventHandler OnStateChanged;
        public event EventHandler OnTogglePause;

        private enum State {
            CountdownToStart,
            GamePlaying,
            GameOver
        }

        private State state;
        private float countdownToStartTimer = 3f;

        public float CountdownToStartTimer { get => countdownToStartTimer; }

        [SerializeField] private DroneIntegrity droneIntegrity;

        private bool isGamePaused = false;

        private void Awake() {
            Instance = this;

            state = State.CountdownToStart;
            GameInput.Instance.SetPlayerMapEnabled(false);

            Time.timeScale = 1f;
        }
        private void Start() {
            GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        }

        private void GameInput_OnPauseAction(object sender, EventArgs e){
            TogglePause();
        }

        private void Update() {
            switch (state){
                case State.CountdownToStart:
                    CountdownToStartActions();
                    break;

                case State.GamePlaying:
                    GamePlayingActions();
                    break;

                case State.GameOver:
                    
                    break;
            }
        }

        private void CountdownToStartActions(){
            countdownToStartTimer -= Time.deltaTime;
            if(countdownToStartTimer < 0f) {
                state = State.GamePlaying;
                OnStateChanged?.Invoke(this, EventArgs.Empty);

                GameInput.Instance.SetPlayerMapEnabled(true);
            }
        }

        private void GamePlayingActions(){
            if(droneIntegrity.health.CurrentHealth <= 0){
                SetGameOver();
            }
        }

        private void SetGameOver(){
            state = State.GameOver;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
            
            Time.timeScale = 0f;
            GameInput.Instance.SetPlayerMapEnabled(false);
        }

        public bool IsCountdownToStartActive(){
            return state == State.CountdownToStart;
        }
        public bool IsGamePlaying(){
            return state == State.GamePlaying;
        }

        public bool IsGameOver(){
            return state == State.GameOver;
        }

        public bool IsGamePaused(){
            return isGamePaused;
        }

        public void TogglePause(){
            isGamePaused = !isGamePaused;
            if(isGamePaused){
                Time.timeScale = 0f;
            } else {
                Time.timeScale = 1f;
            }

            OnTogglePause?.Invoke(this, EventArgs.Empty);
        }
    }
}
