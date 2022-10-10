using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private float Speed;
    private Rigidbody Rigid = null;

    public Transform FirePoint;
    public GameObject BulletPrefab;
    public float Power;
    public float Radius;
    public float Vib;

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
        // Rigidbody ���۳�Ʈ�� transform.forward �������� 500.0f ��ŭ ���� ���Ѵ�
        //Rigid.AddForce(transform.forward * 2500.0f);
    }

    /*
    // ������ �������� ����
    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
     */
    void Update()
    {
        // -1 ~ +1 ���� �Ǽ� ������ ��ȯ 
        float fHor = Input.GetAxis("Horizontal");
        float fVer = Input.GetAxis("Vertical");

        // -1, 0, 1
       //float fHor = Input.GetAxisRaw("Horizontal");
       //float fVer = Input.GetAxisRaw("Vertical");
        
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
            if (Physics.Raycast(FirePoint.position, FirePoint.transform.forward, out hit, 100.0f, TargetMask))
            {
                /*
                Vector3 offset = new Vector3(
                    Random.Range(-0.5f, +0.5f),
                    Random.Range(-0.5f, +0.5f), 0.0f);
                 */
                Vector3 offset = new Vector3(
                   Mathf.Cos(90 * Mathf.Deg2Rad),
                   Mathf.Sin(90 * Mathf.Deg2Rad),
                    0.1f) * Radius + FirePoint.position ;

                /*
                float RandomNumber = Random.Range(-1.0f, 1.0f);

                Vector3 Vibrater = new Vector3(
                   Mathf.Cos(RandomNumber * Mathf.Deg2Rad),
                   Mathf.Sin(RandomNumber  * Mathf.Deg2Rad),
                    0.0f) * Vib;
                */
                Vector3 Vibrater = new Vector3(
                    Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0.0f) * Vib;

                Debug.DrawLine(FirePoint.position, offset + Vibrater, Color.red);
               // Debug.DrawLine(FirePoint.position, FirePoint.position + offset*Radius, Color.red);

                // �Ѿ��� ����
                GameObject obj = Instantiate(BulletPrefab);

                // hit.point = ���� hit�� ��ġ
                // ���� �Ѿ��� ��ġ�� hit.point�� �ʱ�ȭ
                obj.transform.position = offset + Vibrater;

                //Rigidbody Rigid = obj.GetComponent<Rigidbody>();

                // Addforce = ���� ���Ѵ�
                // �Ѿ��� �߻�� ���������� Firepoint.transform.forward  �������� Power * 1000 �� ������ �Ѿ��� �߻�
                //Rigid.AddForce(FirePoint.transform.forward * 1000);
                /*
                if (hit.transform.tag != "Bullet")
                {
                    // �Ѿ��� ����
                    GameObject obj = Instantiate(BulletPrefab);

                    // hit.point = ���� hit�� ��ġ
                    // ���� �Ѿ��� ��ġ�� hit.point�� �ʱ�ȭ
                    obj.transform.position = hit.point;
                }                
                 */
            }
        }
       
        /*
          if (Input.GetKeyUp(KeyCode.Space))
        {
            // ��ü ���� �Լ�, �Ѿ��� �����Ѵ�
            GameObject obj = Instantiate(BulletPrefab);

            // ������ �Ѿ��� ��ǥ�� �Ѿ��� ������ ��ġ�� �ʱ�ȭ
            obj.transform.position = FirePoint.position;

            // Rigidbody : ��������
            // obj = �Ѿ�
            // obj�� ���� ���ԵǾ��ִ� Rigidbody Component�� �޾ƿ´�
            Rigidbody Rigid = obj.GetComponent<Rigidbody>();

            // Addforce = ���� ���Ѵ�
            // �Ѿ��� �߻�� ���������� Firepoint.transform.forward  �������� Power * 1000 �� ������ �Ѿ��� �߻�
            Rigid.AddForce(FirePoint.transform.forward * Power * 1000);
        }
         */
    }
}
