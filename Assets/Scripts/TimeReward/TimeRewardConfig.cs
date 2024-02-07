using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(
        fileName = "TimeRewardConfig"
    )]
    public sealed class TimeRewardConfig : ScriptableObject
    {
        [FormerlySerializedAs("rewardReceiver")] [SerializeReference]
        public IRewardReceiver RewardReceiver;
        
        public float Duration;
        public int RewardCount;
    }
