using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator animator;
    [SerializeField] float transitionTime;
    [SerializeField] bool deleteStart;
    [SerializeField] bool deleteEnd;
    [SerializeField] GameObject sound;
    AudioSource audioSrc;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            LoadNextLevel();
        }
    }

    private void Start()
    {
        audioSrc = sound.GetComponent<AudioSource>();
        if (deleteStart) return;
        animator.SetTrigger("End");
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void LoadCurrentLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }
    IEnumerator LoadLevel(int sceneIndex)
    {
        if (!deleteEnd)
        {
            animator.SetTrigger("Start");
            StartCoroutine(SceneBGMFadeOut());

            yield return new WaitForSeconds(transitionTime);
        }

        SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator SceneBGMFadeOut()
    {
        float speed = 0.05f;

        while (audioSrc.volume != 0)
        {
            audioSrc.volume -= speed;

            yield return new WaitForSeconds(0.1f);
        }
    }

}   
