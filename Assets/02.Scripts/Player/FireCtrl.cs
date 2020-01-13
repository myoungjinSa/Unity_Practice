using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//총알 발사와 재장전 오디오 클립을 저장할 구조체
[System.Serializable]
public struct PlayerSfx
{
    public AudioClip[] fire;
    public AudioClip[] reload;

}



public class FireCtrl : MonoBehaviour
{
    //무기 타입
    public enum WeaponType
    {
        RIFLE = 0,
        SHOTGUN
    }

    //주인공이 현재 들고 있는 무기를 저장할 변수
    public WeaponType currWeapon = WeaponType.RIFLE;



    //총알 프리팹
    public GameObject bullet;

    //탄피 추출 파티클
    public ParticleSystem cartridge;
    //총구 화염 파티클
    private ParticleSystem muzzleFlash;

    //AudioSource 컴포넌트를 저장할 변수. 언더 스코어를 붙이는 이유는 예전 유니티에서 제공하던 AudioSource의 프로퍼티였기 때문이다. 
    //따로 붙이지않으면 현재 유니티는 경고 문구를 냄. 경고 문구가 특별히 거슬리지 않으면 붙이지않아도 됨
    private AudioSource _audio;

    //총알 발사 좌표
    public Transform firePos;

    //오디오 클립을 저장할 변수
    public PlayerSfx playerSfx;

    //Shake 클래스를 저장할 추출
    private Shake shake;
    


    void Start()
    {
        //FirePos 하위에 있는 컴포넌트 추출
        muzzleFlash = firePos.GetComponentInChildren<ParticleSystem>();

        //AudioSource 컴포넌트 추출
        _audio = GetComponent<AudioSource>();

        //Shake 스크립트 추출
        shake = GameObject.Find("CameraRig").GetComponent<Shake>();
        

    }

    void Update()
    {
        //마우스 왼쪽 버튼을 클릭했을 때 Fire함수 호출       //0 :왼쪽 버튼, 1: 오른쪽 버튼 ,2: 가운데 버튼
        if(Input.GetMouseButtonDown(0))
        {
           // Debug.Log("발사");
            Fire();
        }
    }
    void Fire()
    {
        //셰이크 효과 추출
        StartCoroutine(shake.ShakeCamera());

        //Instantiate(총알 프리팹,총알 생성 위치,총알 각도)
        //Bullet 프리팹을 동적으로 생성
        Instantiate(bullet, firePos.position, firePos.rotation);

        //파티클 실행
        cartridge.Play();


        //총구 화염 파티클 실행
        muzzleFlash.Play();

        //사운드 발생
        FireSfx();
    }

    void FireSfx()
    {
        //현재 들고 있는 무기의 오디오 클립을 가져옴
        var _sfx = playerSfx.fire[(int)currWeapon];
        //사운드 발생
        _audio.PlayOneShot(_sfx, 1.0f);

    }
}
