using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageStart : Obj_Base
{
    public int stage;
    [SerializeField] Image _image;

    public override void UseObj()
    {
        int clearStage = PlayerPrefs.GetInt("clearStage", -1);
        if((clearStage + 1)/*0*/ >= stage-1)
        {
            MoveManager.Instance.curPlayer.allStop = true;
            FadeManager fade = FindAnyObjectByType<FadeManager>();
            fade?.Skip();
            fade?.FadeOut(1.0f);
            RoundData.Instance.Reset(stage - 1);

            Invoke("InStage", 1.0f);
        }
        else
        {
            Debug.Log("_99_Exit");
        }
    }
    private void InStage()
    {
        SceneManager.LoadScene("_02_InGame");
    }
}
