using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurtainEnding : MonoBehaviour
{
    [SerializeField] float CurtainSpd;
    [SerializeField] float UpSpd;
    RectTransform Rect;
    GameObject tail;
    bool GameEnd;
    private void Start()
    {
        GameEnd = false;
        Rect = GetComponent<RectTransform>();
        tail = transform.Find("tail").gameObject;
        tail.SetActive(false);
    }

    void Update()
    {
        if(Rect.localPosition.y > 1500 && GameEnd)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(1);
                ConsultingManager.ResetChance();
                ConsultingManager.ResetFish();
                ConsultingManager.ResetTutorial();
            }
        }
    }
    public void Ending()
    {
        StartCoroutine(ENDING());
    }

    IEnumerator ENDING()
    {
        while (Rect.localPosition.y > 0)
        {
            Rect.localPosition = new Vector2(0, Rect.localPosition.y - CurtainSpd * Time.deltaTime * 100);
            yield return null;
        }

        tail.SetActive(true);
        yield return new WaitForSeconds(2f);

        GameEnd = true;
        while (Rect.localPosition.y < 1800)
        {
            Rect.localPosition = new Vector2(0, Rect.localPosition.y + UpSpd * Time.deltaTime * 100);
            yield return null;
        }
    }
}
