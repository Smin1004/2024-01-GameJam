using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _02_InGame : MonoBehaviour
{
    [SerializeField] Setting _setting;
    [SerializeField] FadeManager _fade;
    [SerializeField] MoveManager _moveManager;
    private void Start()
    {
        _fade.FadeIn(1.0f);
        AudioManager.Instance.PlayMusic("InGameSong");
    }
    void Update()
    {
        if(!_moveManager.GameEnd)
        if(Input.GetKeyDown(KeyCode.Escape)) _setting.OnOffSetting();
    }
}
