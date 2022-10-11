using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private float Speed;
    private Rigidbody Rigid = null;

    public Transform FirePoint;
    public GameObject BulletPrefab;
    public GameObject Bullet_FXPrefab;
    public float Power;
    public float Radius;
    public float Vib;

    bool BulletRender;

    public LayerMask TargetMask;


    // 런타임 이후에 처음 한번 실행
    // Start 함수보다 먼저 실행됨
    private void Awake()
    {
        // 현재 스크립트가 포함된 객체의 Rigidbody 컴퍼넌트를 받아옴.
        // Rigidbody 컴퍼넌트가 존재하지 않으면 아무것도 받아오지 않음.
        Rigid = this.GetComponent<Rigidbody>();       

        // 충돌처리를 진행하기 위해서는 아래 두 컴퍼넌트가 반드시 포함되어야 한다.
        // Rigidbody = 물리 엔진 : 움직이는 객체에 추가
        // Collider =  충돌체 : 충돌이 필요한 모든 객체에 추가
    }

    // 런타임 이후에 처음 한번 실행
    void Start()
    {
        Speed = 5.0f;
        Power = 0;
        
        BulletRender = false;
    }

    void Update()
    {
        // -1 ~ +1 까지 실수 단위로 반환 
        float fHor = Input.GetAxis("Horizontal");
        float fVer = Input.GetAxis("Vertical");

        //Vector3 Movement = Vector3.forward * Speed;
        Vector3 Movement = new Vector3(fHor, 0.0f, fVer) * Speed * Time.deltaTime;
        transform.position += Movement;

        // 버튼이 입력되면 Power 초기화
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vib = 0;
            Radius = 0;
            Power = 0;
        }
        // 버튼을 누르고 있는 동안 Power의 값을 누적시킴
        if (Input.GetKey(KeyCode.Space))
        {
            Power += Time.deltaTime;
            Radius += Time.deltaTime;
            Vib += Time.deltaTime;
        }
        // 버튼을 놓았을때
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // 타겟의 정보를 받아온다
            RaycastHit hit;

            // 가상의 선을 발사해서 충돌이 있다면 hit 대상을 미리 확인한다
            if (Physics.Raycast(FirePoint.position, FirePoint.transform.forward, out hit, Mathf.Infinity, TargetMask))
            {
                Vector3 offset = new Vector3(
                   Mathf.Cos(90 * Mathf.Deg2Rad),
                   Mathf.Sin(90 * Mathf.Deg2Rad),
                    0.1f) * Radius + transform.position;

                Vector3 Vibrater = new Vector3(
                    Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0.0f) * Vib;

                if (BulletRender)
                {
                    // 총알을 복제
                    GameObject obj = Instantiate(BulletPrefab);

                    // hit.point = 현재 hit의 위치
                    // 현재 총알의 위치를 hit.point로 초기화
                    obj.transform.position = offset + Vibrater;

                    Rigidbody Rigid = obj.GetComponent<Rigidbody>();

                    Rigid.AddForce(FirePoint.transform.forward * 1000);

                    Debug.DrawLine(FirePoint.position, offset + Vibrater, Color.red);
                }
                else
                {
                    Debug.DrawLine(FirePoint.position, offset + Vibrater + hit.point, Color.red);
                }

                if (Input.GetKey(KeyCode.E))
                {
                    //EnemyManager.GetInstance()
                }
            }
        }
    }
}
