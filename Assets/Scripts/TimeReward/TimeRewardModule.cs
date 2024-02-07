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
            var rewardReceiverType = _config.RewardReceiver.GetType();
            var rewardReceiverComp = container.Resolve(rewardReceiverType);
            var saveLoader = container.Resolve<RealtimeSaveLoader>();
          
            _timeReward.Construct(rewardReceiverComp as IRewardReceiver, _config.Duration, _config.RewardCount, gameObject.name);
            saveLoader.RegisterTimer(_timeReward);
        }
        
        public void Initialize()
        {
            _timeReward.Initialize();
        }
    }
