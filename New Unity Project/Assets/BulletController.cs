using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject Fx;

    private void OnCollisionEnter(Collision collision)
    {
        // ���� ��ũ��Ʈ�� ���Ե� gameObject�� ����
        Destroy(this.gameObject, 0.05f);

        // Instantiate = ���� �Լ�
        // Fx�� ���纻�� Ob�� �Ѱ���
        GameObject Obj = Instantiate(Fx);

        // ���� �Ѿ� = �� ��ũ��Ʈ�� ���Ե� gameObject
        // �Ѿ��� ��������� �ݴ������ �ٶ󺸴� ���͸� ����
        Vector3 Direction = (transform.position - collision.transform.position).normalized;

        // ���� �Ѿ��� ��ġ�κ��� Direction �������� 2.0 ��ŭ �̵�
        // ��������� �Ѿ� ��������� �ݴ�� ������
        Obj.transform.position = transform.position + (Direction * 2.0f);

        // Fx�� ���纻�� Obj�� �ִϸ��̼��� ������ 1.5�� �ڿ� ����
        Destroy(Obj.gameObject, 1.5f);
    }
}
