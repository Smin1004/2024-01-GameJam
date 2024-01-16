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
    [SerializeField]Text _upKey;
    [SerializeField]Text _downKey;
    [SerializeField]Text _leftKey;
    [SerializeField]Text _rightKey;
    [SerializeField] Text _weaponKey;

    [Header("AudioSound")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _keyDownSound;

    private int _keyType = 1;
    public bool _settingShow { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        KeySetup();
        SliderSetup();

        //KeyCode up = (KeyCode)PlayerPrefs.GetInt("up", (int)KeyCode.W);
        //KeyCode down = (KeyCode)PlayerPrefs.GetInt("down", (int)KeyCode.S);
        //KeyCode left = (KeyCode)PlayerPrefs.GetInt("left", (int)KeyCode.A);
        //KeyCode right = (KeyCode)PlayerPrefs.GetInt("right", (int)KeyCode.D);
        //KeyCode weapon = (KeyCode)PlayerPrefs.GetInt("weapon", (int)KeyCode.F);

        InputSetup();


        _musicSlider.onValueChanged.AddListener(x =>
        {
            PlayerPrefs.SetFloat("music", x);
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
    private void KeySetup() => _keyType = PlayerPrefs.GetInt("key", 1);
    public void OnOffSetting()
    {
        _settingShow = !_settingShow;
        if (_settingShow)
        {
            SettingPopupSetUp();
            _setting.alpha = 1.0f;
        }
        else _setting.alpha = 0.0f;
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
                _nowKeyType.transform.localPosition = new Vector2(-290,95);
                    break;
            case 2: _btn2.Select(); _keySetting.interactable = false;
                _nowKeyType.transform.localPosition = new Vector2(0,95);
                break;
            case 3: _btn3.Select(); _keySetting.interactable = true; 
                _nowKeyType.transform.localPosition = new Vector2(290,95);break;
        }
    }
    private int key = -1;
    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            if (key != -1)
            {
                //text.text = e.keyCode.ToString();
                //Debug.Log(e.keyCode.ToString());
                //PlayerPrefs.SetInt("key", key);
                switch (key)
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
                }
                key = -1;
            }
        }
    }
    public void KeyChange(int k) => key = k;

    public void SettingReset()
    {
        PlayerPrefs.DeleteKey("music");
        PlayerPrefs.DeleteKey("sfx");

        PlayerPrefs.DeleteKey("up");
        PlayerPrefs.DeleteKey("down");
        PlayerPrefs.DeleteKey("left");
        PlayerPrefs.DeleteKey("right");
        PlayerPrefs.DeleteKey("weapon");

        PlayerPrefs.DeleteKey("key");

        //SettingPopupSetUp();
        OnOffSetting();

        InputSetup();
        SliderSetup();
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
                break;
            case 2:
                InputManager.instance.upKey = KeyCode.UpArrow;
                InputManager.instance.downKey = KeyCode.DownArrow;
                InputManager.instance.leftKey = KeyCode.LeftArrow;
                InputManager.instance.rightKey = KeyCode.RightArrow;
                InputManager.instance.weaponKey = KeyCode.Return;
                break;
            case 3:
                InputManager.instance.upKey = (KeyCode)PlayerPrefs.GetInt("up", (int)KeyCode.W);
                InputManager.instance.downKey = (KeyCode)PlayerPrefs.GetInt("down", (int)KeyCode.S);
                InputManager.instance.leftKey = (KeyCode)PlayerPrefs.GetInt("left", (int)KeyCode.A);
                InputManager.instance.rightKey = (KeyCode)PlayerPrefs.GetInt("right", (int)KeyCode.D);
                InputManager.instance.weaponKey = (KeyCode)PlayerPrefs.GetInt("weapon", (int)KeyCode.E);
                InputManager.instance.playerKey = KeyCode.Q;
                break;
        }

        _upKey.text = InputManager.instance.upKey.ToString().WordCount();
        _downKey.text = InputManager.instance.downKey.ToString().WordCount();
        _leftKey.text = InputManager.instance.leftKey.ToString().WordCount();
        _rightKey.text = InputManager.instance.rightKey.ToString().WordCount();
        _weaponKey.text = InputManager.instance.weaponKey.ToString().WordCount();
    }
}
