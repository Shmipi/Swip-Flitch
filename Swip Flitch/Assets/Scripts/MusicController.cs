using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] public float songLength;

    
    private AudioSource aSV1;
    private AudioSource aSV2;
    
    
    public AudioClip[] audioClips;
    private GameObserver gameObserver;


    void Start()
    {
        gameObserver = FindObjectOfType<GameObserver>();
        aSV1 = gameObject.AddComponent<AudioSource>();
        aSV2 = gameObject.AddComponent<AudioSource>();
        

        aSV1.clip = audioClips[0];
        aSV2.clip = audioClips[1];
        

        aSV1.volume = 1f;
        aSV1.loop = true;
        aSV2.volume = 0f;
        aSV2.loop = true;
        

        aSV1.Play();
        aSV2.Play();
    }

    // Update is called once per frame
    void Update()
    {

        if (gameObserver.flipped == true)
        {
            //doubleJump.PlayOneShot(audioClips[2]);
            ChangeTrack(aSV2, aSV1);
        } else
        {
            //doubleJump.PlayOneShot(audioClips[2]);
            ChangeTrack(aSV1, aSV2);
        }
    }


    private void ChangeTrack(AudioSource fadeIn, AudioSource fadeOut)
    {
        StartCoroutine(StartFade(fadeIn, 0.1f, 1f));
        StartCoroutine(StartFade(fadeOut, 0.1f, 0f));

        //StartFade(audioSourceToFade, durationOfFade, targetVolume);

    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        if (start == targetVolume)
        {
            yield break;
        }
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
