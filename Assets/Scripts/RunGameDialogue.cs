using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunGameDialogue : MonoBehaviour
{
    [SerializeField] List<string> dialList = new List<string>();
    [SerializeField] List<Image> imageList = new List<Image>();
    [SerializeField] Text dialText;
    [SerializeField] TileManager tm;
    [SerializeField] GameObject Enemy;
    [SerializeField] GameObject player;
    [SerializeField] ProgressManager pm;
    Animator ani;
    public int curDialNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextDialogue();
        }
    }

    void NextDialogue()
    {
        if (curDialNum<dialList.Count - 1)
        {
            curDialNum++;
            SwitchDialogue();
            SwitchImage();
            SwitchEffects();
        }
        
    }

    void SwitchDialogue()
    {
        dialText.text = dialList[curDialNum];
    }

    void SwitchImage()
    {
        foreach (Image image in imageList)
        {
            image.gameObject.SetActive(false);
        }
        imageList[curDialNum].gameObject.SetActive(true);
    }

    void SwitchEffects()
    {
        if (curDialNum==8)
        {
            ani.SetTrigger("up");
            tm.TileSpeed = 5;
            Enemy.GetComponent<CirclularMovement>().enabled = true;
            pm.enabled = true;
            player.SetActive(true);
        }
    }
}
