using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ڵ����� ���۳�Ʈ ����
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Node : MonoBehaviour
{
    [HideInInspector] public Node next;
    private bool Check;
    private void Awake()
    {
        // ���������� �޾ƿ´�
        Rigidbody Rigid =  GetComponent<Rigidbody>();

        // �޾ƿ� ���������� x �� z ��ǥ�� ������Ų��
        Rigid.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

        // �浹ü�� �޾ƿ´�
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
