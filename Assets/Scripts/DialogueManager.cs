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
    [SerializeField] CurtainEnding ending;
    [SerializeField] int bookUp;
    [SerializeField] int bookDown;
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
        if(/*!player.isBookMode && */player.isDialogueMode && Input.GetKeyDown(KeyCode.Space))
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
        //list outofrangeindex 에러 처리
        if (curDialNum < dialList.Count - 1) 
        {
            curDialNum++;
            if (stopDialNum.Contains(curDialNum)) 
            {
                player.isDialogueMode = false;
            }
            if (curDialNum==bookDown)
            {
                
                book.SetActive(true);
                //book.GetComponent<AutoFlip>().FlipRightPage();
                Debug.Log("book down");
            }
            if (bookDialNum.Contains(curDialNum))
            {
                book.GetComponent<AutoFlip>().FlipRightPage();
            }
            if (curDialNum == bookUp)
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
        if(enemy!=null)enemy.GetComponent<Animator>().SetTrigger("disappear");
        yield return new WaitForSeconds(2);
        if (ending) ending.Ending();
        else loader.LoadNextLevel();
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
