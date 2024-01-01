using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//걍 여기서 다 하자 shit
public class CirclularMovement : MonoBehaviour
{
    [SerializeField] Transform rotationCenter;
    [SerializeField] int objectNum = 4;
    [SerializeField] float rotationRadius = 2f, angularSpeed = 2f;
    [SerializeField] RunGamePlayer player;
    [SerializeField] GameObject target;
    [SerializeField] GameObject staticTarget;
    [SerializeField] ProgressManager pm;
    Vector3 targetPos;
    Vector3 shootPos;
    Animator animator;

    private float[] angles;

    private void Start()
    {
        animator = GetComponent<Animator>();  
        
        
        angles = new float[objectNum];

        for (int i = 0; i < objectNum; i++) 
        {
            angles[i] = i * 2 * Mathf.PI / objectNum;
        }

        StartCoroutine(StartBossAtk());
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < objectNum; i++)
        {
            float x = Mathf.Cos(angles[i]) * rotationRadius;
            float y = Mathf.Sin(angles[i]) * rotationRadius;

            transform.GetChild(i).position = rotationCenter.position + new Vector3(x, y, 0f);

            angles[i] += angularSpeed * Time.deltaTime;

            if (angles[i] > Mathf.PI * 2)
            {
                angles[i] -= Mathf.PI * 2;
            }
        }
        FollowTarget();

        if (pm.isFinished)
        {
            StopAllCoroutines();
            target.SetActive(false);
            staticTarget.SetActive(false);
        }
    }

    void FollowTarget()
    {
        target.transform.position = Vector3.MoveTowards(target.transform.position, player.transform.position, Time.deltaTime*3);
        targetPos = target.transform.position;
    }

    IEnumerator StartBossAtk()
    {
        while(true)
        {
            target.gameObject.SetActive(false);

            yield return new WaitForSeconds(5);
            //다시 켜기
            for (int i = 0; i < 4; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            animator.SetTrigger("spawn");

            for (int i = 0; i < objectNum; i++)
            {
                target.gameObject.SetActive(true);

                yield return new WaitForSeconds(4);

                Vector3 playerPos = player.transform.position;
                GameObject currentShoot = transform.GetChild(i).gameObject;
                //1초 전에 있었던 곳에 쏘기 플레이어가 피할 수 있어야하니까
                target.gameObject.SetActive(false);
                shootPos = targetPos;
                staticTarget.transform.position = shootPos;
                staticTarget.gameObject.SetActive(true);

                yield return new WaitForSeconds(1);

                Vector3 shootDir = shootPos - currentShoot.transform.position;
                GameObject newobj = Instantiate(currentShoot, currentShoot.transform.position, Quaternion.identity);

                newobj.GetComponent<Rigidbody2D>().AddForce(shootDir * 4, ForceMode2D.Impulse);
                newobj.GetComponent<TrailRenderer>().enabled = true;

                currentShoot.SetActive(false);
                staticTarget.gameObject.SetActive(false);
            }

            animator.SetTrigger("retreat");
        }
        
    }

    void Shoot(int i)
    {
        //player position 
        Vector3 playerPos = player.transform.position;
        GameObject currentShoot = transform.GetChild(i).gameObject;
        Vector3 shootDir = playerPos - currentShoot.transform.position;

        GameObject newobj = Instantiate(currentShoot, currentShoot.transform.position, Quaternion.identity);

        newobj.GetComponent<Rigidbody2D>().AddForce(shootDir*5, ForceMode2D.Impulse);
        newobj.GetComponent<TrailRenderer>().enabled = true;

        currentShoot.SetActive(false);
    }
}
