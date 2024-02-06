using UnityEngine;

namespace Game.App
{
    public sealed class RealtimePreferences
    {
        private const string PREFS_KEY = "UserSessionData";
     
        public bool LoadData(out RealtimeData data)
        {
            if (PlayerPrefs.HasKey(PREFS_KEY))
            {
                data.nowSeconds = (long)PlayerPrefs.GetFloat(PREFS_KEY);
                return true;
            }

            data = default;
            return false;
        }

        public void SaveData(RealtimeData data)
        {
            PlayerPrefs.SetFloat(PREFS_KEY, data.nowSeconds);
        }
    }
}