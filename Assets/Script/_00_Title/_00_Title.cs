using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class _00_Title : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Text _titleText;
    [SerializeField] Image _titleIMG;
    [SerializeField] FadeManager _fade;
    [SerializeField] Setting _setting;

    private float _textAlpha = 0.0f;
    private float _targetAlpha = 1.0f;

    private float _imgSize = 1;
    private bool _SceneLoaded = false;

    private void Awake()
    {
        //_keyType = 1;
        _SceneLoaded = false;
        _textAlpha = 0.0f;
        _targetAlpha = 1.0f;
        _imgSize = 1;
    }
    private void Start()
    {
        _fade.FadeIn(0.5f);
    }

    private void Update()
    {
        if (!_SceneLoaded)
        {
            //텍스트 알파값 설정
            if (_textAlpha <= 0.0f) _targetAlpha = 1.25f;
            if (_textAlpha >= 1.0f) _targetAlpha = -0.01f;
            _textAlpha = Mathf.Lerp(_textAlpha, _targetAlpha, Time.deltaTime * 3.5f);

            //알파값 바꾸기
            Color textColor = _titleText.color;
            textColor.a = 1 - _textAlpha;
            _titleText.color = textColor;
            //Debug.Log($"{_textAlpha}:{_targetAlpha}");

            //이미지 크키 설정
            _imgSize = Time.time % 1.5f;
            if (_imgSize < 1.0f) _imgSize = 1;
            if (_imgSize > 1.25f) _imgSize = (1.5f - _imgSize) + 1;
            //_titleIMG.rectTransform.localScale = new Vector2(_imgSize, _imgSize);
            //ReadOnlyCollectionBase
            //Debug.Log(_imgSize);

            //ESC키 검사
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _fade.Skip();
                _setting.OnOffSetting();
            }
            //키가 눌렸는지 검사
            else NextScene();
        }
        else
        {
            //항상 보이게
            Color textColor = _titleText.color;
            textColor.a = 1.0f;
            _titleText.color = textColor;

            //안 움직이게
            _titleIMG.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        }
    }
    private void NextScene()
    {
        if (Input.anyKeyDown && !_SceneLoaded && !_setting._settingShow) 
        {
            _fade.Skip();
            _fade.FadeOut(0.5f);
            _SceneLoaded = true;
            StartCoroutine(LoadScene());
        }
    }
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("_01_ChapterIndex");
    }
}
