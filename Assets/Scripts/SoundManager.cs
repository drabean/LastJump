using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Inst;
    void Awake()
    {
        Inst = this;
        //DontDestroyOnLoad(gameObject);
        GameObject BGM = new GameObject();
        BGM.transform.SetParent(transform);
        BGM.name = "BGMPlayer";
        BGMPlayer = BGM.AddComponent<AudioSource>();
        BGMPlayer.loop = true;
        BGMPlayer.volume = BGMvolume;

        GameObject Intro = new GameObject();
        Intro.transform.SetParent(transform);
        Intro.name = "IntroPlayer";
        IntroPlayer = Intro.AddComponent<AudioSource>();
        IntroPlayer.volume = BGMvolume;

        for (int i = 0; i < 3; i++)
        {
            GameObject temp = new GameObject();
            temp.transform.SetParent(transform);
            temp.name = "SFXPlayer";
            AudioSource audio = temp.AddComponent<AudioSource>();
            SFXPlayers.Add(audio);
        }
    }

    Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();
    List<AudioSource> SFXPlayers = new List<AudioSource>();
    float SFXvolume = 1f;

    AudioSource BGMPlayer;
    float BGMvolume = 1f;
    AudioSource IntroPlayer;
    #region SFX
    public void Play(string name)
    {
        AudioClip sfx;

        if (sounds.ContainsKey(name))
        {
            sfx = sounds[name];
        }
        else
        {
            sfx = Resources.Load<AudioClip>("SFX/" + name);
            sounds.Add(name, sfx);
        }

        AudioSource audioSource = findAudioSource();
        if (audioSource != null)
        {
            audioSource.clip = sfx;
            audioSource.Play();
        }

    }

    AudioSource findAudioSource()
    {
        for (int i = 0; i < SFXPlayers.Count; i++)
        {
            if (!SFXPlayers[i].isPlaying)
            {
                SFXPlayers[i].volume = SFXvolume;
                return SFXPlayers[i];
            }
        }

        GameObject temp = new GameObject();
        temp.name = "SFXPlayer";
        temp.transform.SetParent(transform);
        AudioSource audio = temp.AddComponent<AudioSource>();
        SFXPlayers.Add(audio);
        return audio;
    }

    public void ChangeSFXVolume(float volume)
    {
        SFXvolume = volume;
        foreach (AudioSource s in SFXPlayers)
        {
            s.volume = SFXvolume;
        }
    }
    #endregion
    #region BGM
    public void PlayBGM(string name)
    {
        AudioClip sfx;

        sfx = Resources.Load<AudioClip>("BGM/" + name);

        BGMPlayer.Stop();
        BGMPlayer.volume = BGMvolume;
        BGMPlayer.clip = sfx;
        BGMPlayer.Play();
    }

    public void PlayBGM(AudioClip Intro, AudioClip BGM)
    {
        StartCoroutine(co_PlayBGM(Intro, BGM));

    }

    public IEnumerator co_PlayBGM(AudioClip Intro, AudioClip BGM)
    {
        BGMPlayer.Stop();
        if (Intro != null)
        {
            IntroPlayer.clip = Intro;

            IntroPlayer.Play();
            while (IntroPlayer.isPlaying)
            {
                yield return null;
            }
        }

        BGMPlayer.volume = BGMvolume;
        BGMPlayer.clip = BGM;
        BGMPlayer.Play();
    }

    public Coroutine BGMFadeout()
    {
        return StartCoroutine(co_BGMFadeOut());
    }
    IEnumerator co_BGMFadeOut(float duration = 1)
    {
        float progress = 1f;

        while (progress >= 0)
        {
            BGMPlayer.volume = progress * BGMvolume;
            progress -= Time.deltaTime / duration;
            yield return null;
        }
        BGMPlayer.Stop();
    }


    public void StopBGM()
    {
        if (BGMPlayer == null) return;
        BGMPlayer.Stop();
    }

    public void ChangeBGMVolume(float volume)
    {
        BGMvolume = volume;
        BGMPlayer.volume = volume;
        IntroPlayer.volume = volume;
    }
    #endregion
}