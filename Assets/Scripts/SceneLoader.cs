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
        if (deleteStart) return;
        animator.SetTrigger("End");
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int sceneIndex)
    {
        if (!deleteEnd)
        {
            animator.SetTrigger("Start");

            yield return new WaitForSeconds(transitionTime);
        }
        
        SceneManager.LoadScene(sceneIndex);
    }
}
