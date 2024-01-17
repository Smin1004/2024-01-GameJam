using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using ExtensionMethods;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [Header("KeyType")]
    [SerializeField] Button _btn1;
    [SerializeField] Button _btn2;
    [SerializeField] Button _btn3;
    [SerializeField] Text _nowKeyType;

    [Header("Audio")]
    [SerializeField] Slider _musicSlider;
    [SerializeField] Slider _sfxSlider;

    [Header("Group")]
    [SerializeField] CanvasGroup _setting;
    [SerializeField] CanvasGroup _keySetting;

    [Header("key")]
    [SerializeField] Text _upKey;
    [SerializeField] Text _downKey;
    [SerializeField] Text _leftKey;
    [SerializeField] Text _rightKey;
    [SerializeField] Text _weaponKey;
    [SerializeField] Text _characterKey;

    [SerializeField] GameObject _keySettingImg;
    private bool _isKeySetting = false;

    [Header("AudioSound")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _keyDownSound;

    private int _keyType = 1;
    private int _key = -1;
    public bool SettingShow { get; private set; }
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    // Start is called before the first frame update
    void Start()
    {
        KeySetup();
        SliderSetup();
        InputSetup();

        //KeyCode up = (KeyCode)PlayerPrefs.GetInt("up", (int)KeyCode.W);
        //KeyCode down = (KeyCode)PlayerPrefs.GetInt("down", (int)KeyCode.S);
        //KeyCode left = (KeyCode)PlayerPrefs.GetInt("left", (int)KeyCode.A);
        //KeyCode right = (KeyCode)PlayerPrefs.GetInt("right", (int)KeyCode.D);
        //KeyCode weapon = (KeyCode)PlayerPrefs.GetInt("weapon", (int)KeyCode.F);



        _musicSlider.onValueChanged.AddListener(x =>
        {
            PlayerPrefs.SetFloat("music", x);
            //Managers.Sound
        });
        _sfxSlider.onValueChanged.AddListener(x =>
        {
            PlayerPrefs.SetFloat("sfx", x);
        });
    }
    private void SliderSetup()
    {
        var music = PlayerPrefs.GetFloat("music", 1.0f);
        var sfx = PlayerPrefs.GetFloat("sfx", 1.0f);
        _musicSlider.value = music;
        _sfxSlider.value = sfx;
    }
    private void KeySetup()
    { 
        _keyType = PlayerPrefs.GetInt("key", 1);
        _isKeySetting = false;
        _keySettingImg.SetActive(false);
    }
    public void OnOffSetting()
    {
        if (!_isKeySetting)
        {
            SettingShow = !SettingShow;
            if (SettingShow)
            {
                SettingPopupSetUp();
                _setting.alpha = 1.0f;
            }
            else _setting.alpha = 0.0f;
        }
        else
        {
            _isKeySetting = false;
            _keySettingImg.SetActive(false);

            _key = -1;
        }
        _canvasGroup.blocksRaycasts = SettingShow;
    }
    public void KeyTypeChange(int i)
    {
        //if (i > 0 && i < 4)
        _keyType = i;
        PlayerPrefs.SetInt("key", _keyType);
        InputSetup();
        SettingPopupSetUp();
    }
    public void SettingPopupSetUp()
    {
        switch (_keyType)
        {
            case 1:
                _btn1.Select();
                _keySetting.interactable = false;
                _nowKeyType.transform.localPosition = new Vector2(-280, 95);
                break;
            case 2: _btn2.Select(); _keySetting.interactable = false;
                _nowKeyType.transform.localPosition = new Vector2(0, 95);
                break;
            case 3: _btn3.Select(); _keySetting.interactable = true;
                _nowKeyType.transform.localPosition = new Vector2(280, 95); break;
        }
    }
    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            if(e.keyCode != KeyCode.Escape)
            if (_key != -1)
            {
                //text.text = e.keyCode.ToString();
                //Debug.Log(e.keyCode.ToString());
                //PlayerPrefs.SetInt("key", key);
                switch (_key)
                {
                    case 1:
                        PlayerPrefs.SetInt("up", (int)e.keyCode);
                        InputManager.instance.upKey = e.keyCode;
                        _upKey.text = e.keyCode.ToString();
                        break;
                    case 2:
                        PlayerPrefs.SetInt("down", (int)e.keyCode);
                        InputManager.instance.downKey = e.keyCode;
                        _downKey.text = e.keyCode.ToString();
                        break;
                    case 3:
                        PlayerPrefs.SetInt("left", (int)e.keyCode);
                        InputManager.instance.leftKey = e.keyCode;
                        _leftKey.text = e.keyCode.ToString();
                        break;
                    case 4:
                        PlayerPrefs.SetInt("right", (int)e.keyCode);
                        InputManager.instance.rightKey = e.keyCode;
                        _rightKey.text = e.keyCode.ToString();
                        break;
                    case 5:
                        PlayerPrefs.SetInt("weapon", (int)e.keyCode);
                        InputManager.instance.weaponKey = e.keyCode;
                        _weaponKey.text = e.keyCode.ToString();
                        break;
                    case 6:
                        PlayerPrefs.SetInt("character", (int)e.keyCode);
                        //InputManager.instance. = e.keyCode;
                        InputManager.instance.playerKey = e.keyCode;
                        _characterKey.text = e.keyCode.ToString();
                        break;
                }
                _isKeySetting = false;
                _keySettingImg.SetActive(false);

                _key = -1;
            }
        }
    }
    public void KeyChange(int k)
    {
        _key = k;
        _isKeySetting = true;
        _keySettingImg.SetActive(true);
    }

    public void SettingReset()
    {
        PlayerPrefs.DeleteKey("music");
        PlayerPrefs.DeleteKey("sfx");

        PlayerPrefs.DeleteKey("up");
        PlayerPrefs.DeleteKey("down");
        PlayerPrefs.DeleteKey("left");
        PlayerPrefs.DeleteKey("right");
        PlayerPrefs.DeleteKey("weapon");
        PlayerPrefs.DeleteKey("character");

        PlayerPrefs.DeleteKey("key");

        //SettingPopupSetUp();
        KeySetup();
        SettingPopupSetUp();
        InputSetup();
        SliderSetup();
    }
    public void DataReset()
    {
        PlayerPrefs.DeleteKey("clearStage");
    }
    private void InputSetup()
    {
        switch (_keyType)
        {
            case 1:
                InputManager.instance.upKey = KeyCode.W;
                InputManager.instance.downKey = KeyCode.S;
                InputManager.instance.leftKey = KeyCode.A;
                InputManager.instance.rightKey = KeyCode.D;
                InputManager.instance.weaponKey = KeyCode.F;
                InputManager.instance.playerKey = KeyCode.Space;
                break;
            case 2:
                InputManager.instance.upKey = KeyCode.UpArrow;
                InputManager.instance.downKey = KeyCode.DownArrow;
                InputManager.instance.leftKey = KeyCode.LeftArrow;
                InputManager.instance.rightKey = KeyCode.RightArrow;
                InputManager.instance.weaponKey = KeyCode.Return;
                InputManager.instance.playerKey = KeyCode.Space;
                break;
            case 3:
                InputManager.instance.upKey = (KeyCode)PlayerPrefs.GetInt("up", (int)KeyCode.W);
                InputManager.instance.downKey = (KeyCode)PlayerPrefs.GetInt("down", (int)KeyCode.S);
                InputManager.instance.leftKey = (KeyCode)PlayerPrefs.GetInt("left", (int)KeyCode.A);
                InputManager.instance.rightKey = (KeyCode)PlayerPrefs.GetInt("right", (int)KeyCode.D);
                InputManager.instance.weaponKey = (KeyCode)PlayerPrefs.GetInt("weapon", (int)KeyCode.E);
                InputManager.instance.playerKey = (KeyCode)PlayerPrefs.GetInt("character", (int)KeyCode.Space);
                break;
        }

        _upKey.text = InputManager.instance.upKey.ToString().WordCount();
        _downKey.text = InputManager.instance.downKey.ToString().WordCount();
        _leftKey.text = InputManager.instance.leftKey.ToString().WordCount();
        _rightKey.text = InputManager.instance.rightKey.ToString().WordCount();
        _weaponKey.text = InputManager.instance.weaponKey.ToString().WordCount();
        _characterKey.text = InputManager.instance.playerKey.ToString().WordCount();
    }
}
