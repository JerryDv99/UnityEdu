using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private float Speed;
    private Rigidbody Rigid = null;
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

        // Rigidbody 컴퍼넌트에 transform.forward 방향으로 500.0f 만큼 힘을 가한다
        Rigid.AddForce(transform.forward * 2500.0f);
    }

    /*
    // 일정한 간격으로 갱신
    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // -1 ~ +1 까지 실수 단위로 반환 
        //float fHor = Input.GetAxis("Horizontal");
        //float fVer = Input.GetAxis("Vertical");

        // -1, 0, 1
        float fHor = Input.GetAxisRaw("Horizontal");
        float fVer = Input.GetAxisRaw("Vertical");
        
        //Vector3 Movement = Vector3.forward * Speed;
        Vector3 Movement = new Vector3(fHor, 0.0f, fVer) * Speed * Time.deltaTime;
        transform.position += Movement;
    }
     */
}
