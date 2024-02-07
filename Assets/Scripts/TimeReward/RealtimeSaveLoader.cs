using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Zenject;

namespace Lessons.MetaGame
{
    public sealed class RealtimeSaveLoader : IInitializable
    {
        private readonly HashSet<IRealtimeTimer> timers = new();

        public void RegisterTimer(IRealtimeTimer timer)
        {
            if (timers.Add(timer))
            {
                timer.OnStarted += SaveTimer;
            }
        }

        public void UnregisterTimer(IRealtimeTimer timer)
        {
            if (timers.Remove(timer))
            {
                timer.OnStarted -= SaveTimer;
            }
        }

        void IInitializable.Initialize()
        { 
            var now = DateTime.Now;
            foreach (var timer in timers)
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
    
    // public sealed class TimeRewardSaveLoader : MonoBehaviour, IGameLoadListener
    // {
    //     private const string GAME_TIME_PREFS = "GameTime";
    //     private TimeReward _timeReward;
    //
    //     public void OnLoadGame(GameFacade gameFacade)
    //     {
    //         this._timeReward = gameFacade.GetService<TimeReward>();
    //         this._timeReward.OnTimerStarted += this.OnTimerStarted;
    //         this.SynchronizeTime();
    //     }
    //
    //     private void SynchronizeTime()
    //     {
    //         if (!PlayerPrefs.HasKey(GAME_TIME_PREFS))
    //         {
    //             return;
    //         }
    //
    //         var serializedTime = PlayerPrefs.GetString(GAME_TIME_PREFS);
    //         var previousTime = DateTime.Parse(serializedTime, CultureInfo.InvariantCulture);
    //
    //         var timeSpan = DateTime.Now - previousTime;
    //         var pauseSeconds = timeSpan.TotalSeconds;
    //         this._timeReward.DecrementTimer((float) pauseSeconds);
    //         Debug.Log($"PAUSE SECONDS {pauseSeconds}");
    //     }
    //
    //     private void OnTimerStarted()
    //     {
    //         SaveTime();
    //     }
    //
    //     private void SaveTime()
    //     {
    //         Debug.Log("SAVE TIMER");
    //         var currentTime = DateTime.Now;
    //         var serializedTime = currentTime.ToString(CultureInfo.InvariantCulture);
    //         PlayerPrefs.SetString(GAME_TIME_PREFS, serializedTime);
    //     }
    // }
}