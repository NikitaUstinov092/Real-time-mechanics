using System;
using Elementary;
using Lessons.MetaGame;
using Sirenix.OdinInspector;
using UnityEngine;

    public sealed class TimeReward : IRealtimeTimer
    {
        public event Action<IRealtimeTimer> OnStarted;
        
        [ShowInInspector, ReadOnly]
        public string Id { get; private set; }

        [ShowInInspector, ReadOnly]
        private readonly Countdown _timer = new();
        
        [ShowInInspector, ReadOnly]
        private int _rewardCount;
        
        [SerializeField]
        private IRewardReceiver _rewardReceiver;

        public void Construct(IRewardReceiver rewardReceiver, float duration, int rewardCount, string timerId)
        {
            _rewardReceiver = rewardReceiver;
            _timer.Duration = duration;
            _timer.RemainingTime = duration;
            _rewardCount = rewardCount;
            Id = timerId;
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

            _rewardReceiver.Reward(_rewardCount);
            
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
