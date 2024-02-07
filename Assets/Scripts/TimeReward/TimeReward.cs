using System;
using Elementary;
using Lessons.MetaGame;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

    public sealed class TimeReward : IRealtimeTimer
    {
        public event Action<IRealtimeTimer> OnStarted;
        
        [ShowInInspector, ReadOnly]
        public string Id => nameof(TimeReward);

        [ShowInInspector, ReadOnly]
        private readonly Countdown _timer = new();
        
        [SerializeField]
        private IRewardReceiver rewardReceiver;
        
        [SerializeField]
        private TimeRewardConfig _config;
        public void Construct(IRewardReceiver rewardReceiver, TimeRewardConfig config)
        {
            this.rewardReceiver = rewardReceiver;
            _config = config;

            _timer.Duration = config.Duration;
            _timer.RemainingTime = config.Duration;

            Initialize();
        }
        
        [Button]
        public bool CanReceiveReward()
        {
            return _timer.Progress >= 1;
        }

        [Button]
        public void ReceiveReward()
        {
            if (!CanReceiveReward())
            {
                Debug.LogError("Can't receive reward!");
                return;
            }

            rewardReceiver.Reward(_config.RewardCount);
            
            _timer.ResetTime();
            _timer.Play();
            
            OnStarted?.Invoke(this);
        }
        
        public void Initialize()
        {
            if (_timer.Progress <= 0)
            {
                OnStarted?.Invoke(this);
            }

            _timer.Play();
        }

        void IRealtimeTimer.Synchronize(float offlineSeconds)
        {
            _timer.RemainingTime -= offlineSeconds;
        }
    }
