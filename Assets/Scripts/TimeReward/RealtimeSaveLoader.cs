using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Zenject;

namespace Lessons.MetaGame
{
    public sealed class RealtimeSaveLoader : IInitializable
    {
        private readonly HashSet<IRealtimeTimer> _timers = new();

        public void RegisterTimer(IRealtimeTimer timer)
        {
            if (_timers.Add(timer))
            {
                timer.OnStarted += SaveTimer;
            }
        }

        public void UnregisterTimer(IRealtimeTimer timer)
        {
            if (_timers.Remove(timer))
            {
                timer.OnStarted -= SaveTimer;
            }
        }

        void IInitializable.Initialize()
        { 
            var now = DateTime.Now;
            foreach (var timer in _timers)
            {
                SynchronizeTimer(timer, now);
            }
        }

        private void SynchronizeTimer(IRealtimeTimer timer, DateTime now)
        {
            var timerId = timer.Id;
            
            if (!PlayerPrefs.HasKey(timerId))
            {
                return;
            }

            var serializedTime = PlayerPrefs.GetString(timerId);
            var previousTime = DateTime.Parse(serializedTime, CultureInfo.InvariantCulture);

            var timeSpan = now - previousTime;
            var offlineSeconds = (float) timeSpan.TotalSeconds;
            timer.Synchronize(offlineSeconds);
        }

        private void SaveTimer(IRealtimeTimer timer)
        {
            var currentTime = DateTime.Now;
            var serializedTime = currentTime.ToString(CultureInfo.InvariantCulture);
            PlayerPrefs.SetString(timer.Id, serializedTime);
        }
        
    }
    
}