using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _01_ChapterIndex : MonoBehaviour
{
    [SerializeField] FadeManager _fade;
    private void Start()
    {
        _fade.FadeIn(0.5f);
        Invoke("SceneStart", 1);
    }
    void SceneStart()
    {
        SceneManager.LoadScene("TestScene");
    }
}
