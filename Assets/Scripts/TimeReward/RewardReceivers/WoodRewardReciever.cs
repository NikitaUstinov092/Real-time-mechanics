using UnityEngine;

public class WoodRewardReciever : IRewardReceiver
{
    public void Reward(int rewardCount)
    {
       Debug.Log($"Получено {rewardCount} дерева в качестве вознаграждения");
    }
}
