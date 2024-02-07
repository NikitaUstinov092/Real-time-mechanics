using UnityEngine;

public class LifeRewardReceiver : IRewardReceiver
{
    public void Reward(int rewardCount)
    {
        Debug.Log( $"Получены {rewardCount} жизни в качестве вознаграждения");
    }
}
