using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private const string bulletTag = "BULLET";
    private float initHp = 100.0f;
    public float currHp;

    // Start is called before the first frame update
    void Start()
    {
        currHp = initHp;    
    }

    //충돌한 Collider 의 IsTrigger 옵션이 체크됐을 때 발생
    void OnTriggerEnter(Collider coll)
    {
        //충돌한 Collider 의 태그가 BULLET이면 Player의 currHp를 차감
        if(coll.tag == bulletTag)
        {
            Destroy(coll.gameObject);

            currHp -= 5.0f;
            Debug.Log("Player HP = " + currHp.ToString());

            //Player 의 생명이 0이하이면 사망 처리
            if(currHp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }


    //Player의 사망처리 루틴
    void PlayerDie()
    {
        Debug.Log("PlayerDie !");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
