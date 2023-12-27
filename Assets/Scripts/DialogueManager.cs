using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] List<string> dialList = new List<string>();
    [SerializeField] List<Image> imageList = new List<Image>();
    bool switchDialogue;
    public int curDialNum = 0;
    [SerializeField] Text dialogueText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        KeyInput();
        CurDialSelect();
        CurDialPrint();
        CurImagePrint();
    }

    void KeyInput()
    {
        switchDialogue = Input.GetKeyDown(KeyCode.Space);
    }

    void CurDialSelect()
    {
        //list outofrangeindex 에러 처리
        if (switchDialogue && curDialNum < dialList.Count - 1) 
        {
            curDialNum++;
        }
    }

    void CurDialPrint()
    {
        dialogueText.text = dialList[curDialNum];
    }

    void CurImagePrint()
    {
        if (switchDialogue)
        {
            foreach (Image image in imageList) 
            {
                image.gameObject.SetActive(false);
            }
            imageList[curDialNum].gameObject.SetActive(true);
        }
        
    }
}
