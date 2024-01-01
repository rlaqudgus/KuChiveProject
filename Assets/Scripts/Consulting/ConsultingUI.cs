using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConsultingUI : MonoBehaviour
{
    [SerializeField] Text FishText;
    [SerializeField] Text ChanceText;
    [SerializeField] GameObject TutorialPanel;
    [SerializeField] List<string> TutorialList;
    [SerializeField] SceneLoader SceneLoader;
    [SerializeField] BoatMovement Boat;
    Text TutorialText;
    int idx;

    private void Start()
    {
        idx = 0;
        TutorialText = TutorialPanel.transform.Find("TutorialText").gameObject.GetComponent<Text>();
        if (ConsultingManager.IsTutorial())
        {
            TutorialPanel.SetActive(true);
            StartCoroutine(Tutorial());
        }
    }
    void Update()
    {
        FishText.text = "COUNT: " + ConsultingManager.GetFish().ToString();
        ChanceText.text = "CHANCE: " + ConsultingManager.GetChance().ToString();
        if (ConsultingManager.GetFish() >= 3) StartCoroutine(Success());
        if (ConsultingManager.GetChance() < 1) StartCoroutine(Fail());
    }

    IEnumerator Tutorial()
    {
        while (idx < TutorialList.Count)
        {
            TutorialText.text = TutorialList[idx];
            yield return null;
            if(Input.GetKeyDown(KeyCode.Space)) idx++;
        }
        TutorialPanel.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        ConsultingManager.TutorialEnd();
    }

    IEnumerator Success()
    {
        Boat.alive = false;
        yield return new WaitForSeconds(1f);
        SceneLoader.LoadNextLevel();
    }
    IEnumerator Fail()
    {
        ConsultingManager.ResetFish();
        ConsultingManager.ResetChance();
        Boat.alive = false;
        yield return new WaitForSeconds(1f);
        SceneLoader.LoadCurrentLevel();
    }
}