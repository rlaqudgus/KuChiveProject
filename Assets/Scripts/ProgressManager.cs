using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    public Image fillImg;
    public Image boat;
    [SerializeField] int ProgressTime;
    [SerializeField] TileManager tm;
    public bool isFinished;
    [SerializeField] GameObject tileParent;
    [SerializeField] GameObject boss;
    [SerializeField] RunGamePlayer player;
    [SerializeField] SceneLoader loader;
    // Start is called before the first frame update
    void Start()
    {
        //fillImg.rectTransform.offsetMin.x = 700;
        //StartCoroutine(fillGauge());
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinished) return;
        //bar가 특정 부분 이상 넘어가면 게임 종료
        fillGauge();
        
    }

    void fillGauge()
    {
        Vector2 rect = new Vector2(fillImg.rectTransform.offsetMax.x + ((Time.deltaTime / ProgressTime) * 700), 0);
        fillImg.rectTransform.offsetMax = rect;

        if (fillImg.rectTransform.offsetMax.x > 0)
        {
            fillImg.rectTransform.offsetMax = new Vector2(0, 0);

            isFinished = true;

            StartCoroutine(next());
        }
    }

    IEnumerator next()
    {
        tm.TileSpeed = 0;

        yield return new WaitForSeconds(1);


        boss.GetComponent<Animator>().SetTrigger("fade");

        for (int i = 0; i < 2; i++)
        {
            int count = tileParent.transform.GetChild(i).childCount;

            for (int j = 0; j < count; j++)
            {

                StartCoroutine(FirstEnemyfade(i,j));
            }

        }
        yield return new WaitForSeconds(1);

        StartCoroutine(PlayerForward());

        yield return new WaitForSeconds(2);

        loader.LoadNextLevel();

    }

    IEnumerator PlayerForward()
    {
        while (true) 
        {
            Vector3 moveVec = new Vector3(player.transform.position.x + (0.1f), player.transform.position.y, player.transform.position.z);
            player.transform.position = moveVec;

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator FirstEnemyfade(int i, int j)
    {
        while (true)
        {
            float aValue = tileParent.transform.GetChild(i).GetChild(j).GetComponent<SpriteRenderer>().color.a;
            Color fadeColor = new Color(255, 255, 255, aValue - 0.1f);
            tileParent.transform.GetChild(i).GetChild(j).GetComponent<SpriteRenderer>().color = fadeColor;
            if (tileParent.transform.GetChild(i).GetChild(j).gameObject.name=="RunGameEnemy")
            {
                tileParent.transform.GetChild(i).GetChild(j).GetChild(0).gameObject.SetActive(false);
            }
            

            yield return new WaitForSeconds(0.1f);
        }
        
    }

    IEnumerator BossFade()
    {
        while (true) 
        {
            float aValue = boss.GetComponent<SpriteRenderer>().color.a;
            Color fadeColor = new Color(255, 255, 255, aValue - 0.1f);
            boss.GetComponent <SpriteRenderer>().color = fadeColor;

            yield return new WaitForSeconds(0.1f);
        }
    }
   
}
