using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageStart : Obj_Base
{
    public int stage;
    [SerializeField] Image _image;
    [SerializeField] GameObject _container;

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
            
            GameObject temp = Instantiate(_container, FindAnyObjectByType<CameraMove>().transform);
            temp.transform.localPosition = new Vector3(0, 3,10);
        }
    }
    private void InStage()
    {
        SceneManager.LoadScene("_02_InGame");
    }
}
