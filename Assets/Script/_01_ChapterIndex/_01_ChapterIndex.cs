using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _01_ChapterIndex : MonoBehaviour
{
    [SerializeField] FadeManager _fade;
    [SerializeField] Setting _setting;
    private void Start()
    {
        _fade.FadeIn(1.0f);
        //Invoke("SceneStart", 1);
    }
    void SceneStart()
    {
        SceneManager.LoadScene("TestScene");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _fade.Skip();
            _setting.OnOffSetting();
        }
    }
}
