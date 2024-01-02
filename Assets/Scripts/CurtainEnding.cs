using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurtainEnding : MonoBehaviour
{
    [SerializeField] float CurtainSpd;
    [SerializeField] float UpSpd;
    [SerializeField] Canvas canvas;
    [SerializeField] Transform camera_t;
    RectTransform PanelRectTransform;
    RectTransform EndRectTransform;
    GameObject tail;
    bool GameEnd;
    private void Start()
    {
        GameEnd = false;
        tail = transform.Find("tail").gameObject;
        PanelRectTransform = canvas.transform.Find("TextPanel").GetComponent<RectTransform>();
        EndRectTransform = canvas.transform.Find("EndingPanel").GetComponent <RectTransform>();
        tail.SetActive(false);
    }

    void Update()
    {
        if(transform.position.y > 9 && GameEnd)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(1);
                ConsultingManager.ResetChance();
                ConsultingManager.ResetFish();
                ConsultingManager.ResetTutorial();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

            }
        }
    }
    public void Ending()
    {
        if (camera_t != null) transform.position = new Vector2(camera_t.position.x, 11);
        StartCoroutine(ENDING());
    }

    IEnumerator ENDING()
    {
        StartCoroutine(ENDTEXT());
        while (transform.position.y >0)
        {
            transform.position = new Vector2(camera_t.position.x, transform.position.y - CurtainSpd * Time.deltaTime);
            yield return null;
        }

        tail.SetActive(true);
        yield return new WaitForSeconds(2f);

        StartCoroutine(CREDIT());
        GameEnd = true;
        while (transform.position.y < 11)
        {
            transform.position = new Vector2(camera_t.position.x, transform.position.y + UpSpd * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator CREDIT()
    {
        while (PanelRectTransform.localPosition.y < 100)
        {
            EndRectTransform.localPosition = new Vector2(0, EndRectTransform.localPosition.y + UpSpd * Time.deltaTime * 100);
            PanelRectTransform.localPosition = new Vector2(0, PanelRectTransform.localPosition.y + UpSpd * Time.deltaTime * 100);
            yield return null;
        }
    }

    IEnumerator ENDTEXT()
    {
        while (EndRectTransform.localPosition.y > -400)
        {
            EndRectTransform.localPosition = new Vector2(0, EndRectTransform.localPosition.y - CurtainSpd * Time.deltaTime * 50);
            yield return null;
        }
    }
}
