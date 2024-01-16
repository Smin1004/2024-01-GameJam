using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _02_InGame : MonoBehaviour
{
    [SerializeField] Setting _setting;
    [SerializeField] FadeManager _fade;
    private void Start()
    {
        _fade.FadeIn(0.5f);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) _setting.OnOffSetting();
    }
}
