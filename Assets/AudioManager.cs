using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                                   //UI ����� ����
using UnityEngine.Audio;                                //����� ����� ����
using System;
using UnityEngine.Rendering;                                           //�迭�� ���ٽ� ����� ���ؼ� 

[Serializable]
public class Sound                                      //���� Ŭ���� �̸��� �����ϱ� ���� ���
{
    public string name;                                 //�̸��� �����ش�.
    public AudioClip clip;                              //����� Ŭ��
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    //����� Clip �迭 
    public Sound[] musicSounds;                         //����� ���� ����
    public Sound[] sfxSound;

    public AudioSource musicSource;                     //����� ����� �ҽ� ����
    public AudioSource sfxSource;

    //����� �ɼ�
    public AudioMixer mixer;                            //����� ����� �ͼ�
    public Slider musicSlider;                          //�ɼ�â���� ����� MusicSlider
    public Slider sfxSlider;                            //�ɼ�â���� ����� SFXSlider

    const string MIXER_MUSIC = "Music";           //����� Param �� (Music)
    const string MIXER_SFX = "SFX";               //����� Param �� (SFX)

    //����� �г� ����
    //public GameObject AudioPanel;
    //public bool AudioPanelFlag = false;                 //����� OnOff �Ǿ��ִ��� ���θ� �˻�

    private void Awake()
    {
        Instance = this;

        float music = PlayerPrefs.GetFloat("music", 1.0f);           //���۽� 1�� ����
        float sfx = PlayerPrefs.GetFloat("sfx", 1.0f);               //���۽� 1�� ����

        SetMusicVolume(music);
        SetSFXVolume(sfx);

        musicSlider.onValueChanged.AddListener(SetMusicVolume);     //�����̴��� ���� ���� �Ǿ����� �ش� �Լ��� ȣ�� �Ѵ�. 
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);     //�����̴��� ���� ���� �Ǿ����� �ش� �Լ��� ȣ�� �Ѵ�. 
    }
    void SetMusicVolume(float value)
    {

        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);       //Log10������ 0 ~ 80 �� ������ �����Ҽ� �ְ� ���ش�. 
        //print(Mathf.Log10(value) * 20);//���۽� 1�� ����
    }
    void SetSFXVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);       //Log10������ 0 ~ 80 �� ������ �����Ҽ� �ְ� ���ش�. 
    }

    public void PlayMusic(string name)                      //����� BGM �Լ� ���� 
    {
        Sound sound = Array.Find(musicSounds, x => x.name == name);     //Array ���ٽ� name�� ã�Ƽ� ��ȯ

        if (sound == null)                           //name���ε� wav�� ���� ��� Log ���
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = sound.clip;              //������ ����� �ҽ��� CLIP�� �ִ´�.    
            musicSource.Play();                         //�Ϲ� Play ���
        }
    }
    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSound, x => x.name == name);     //Array ���ٽ� name�� ã�Ƽ� ��ȯ

        if (sound == null)                           //name���ε� wav�� ���� ��� Log ���
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(sound.clip);                         //�Ϲ� Play ���
        }
    }

    public void PusecMisic()
    {
        musicSource.Pause();
    }
    public void PauseSFX()
    {
        sfxSource.Pause();
    }

    //public void PanelOnOff(bool type)                                   //����� �ɼ� �г�
    //{
    //    AudioPanelFlag = type;                                          //���� Ÿ�԰� ����ȭ
    //    AudioPanel.SetActive(type);                                     //�г��� Ű�� ����. 
    //}
}