using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] Button _btn1;
    [SerializeField] Button _btn2;
    [SerializeField] Button _btn3;
    [SerializeField] Slider _musicSlider;
    [SerializeField] Slider _sfxSlider;
    [SerializeField] CanvasGroup _setting;
    [SerializeField] CanvasGroup _keySetting;

    [Header("SettingKey")]
    [SerializeField] InputField _upKey;
    [SerializeField] InputField _downKey;
    [SerializeField] InputField _leftKey;
    [SerializeField] InputField _rightKey;
    [SerializeField] InputField _weaponSwapKey;

    [Header("Audio")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _keyDownSound;

    private int _keyType = 1;
    public bool _settingShow { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        _musicSlider.onValueChanged.AddListener(OnMusicValueChange);
        _sfxSlider.onValueChanged.AddListener(OnSFXValueChange);

        _upKey.onValueChanged.AddListener(str =>
        {
            _upKey.text = $"{str[^1]}".ToUpper();
        });
        _downKey.onValueChanged.AddListener(str =>
        {
            _downKey.text = $"{str[^1]}".ToUpper();
        });
        _leftKey.onValueChanged.AddListener(str =>
        {
            _leftKey.text = $"{str[^1]}".ToUpper();
        });
        _rightKey.onValueChanged.AddListener(str =>
        {
            _rightKey.text = $"{str[^1]}".ToUpper();
        });
        _weaponSwapKey.onValueChanged.AddListener(str =>
        {
            _weaponSwapKey.text = $"{str[^1]}".ToUpper();
        });
    }
    private void OnMusicValueChange(float value)
    {
        //SettingPopupSetUp();
    }
    private void OnSFXValueChange(float value)
    {
        //SettingPopupSetUp();
    }
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
        SettingPopupSetUp();
    }
    public void SettingPopupSetUp()
    {
        switch (_keyType)
        {
            case 1: _btn1.Select(); _keySetting.interactable = false; break;
            case 2: _btn2.Select(); _keySetting.interactable = false; break;
            case 3: _btn3.Select(); _keySetting.interactable = true; break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
