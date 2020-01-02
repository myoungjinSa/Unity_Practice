using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    //총알의 파괴력
    public float damage = 0.0f;

    //총알의 속도
    public float speed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //월드 좌표의 z축 방향으로 날아감
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);

       // Debug.Log("발사");
        //오브젝트의 로컬 좌표의 z축 방향으로날아감 => GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
