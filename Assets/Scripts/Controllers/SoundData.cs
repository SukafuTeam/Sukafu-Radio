using UnityEngine;
using System.Collections;

public class SoundData : MonoBehaviour {

    public static SoundData Data;
    public AudioSource Source1;
    public AudioSource Source2;

    // Use this for initialization
    public void Awake () {
        if(Data == null)
        {
            Data = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            StopAll();
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {

    }

    public static void Play(int sound)
    {
        if (SoundData.Data == null)
        {
            return;
        }

        if (sound == 1)
        {
            SoundData.Data.Source1.Play();
            SoundData.Data.Source2.Stop();
        }
        else
        {
            SoundData.Data.Source2.Play();
            SoundData.Data.Source1.Stop();
        }
    }

    public static void PlayAll()
    {
        if (SoundData.Data == null)
        {
            return;
        }

        SoundData.Data.Source1.Play();
        SoundData.Data.Source2.Play();
    }

    public static void StopAll()
    {
        if (SoundData.Data == null)
        {
            return;
        }

        SoundData.Data.Source1.Stop();
        SoundData.Data.Source2.Stop();
    }

    public static void Stop(int sound)
    {
        if (SoundData.Data == null)
        {
            return;
        }

        if (sound == 1)
        {
            SoundData.Data.Source1.Stop();
        }
        else
        {
            SoundData.Data.Source2.Stop();
        }
    }

    public static void ChangeClip(int sound, AudioClip clip)
    {
        if (SoundData.Data == null)
        {
            return;
        }

        if (sound == 1)
        {
            SoundData.Data.Source1.clip = clip;
        }
        else
        {
            SoundData.Data.Source2.clip = clip;
        }
    }

    public static void SetGameplay(int style)
    {
        if (SoundData.Data == null)
        {
            return;
        }

        Data.StartCoroutine(Data.FadeSong(style));
    }

    public IEnumerator FadeSong(int style)
    {
        if (style == 1)
        {
            while (Data.Source1.volume < 1 || Data.Source2.volume > 0)
            {
                Data.Source1.volume += 0.02f;
                Data.Source2.volume -= 0.02f;
                yield return null;
            }
        }
        else
        {
            while (Data.Source2.volume < 1 || Data.Source1.volume > 0)
            {
                Data.Source2.volume += 0.02f;
                Data.Source1.volume -= 0.02f;
                yield return null;
            }
        }
    }
}
