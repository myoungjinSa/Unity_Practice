using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //인스펙터 뷰에 여러 속성이 나열될 경우 Header 어트리뷰트에 제목을 표시해서 가독성을 높일 수 있음
    [Header("Enemy Create Info")]

    //생성할 총알 프리팹
    public GameObject bulletPrefab;

    //오브젝트 풀에 생성할 개수
    public int maxPool = 10;

    public List<GameObject> bulletPool = new List<GameObject>();


    //적 캐릭터가 출현할 위치를 담을 배열
    public Transform[] points;

    //적 캐릭터 프리팹을 저장할 변수
    public GameObject enemy;

    //적 캐릭터를 생성할 주기
    public float creatTime = 2.0f;

    //적 캐릭터의 최대 생성 개수
    public int maxEnemy = 10;

    //게임 종료 여부를 판단할 변수
    public bool isGameOver = false;

    //싱글턴에 접근하기 위한 Static 변수 선언
    public static GameManager instance = null;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this) 
        {
            // instance 에 할당된 클래스의 인스턴스가 다를 경우 새로 생성된 클래스를 의미함
            // 다른씬으로 전환했다가 다시 원래의 씬으로 되돌아 왔을 때 다른 GameManager 스크립트의 Awake가 실행된다. 이 때 instance 변수는 static 이기 때문에 이미 값이 들어있다.

            Destroy(this.gameObject);
        }

        //한 씬에서 다른 씬이 호출되면 기본적으로는 현재 씬에 있는 모든 게임오브젝트가 삭제된다. 
        //만약 다른씬으로 넘어가도 계속 유지되길 원하는 게임오브젝트가 있다면 DontDestroyOnLoad 함수를 사용함
        //다른 씬으로 넘어가더라도 삭제하지 않고 유지함
        DontDestroyOnLoad(this.gameObject);


        //오브젝트 풀링 생성함수 호출
        CreatePooling();
        
    }

    public GameObject GetBullet()
    {
        for(int i = 0;i<bulletPool.Count;i++)
        {
            //비활성화 여부로 사용 가능한 총알인지를 판단
            if(bulletPool[i].activeSelf == false)
            {
                return bulletPool[i];
            }
        }

        return null;
    }

    //오브젝트 풀에 총알을 생성하는 함수
    public void CreatePooling()
    {
        //총알을 생성해 차일드화할 페어런트 게임오브젝트를 생성
        GameObject objectPools = new GameObject("ObjectPools");

        //풀링 개수만큼 미리 총알을 생성
        for(int i = 0;i < maxPool; i++)
        {
            var obj = Instantiate<GameObject>(bulletPrefab, objectPools.transform);

            obj.name = "Bullet_" + i.ToString("00");

            //비 활성화
            obj.SetActive(false);

            //리스트에 생성한 총알 추가
            bulletPool.Add(obj);

        }


    }

    IEnumerator CreateEnemy()
    {
        //게임 종료 시까지 무한 루프
        while(!isGameOver)
        {
            //현재 생성된 적 캐릭터의 개수 산출
            int enemyCount = (int)GameObject.FindGameObjectsWithTag("ENEMY").Length;

            //적 캐릭터의 최대 생성 개수보다 작을 때만 적 캐릭터를 생성
            if(enemyCount < maxEnemy)
            {
                //적 캐릭터의 생성 주기 시간만큼 대기
                yield return new WaitForSeconds(creatTime);

                //불 규칙 적인 위치 산출
                int idx = Random.Range(1, points.Length);

                //적 캐릭터의 동적 생성
                Instantiate(enemy, points[idx].position, points[idx].rotation);

            }
            else
            {
                yield return null;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //하이러키 뷰의 SpawnPointGroup 을 찾아 하위에 있는 모든 Transform 컴포넌트를 찾아옴
        points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();

        if(points.Length > 0)
        {
            StartCoroutine(this.CreateEnemy());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
