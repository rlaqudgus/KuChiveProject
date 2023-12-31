using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsultingUI : MonoBehaviour
{
    [SerializeField] Text FishText;

    // Update is called once per frame
    void Update()
    {
        FishText.text = "COUNT: " + ConsultingManager.GetFish().ToString();
    }
}
