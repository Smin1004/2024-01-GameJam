using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageStart : Obj_Base
{
    public int stage;
    [SerializeField] Image _image;

    public override void UseObj()
    {
        int clearStage = PlayerPrefs.GetInt("clearStage", 0);
        if((clearStage + 1) > stage)
        {

        }
        else
        {

        }
        MoveManager.Instance.curPlayer.allStop = true;
        FadeManager fade = FindAnyObjectByType<FadeManager>();
        fade?.Skip();
        fade?.FadeOut(1.0f);
        RoundData.Instance.Reset(stage-1);

        Invoke("InStage", 1.0f);
    }
    private void InStage()
    {
        SceneManager.LoadScene("_02_InGame");
    }
}
