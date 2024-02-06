using System;
using Zenject;
using IInitializable = Unity.VisualScripting.IInitializable;

namespace Game.App
{
    public sealed class RealtimeClockSaver : 
        IInitializable,
        IDisposable
    {
        private RealtimePreferences preferences;
        private RealtimeClock realtimeClock;
        
        [Inject]
        public void Construct (RealtimePreferences preferences, RealtimeClock realtimeClock)
        {
            this.preferences = preferences;
            this.realtimeClock = realtimeClock;
        }
        
        void IInitializable.Initialize()
        {
            this.realtimeClock.OnPaused += this.SaveSession;
            this.realtimeClock.OnEnded += this.SaveSession;
        }

        void IDisposable.Dispose()
        {
            this.realtimeClock.OnPaused -= this.SaveSession;
            this.realtimeClock.OnEnded -= this.SaveSession;
        }

        private void SaveSession()
        {
            var data = new RealtimeData
            {
                nowSeconds = this.realtimeClock.RealtimeSeconds
            };
            this.preferences.SaveData(data);
        }
    }
}