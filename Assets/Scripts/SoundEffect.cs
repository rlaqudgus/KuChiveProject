using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        StartCoroutine(SceneBGMFadeIn());
    }

    IEnumerator SceneBGMFadeIn()
    {
        audioSrc.volume = 0;

        float speed = 0.05f;

        while (audioSrc.volume < 0.4f)
        {
            audioSrc.volume += speed;

            yield return new WaitForSeconds(0.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
