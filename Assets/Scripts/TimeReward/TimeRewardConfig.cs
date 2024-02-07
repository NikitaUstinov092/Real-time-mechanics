using UnityEngine;


    [CreateAssetMenu(
        fileName = "TimeRewardConfig"
    )]
    public sealed class TimeRewardConfig : ScriptableObject
    {
        [SerializeReference]
        public IRewardReceiver rewardReceiver;
        
        public float Duration;
        public int RewardCount;
    }
