using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{    
    private GameObject EnemyPrefab;
    private List<SkinnedMeshRenderer> renderers = new List<SkinnedMeshRenderer>();

    private void Awake()
    {
        EnemyPrefab = Resources.Load("Prefabs/Objects/Jammo") as GameObject;
    }

    // �浹�� �����Ǹ� ����Ǵ� �浹���� �Լ�, Collider �ʿ�
    private void OnCollisionEnter(Collision collision)
    {
        // �ڷ�ƾ �Լ��� ����
        StartCoroutine(Create());
    }

    // GameObject�� ��� ������ Ȯ���� SkinnedMeshRenderer�� Ž��
    void FindRenderer(GameObject _Obj)
    {
        for(int i = 0; i < _Obj.transform.childCount; ++i)
        {
            GameObject Obj = _Obj.transform.GetChild(i).gameObject;

            // ���������� ������ �������������� ����
            if (Obj.transform.childCount > 0)
                FindRenderer(Obj);

            SkinnedMeshRenderer renderer = Obj.transform.GetComponent<SkinnedMeshRenderer>();

            if (renderer != null)
                renderers.Add(renderer);
        }
    }

    IEnumerator Create()
    {        
        // 0.5�� �Ŀ� �Լ��� �����
        yield return new WaitForSeconds(0.5f);

        while(true)
        {
            yield return new WaitForSeconds(10.0f);

           // GameObject Jammo = GameObject.FindWithTag("Jammo");
            GameObject Jammo = Resources.Load<GameObject>("Jammo");

            if (Jammo != null)
                continue;

            GameObject Obj = Instantiate(EnemyPrefab);

            // Obj.transform.parent = ���������� �������ش�
            // parent = �θ� ����
            Obj.transform.parent = this.transform.parent.transform;

            Obj.transform.tag = "Jammo";

            // TestController ��ũ��Ʈ �߰�
            Obj.AddComponent<TestController>();

            Obj.transform.position = transform.position;

            // ������ ������Ʈ�� EnemyManager Ŭ������ ����
            EnemyManager.Instance.AddObject(Obj);

            renderers.Clear();
            FindRenderer(Obj);

            // SkinnedMeshRenderer�� ���İ��� �ְ� ������ ��Ÿ���� �Ѵ�
            foreach (SkinnedMeshRenderer renderer in renderers)
            {
                renderer.material.shader = Shader.Find("Transparent/VertexLit");

                if (renderer != null)
                {
                    if (renderer.material.HasProperty("_Color"))
                    {
                        Color color = renderer.material.GetColor("_Color");
                        // �������� �����
                        renderer.material.SetColor("_Color", new Color(color.r, color.g, color.b, color.a = 0.0f));

                        StartCoroutine(SetColor(renderer, color));
                    }
                }
            }
        }
                
    }

    // ������ ��Ÿ���� �ϴ� ����
    IEnumerator SetColor(SkinnedMeshRenderer renderer, Color color)
    {
        float rColor = 0;

        while (true)
        {
            yield return null;

            rColor += Time.deltaTime;

            renderer.material.SetColor("_Color", new Color(color.r, color.g, color.b, rColor));

            if (rColor >= 255.0f) break;
        }
    }
}
