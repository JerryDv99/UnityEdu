using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private float Speed;
    private Rigidbody Rigid = null;
    // ��Ÿ�� ���Ŀ� ó�� �ѹ� ����
    // Start �Լ����� ���� �����
    private void Awake()
    {
        // ���� ��ũ��Ʈ�� ���Ե� ��ü�� Rigidbody ���۳�Ʈ�� �޾ƿ�.
        // Rigidbody ���۳�Ʈ�� �������� ������ �ƹ��͵� �޾ƿ��� ����.
        Rigid = this.GetComponent<Rigidbody>();       

        // �浹ó���� �����ϱ� ���ؼ��� �Ʒ� �� ���۳�Ʈ�� �ݵ�� ���ԵǾ�� �Ѵ�.

        // Rigidbody = ���� ���� : �����̴� ��ü�� �߰�
        // Collider =  �浹ü : �浹�� �ʿ��� ��� ��ü�� �߰�
    }

    // ��Ÿ�� ���Ŀ� ó�� �ѹ� ����
    void Start()
    {
        Speed = 5.0f;

        // Rigidbody ���۳�Ʈ�� transform.forward �������� 500.0f ��ŭ ���� ���Ѵ�
        Rigid.AddForce(transform.forward * 2500.0f);
    }

    /*
    // ������ �������� ����
    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // -1 ~ +1 ���� �Ǽ� ������ ��ȯ 
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
