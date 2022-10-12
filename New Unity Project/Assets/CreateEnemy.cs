using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    // private 요소를 인스펙터뷰에 나타냄
    [SerializeField] private GameObject EnemyPrefab;


    private void Awake()
    {       
    }

    // 코루틴 함수
    IEnumerator Create()
    {
        
        // 0.5초 후에 함수를 재실행
        yield return new WaitForSeconds(0.5f);

        GameObject Obj = Instantiate(EnemyPrefab);

        /*
        Renderer renderer = Obj.transform.Find("chest_low").GetComponent<Renderer>();

        Color color = renderer.material.color;

        color.a = 0.0f;
         */

        Obj.transform.position = transform.position;

        // GameObject.Find("ObjectName") = "ObjectName"의 GameObject를 반환
        // Obj.transform.parent = 계층구조를 설정해준다
        // parent = 부모를 설정
        Obj.transform.parent = GameObject.Find("EnemyList").transform;

        // 생성된 오브젝트를 EnemyManager 클래스에 보관
        EnemyManager.Instance.AddObject(Obj);

        /*
        float rColor = 0.0f;

        while(true)
        {
            yield return null;

            rColor += Time.deltaTime;

            color.a = rColor;

            if (rColor >= 255.0f) break;
        }
         */
    }

    // 충돌이 감지되면 실행되는 충돌감지 함수, Collider 필요
    private void OnCollisionEnter(Collision collision)
    {
        // 코루틴 함수를 실행
        StartCoroutine(Create());
    }
}
