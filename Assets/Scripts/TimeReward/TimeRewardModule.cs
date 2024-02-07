using Lessons.MetaGame;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

    public sealed class TimeRewardModule : MonoBehaviour
    {
        [ShowInInspector]
        public readonly TimeReward _timeReward = new();
    
        [SerializeField]
        private TimeRewardConfig _config;
     
        [Inject]
        private void Construct(DiContainer container)
        {
            var rewardReceiverType = _config.rewardReceiver.GetType();
            var rewardReceiverComp = container.Resolve(rewardReceiverType);
            
            _timeReward.Construct(rewardReceiverComp as IRewardReceiver, _config.Duration, _config.RewardCount);
            
            var saveLoader = container.Resolve<RealtimeSaveLoader>();
            saveLoader.RegisterTimer(_timeReward);
        }
    }
