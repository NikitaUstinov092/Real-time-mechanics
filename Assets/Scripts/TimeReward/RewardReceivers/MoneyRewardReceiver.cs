using UnityEngine;

public sealed class MoneyRewardReceiver : IRewardReceiver
    {
        public void Reward(int rewardCount)
        {
            Debug.Log($"Получено {rewardCount} денежного вознаграждения");
        }
    }
