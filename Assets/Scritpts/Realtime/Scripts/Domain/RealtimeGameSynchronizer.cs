using System;
using Game.GameEngine;
using Zenject;
using IInitializable = Unity.VisualScripting.IInitializable;

namespace Game.App
{
    public sealed class RealtimeGameSynchronizer : 
        IInitializable,
        IDisposable
    {
        [Inject]
        private DiContainer _diContainer;
        
        [Inject]
        private RealtimeClock realtimeClock;

        void IInitializable.Initialize()
        {
            this.realtimeClock.OnStarted += this.OnSessionStarted;
            this.realtimeClock.OnResumed += this.OnSessionResumed;
        }

        void IDisposable.Dispose()
        {
            this.realtimeClock.OnStarted -= this.OnSessionStarted;
            this.realtimeClock.OnResumed -= this.OnSessionResumed;
        }

        private void OnSessionStarted(long pauseSeconds)
        {
            if (pauseSeconds > 0)
            {
              var container =   _diContainer.Resolve<TimeShiftEmitter>();
              container.EmitEvent(TimeShiftReason.START_GAME, pauseSeconds);
            }
        }

        private void OnSessionResumed(long pauseSeconds)
        {
            if (pauseSeconds > 0)
            {
                var container =   _diContainer.Resolve<TimeShiftEmitter>();
                container.EmitEvent(TimeShiftReason.RESUME_GAME, pauseSeconds);
            }
        }
    }
}