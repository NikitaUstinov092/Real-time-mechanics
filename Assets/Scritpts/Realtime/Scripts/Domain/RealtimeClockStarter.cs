using System;
using System.Collections;
using System.Threading.Tasks;
using Asyncoroutine;
using SystemTime;
using Zenject;

namespace Game.App
{
    public sealed class RealtimeClockStarter
    {
        private RealtimeClock realtimeClock;
        private RealtimePreferences preferences;
        
        [Inject]
        public void Construct (RealtimeClock realtimeClock, RealtimePreferences preferences)
        {
            this.realtimeClock = realtimeClock;
            this.preferences = preferences;
        }
        
        public async Task StartClockAsync()
        {
            if (this.preferences.LoadData(out RealtimeData previousSession))
            {
                await this.StartByPrevious(previousSession.nowSeconds);
            }
            else
            {
                await this.StartAsFirst();
            }
        }

        private IEnumerator StartByPrevious(long previousSeconds)
        {
            yield return OnlineTime.RequestNowSeconds(nowSeconds =>
            {
                var pauseTime = Math.Max(nowSeconds - previousSeconds, 0);
                this.realtimeClock.Play(nowSeconds, pauseTime);
            });
        }

        private IEnumerator StartAsFirst()
        {
            yield return OnlineTime.RequestNowSeconds(nowSeconds =>
            {
                this.realtimeClock.Play(nowSeconds);
            });
        }
    }
}