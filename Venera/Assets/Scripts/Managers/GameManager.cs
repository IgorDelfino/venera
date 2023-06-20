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

        private State _state;
        private float _countdownToStartTimer = 3f;

        public float CountdownToStartTimer { get => _countdownToStartTimer; }

        [SerializeField] private DroneIntegrity _droneIntegrity;

        private bool _isGamePaused = false;

        private void Awake() {
            Instance = this;

            _state = State.CountdownToStart;
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
            switch (_state){
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
            _countdownToStartTimer -= Time.deltaTime;
            if(_countdownToStartTimer < 0f) {
                _state = State.GamePlaying;
                OnStateChanged?.Invoke(this, EventArgs.Empty);

                GameInput.Instance.SetPlayerMapEnabled(true);
            }
        }

        private void GamePlayingActions(){
            if(_droneIntegrity.health.CurrentHealth <= 0){
                SetGameOver();
            }
        }

        private void SetGameOver(){
            _state = State.GameOver;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
            
            Time.timeScale = 0f;
            GameInput.Instance.SetPlayerMapEnabled(false);
        }

        public bool IsCountdownToStartActive(){
            return _state == State.CountdownToStart;
        }
        public bool IsGamePlaying(){
            return _state == State.GamePlaying;
        }

        public bool IsGameOver(){
            return _state == State.GameOver;
        }

        public bool IsGamePaused(){
            return _isGamePaused;
        }

        public void TogglePause(){
            _isGamePaused = !_isGamePaused;
            if(_isGamePaused){
                Time.timeScale = 0f;
            } else {
                Time.timeScale = 1f;
            }

            OnTogglePause?.Invoke(this, EventArgs.Empty);
        }
    }
}
