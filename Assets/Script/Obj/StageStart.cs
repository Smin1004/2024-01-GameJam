using UnityEngine.SceneManagement;

public class StageStart : Obj_Base
{
    public int stage;
    public override void UseObj()
    {
        MoveManager.Instance.curPlayer.allStop = true;
        FadeManager fade = FindAnyObjectByType<FadeManager>();
        fade?.Skip();
        fade?.FadeOut(0.5f);
        RoundData.Instance.Reset(stage-1);

        Invoke("InStage", 2.0f);
    }
    private void InStage()
    {
        SceneManager.LoadScene("TestScene");
    }
}
