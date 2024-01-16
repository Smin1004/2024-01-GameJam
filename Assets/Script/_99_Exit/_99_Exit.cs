using UnityEngine;
using UnityEditor;

public class _99_Exit : MonoBehaviour
{
    void Start()
    {
        #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
