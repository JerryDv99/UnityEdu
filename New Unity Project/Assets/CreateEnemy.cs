using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    // private ��Ҹ� �ν����ͺ信 ��Ÿ��
    [SerializeField] private GameObject EnemyPrefab;


    private void Awake()
    {       
    }

    // �ڷ�ƾ �Լ�
    IEnumerator Create()
    {
        
        // 0.5�� �Ŀ� �Լ��� �����
        yield return new WaitForSeconds(0.5f);

        GameObject Obj = Instantiate(EnemyPrefab);

        /*
        Renderer renderer = Obj.transform.Find("chest_low").GetComponent<Renderer>();

        Color color = renderer.material.color;

        color.a = 0.0f;
         */

        Obj.transform.position = transform.position;

        // GameObject.Find("ObjectName") = "ObjectName"�� GameObject�� ��ȯ
        // Obj.transform.parent = ���������� �������ش�
        // parent = �θ� ����
        Obj.transform.parent = GameObject.Find("EnemyList").transform;

        // ������ ������Ʈ�� EnemyManager Ŭ������ ����
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

    // �浹�� �����Ǹ� ����Ǵ� �浹���� �Լ�, Collider �ʿ�
    private void OnCollisionEnter(Collision collision)
    {
        // �ڷ�ƾ �Լ��� ����
        StartCoroutine(Create());
    }
}
