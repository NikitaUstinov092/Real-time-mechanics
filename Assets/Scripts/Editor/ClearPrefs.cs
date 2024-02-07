using UnityEditor;
using UnityEngine;

public class ClearPrefs : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("MyTools/Clear PlayerPrefs")]
    public static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs cleared.");
    }
#endif
}
