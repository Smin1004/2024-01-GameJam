using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public static void ExitScene()
    {
        SceneManager.LoadScene("_99_Exit");
    }
}
