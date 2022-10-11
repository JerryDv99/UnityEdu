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
        Power = 0;
        
        BulletRender = false;
    }

    void Update()
    {
        // -1 ~ +1 ���� �Ǽ� ������ ��ȯ 
        float fHor = Input.GetAxis("Horizontal");
        float fVer = Input.GetAxis("Vertical");

        //Vector3 Movement = Vector3.forward * Speed;
        Vector3 Movement = new Vector3(fHor, 0.0f, fVer) * Speed * Time.deltaTime;
        transform.position += Movement;

        // ��ư�� �ԷµǸ� Power �ʱ�ȭ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vib = 0;
            Radius = 0;
            Power = 0;
        }
        // ��ư�� ������ �ִ� ���� Power�� ���� ������Ŵ
        if (Input.GetKey(KeyCode.Space))
        {
            Power += Time.deltaTime;
            Radius += Time.deltaTime;
            Vib += Time.deltaTime;
        }
        // ��ư�� ��������
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // Ÿ���� ������ �޾ƿ´�
            RaycastHit hit;

            // ������ ���� �߻��ؼ� �浹�� �ִٸ� hit ����� �̸� Ȯ���Ѵ�
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
                    // �Ѿ��� ����
                    GameObject obj = Instantiate(BulletPrefab);

                    // hit.point = ���� hit�� ��ġ
                    // ���� �Ѿ��� ��ġ�� hit.point�� �ʱ�ȭ
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
