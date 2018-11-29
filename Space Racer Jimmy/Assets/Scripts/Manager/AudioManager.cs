using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    private AudioSource m_AudioSourceMusic;
    [SerializeField]
    private AudioClip m_MusicMenu;
    [SerializeField]
    private AudioClip m_MusicGame;

    private static AudioManager m_Instance;
    public static AudioManager Instance
    {
        get
        {
            return m_Instance;
        }
    }

    private void Awake()
    {
        if (m_Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            m_Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(string aAudioSource)
    {
        if (m_AudioSourceMusic != null)
        {
            m_AudioSourceMusic.Stop();
        }

        if (aAudioSource == "MusicMenu")
        {
            m_AudioSourceMusic.clip = m_MusicMenu;
            m_AudioSourceMusic.Play();
        }
        else if (aAudioSource == "MusicGame")
        {
            m_AudioSourceMusic.clip = m_MusicGame;
            m_AudioSourceMusic.Play();
        }
    }

    public void StopMusic()
    {
        if (m_AudioSourceMusic != null)
        {
            m_AudioSourceMusic.Stop();
        }
    }

    public void PlaySFX(AudioClip aClip, Vector3 aPosition)
    {
        GameObject audio = PoolManager.Instance.GetFromPool(EPoolType.HitSFX, aPosition);
        audio.GetComponent<SFXAudio>().Setup(aClip);
        audio.GetComponent<SFXAudio>().Play();
    }
}



