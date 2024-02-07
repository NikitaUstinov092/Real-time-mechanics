using UnityEngine;

public class TimeRewardInitializer : MonoBehaviour
{
    [SerializeField] 
    private TimeRewardModule[] _timeRewardModules;
    
    public void Start()
    {
        foreach (var timer in _timeRewardModules)
        {
            timer.Initialize();
        }
    }
}
