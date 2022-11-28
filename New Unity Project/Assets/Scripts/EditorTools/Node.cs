using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 자동으로 컴퍼넌트 삽입
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Node : MonoBehaviour
{
    [HideInInspector] public Node next;
    private bool Check;
    private void Awake()
    {
        // 물리엔진을 받아온다
        Rigidbody Rigid =  GetComponent<Rigidbody>();

        // 받아온 물리엔진의 x 와 z 좌표를 고정시킨다
        Rigid.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

        // 충돌체를 받아온다
        SphereCollider coll = GetComponent<SphereCollider>();
        coll.radius = 0.2f;
    }

    IEnumerator Start()
    {
        Check = true;
        while(Check)
        {
            transform.position = new Vector3(
                Random.Range(-15, 15), 25.0f, Random.Range(-15, 15));            
            
            yield return new WaitForSeconds(2.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Ground")
            Check = false;
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, next.transform.position, Color.blue);
    }
}
