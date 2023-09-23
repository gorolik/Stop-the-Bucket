using System;
using System.Collections;
using System.Collections.Generic;
using Sources.Infrastructure;
using UnityEngine;

namespace Sources.Services.Timer
{
    public class TimersHandler : ITimersHandler
    {
        private readonly ICoroutineRunner _coroutineRunner;

        private bool _coroutineIsRun;
        private List<Timer> _timers = new List<Timer>();

        public TimersHandler(ICoroutineRunner coroutineRunner) =>
            _coroutineRunner = coroutineRunner;

        public Timer StartNewTimer(float seconds)
        {
            Timer timer = new Timer(seconds);
            _timers.Add(timer);
            
            timer.TimeEnd += OnTimerEnd;

            if (!_coroutineIsRun)
                _coroutineRunner.StartCoroutine(TimeUpdate());
            
            return timer;
        }

        public void StopTimer(Timer timer) =>
            _timers.Remove(timer);
        
        public void ResumeTimer(Timer timer) =>
            _timers.Add(timer);

        private void OnTimerEnd(Timer timer)
        {
            timer.TimeEnd -= OnTimerEnd;
            _timers.Remove(timer);
        }

        private IEnumerator TimeUpdate()
        {
            _coroutineIsRun = true;
            
            while (_timers.Count > 0)
            {
                for (int i = 0; i < _timers.Count; i++) 
                    _timers[i].Tick(Time.deltaTime);

                yield return null;
            }
            
            _coroutineIsRun = false;
        }

        public class Timer
        {
            private float _time;
            private Coroutine _coroutine;

            public event Action<float> TimeUpdated;
            public event Action<Timer> TimeEnd;

            public Timer(float seconds) =>
                _time = seconds;

            public void Tick(float deltaTime)
            {
                _time -= deltaTime;

                if (_time <= 0)
                {
                    _time = 0;
                    TimeEnd?.Invoke(this);
                }
                
                TimeUpdated?.Invoke(_time);
            }
        }
    }
}