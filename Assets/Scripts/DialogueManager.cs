using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] List<string> dialList = new List<string>();
    [SerializeField] List<Image> imageList = new List<Image>();
    [SerializeField] PlayerMovement player;
    [SerializeField] GameObject book;
    [SerializeField] GameObject enemy;
    [SerializeField] SceneLoader loader;
    [SerializeField] int bookUp;
    [SerializeField] int nextScene;
    public bool switchDialogue;
    public int curDialNum = 0;
    public List<int> stopDialNum = new List<int>();
    public List<int> bookDialNum = new List<int>();
    [SerializeField] Text dialogueText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.isBookMode && player.isDialogueMode && Input.GetKeyDown(KeyCode.Space))
        {
            //KeyInput();
            //CurDialSelect();
            //CurDialPrint();
            //CurImagePrint();
            NextDialogue();
        }
        
    }

    void KeyInput()
    {
        switchDialogue = Input.GetKeyDown(KeyCode.Space);
    }
    public void NextDialogue()
    {
        CurDialSelect();
        CurDialPrint();
        CurImagePrint();
    }
    void CurDialSelect()
    {
        //list outofrangeindex ���� ó��
        if (curDialNum < dialList.Count - 1) 
        {
            curDialNum++;
            if (stopDialNum.Contains(curDialNum)) 
            {
                player.isDialogueMode = false;
            }
            if (bookDialNum.Contains(curDialNum))
            {
                if (player.isBookMode==true)
                {
                    player.isBookMode = false;
                    player.isDialogueMode = true;
                    
                    return;
                }
                player.isBookMode = true;
                book.SetActive(true);
                Debug.Log("book down");
            }
            if(curDialNum == bookUp)
            {
                book.GetComponent<Animator>().SetTrigger("Up");
            }
            if (curDialNum == nextScene)
            {
                StartCoroutine(next());
            }
        }
    }

    IEnumerator next()
    {
        enemy.GetComponent<Animator>().SetTrigger("disappear");
        yield return new WaitForSeconds(2);
        loader.LoadNextLevel();
    }

    void CurDialPrint()
    {
        dialogueText.text = dialList[curDialNum];
    }

    void CurImagePrint()
    {
            foreach (Image image in imageList) 
            {
                image.gameObject.SetActive(false);
            }
            imageList[curDialNum].gameObject.SetActive(true);
        
    }

   
}
