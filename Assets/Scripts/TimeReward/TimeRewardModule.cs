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
            var type = _config.rewardReceiver.GetType();
            var rewardReceiverComp = container.Resolve(type);
            
            _timeReward.Construct(rewardReceiverComp as IRewardReceiver, _config);
            
            var saveLoader = container.Resolve<RealtimeSaveLoader>();
            saveLoader.RegisterTimer(_timeReward);
        }
    }
