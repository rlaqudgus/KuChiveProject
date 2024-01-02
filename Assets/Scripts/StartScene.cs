using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    [SerializeField] GameObject Text;
    [SerializeField] SceneLoader loader;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TEXT());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) loader.LoadNextLevel();
    }

    IEnumerator TEXT()
    {
        while(true){
            if (Text.activeSelf) Text.SetActive(false);
            else Text.SetActive(true);
            yield return new WaitForSeconds(1);
        }
    }
}