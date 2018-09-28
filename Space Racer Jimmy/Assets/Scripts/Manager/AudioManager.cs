using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Pour le singleton, static veux dire qu'on peux y accéder de partout.
    private static AudioManager m_Instance;
    //Içi on fait un "get" public, pour que les autres puissent accéder au singleton sans pouvoir l'assigner
    public static AudioManager Instance
    {
        get
        {
            return m_Instance;
        }
    }


    [SerializeField]
    private AudioSource m_AudioSourceMusic;
    [SerializeField]
    private AudioClip m_MusicMenu;
    [SerializeField]
    private AudioClip m_MusicGame;

    //[SerializeField]
    //private GameObject m_HitTheWall;

    private void Awake()
    {
        //Içî on s'assure qu'il n'y en ai qu'un seul
        if (m_Instance != null)
        {
            //On détruit le deuxième
            Destroy(gameObject);
        }
        else
        {
            //On assigne le premier à notre "static"
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
        /*else if (aAudioSource == "MusicEnd")
        {
            m_AudioSourceMusic.clip = m_MusicEnd;
            m_AudioSourceMusic.Play();
        }*/
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
        Debug.Log("Play");
    }
}



