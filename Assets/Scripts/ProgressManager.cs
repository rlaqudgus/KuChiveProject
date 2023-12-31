using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    public Image fillImg;
    public Image boat;
    [SerializeField] int ProgressTime;
    // Start is called before the first frame update
    void Start()
    {
        //fillImg.rectTransform.offsetMin.x = 700;
        //StartCoroutine(fillGauge());
    }

    // Update is called once per frame
    void Update()
    {
        //fillImg.fillAmount += Time.deltaTime/ProgressTime;
        Vector2 rect = new Vector2(fillImg.rectTransform.offsetMax.x + ((Time.deltaTime / ProgressTime) * 700), 0);
        fillImg.rectTransform.offsetMax = rect;
    }

    IEnumerator fillGauge()
    {
        while (fillImg.fillAmount!=1) 
        {
            fillImg.fillAmount += Time.deltaTime;

            yield return new WaitForSeconds(0.5f);
        }
    }
}
