using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Venera
{
    public class TimeTickSystem : MonoBehaviour
    {
        public class OnTickEventArgs: EventArgs {
            public int Tick;
        }

        public static event EventHandler<OnTickEventArgs> OnTick;
        public static event EventHandler<OnTickEventArgs> OnTick_5;

        private const float TICK_TIMER_MAX = 0.2f;

        OnTickEventArgs args;

        private int _tick;
        private float _tickTimer;

        private void Awake() {
            _tick = 0;
            args = new OnTickEventArgs();
            args.Tick = 0;
        }

        private void Update() {
            _tickTimer += Time.deltaTime;

            if(_tickTimer >= TICK_TIMER_MAX) {
                _tickTimer -= TICK_TIMER_MAX;
                _tick++;
                args.Tick = _tick;

                OnTick?.Invoke(this, args);

                if(_tick % 5 == 0) {
                    OnTick_5?.Invoke(this, args);
                }
            }
        }
    }
}