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

    public RoundData roundData;

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
        roundData.InitData();
        //RoundData.InitData();
    }

    private void Update()
    {
        if (!_SceneLoaded)
        {
            //�ؽ�Ʈ ���İ� ����
            if (_textAlpha <= 0.0f) _targetAlpha = 1.25f;
            if (_textAlpha >= 1.0f) _targetAlpha = -0.01f;
            _textAlpha = Mathf.Lerp(_textAlpha, _targetAlpha, Time.deltaTime * 3.5f);

            //���İ� �ٲٱ�
            Color textColor = _titleText.color;
            textColor.a = 1 - _textAlpha;
            _titleText.color = textColor;
            //Debug.Log($"{_textAlpha}:{_targetAlpha}");

            //�̹��� ũŰ ����
            _imgSize = Time.time % 1.5f;
            if (_imgSize < 1.0f) _imgSize = 1;
            if (_imgSize > 1.25f) _imgSize = (1.5f - _imgSize) + 1;
            //_titleIMG.rectTransform.localScale = new Vector2(_imgSize, _imgSize);
            //ReadOnlyCollectionBase
            //Debug.Log(_imgSize);

            //ESCŰ �˻�
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _fade.Skip();
                _setting.OnOffSetting();
            }
            //Ű�� ���ȴ��� �˻�
            else NextScene();
        }
        else
        {
            //�׻� ���̰�
            Color textColor = _titleText.color;
            textColor.a = 1.0f;
            _titleText.color = textColor;

            //�� �����̰�
            _titleIMG.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        }
    }
    private void NextScene()
    {
        if (Input.anyKeyDown && !_SceneLoaded && !_setting.SettingShow) 
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
        //SceneManager.LoadScene("_01_ChapterIndex");
        SceneManager.LoadScene("_01_ChapterIndex");
    }
}
