using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    // private ��Ҹ� �ν����ͺ信 ��Ÿ��
    [SerializeField] private GameObject EnemyPrefab;

    //[SerializeField] private SkinnedMeshRenderer renderer = null;
    private List<string> names = new List<string>();

    private GameObject Obj;

    // �浹�� �����Ǹ� ����Ǵ� �浹���� �Լ�, Collider �ʿ�
    private void OnCollisionEnter(Collision collision)
    {
        // �ڷ�ƾ �Լ��� ����
        StartCoroutine(Create());
    }

    IEnumerator Create()
    {        
        // 0.5�� �Ŀ� �Լ��� �����
        yield return new WaitForSeconds(0.5f);

        Obj = Instantiate(EnemyPrefab);

        Obj.transform.position = transform.position;

        // Obj.transform.parent = ���������� �������ش�
        // parent = �θ� ����
        Obj.transform.parent = this.transform;

        // ������ ������Ʈ�� EnemyManager Ŭ������ ����
        EnemyManager.Instance.AddObject(Obj);

        // ���İ��� ������ ������Ʈ �̸��� �޾ƿ´�====
        string[] culling = new string[3];

        culling[0] = "Armatuar.001";
        culling[1] = "head_eyes_low";
        culling[2] = "ear.joint_low";

        for (int i = 0; i < Obj.transform.childCount; ++i)
        {
            string name = Obj.transform.GetChild(i).ToString();

            bool Check = true;

            for (int j = 0; j < culling.Length; ++j)
                if (name == culling[j])
                    Check = false;

            if (Check)           
                names.Add(name);
        }
        //===========================================

        foreach (string name in names)
        {
            SkinnedMeshRenderer renderer = Obj.transform.Find(name).GetComponent<SkinnedMeshRenderer>();

            if (renderer != null)
            {
                renderer.material.shader = Shader.Find("Transparent/VertexLit");

                if (renderer.material.HasProperty("_Color"))
                {
                    Color color = renderer.material.GetColor("_Color");
                    renderer.material.SetColor("_Color", new Color(color.r, color.g, color.b, color.a = 0.0f));

                    StartCoroutine(SetColor(renderer, color));
                }
            }            
        }        
    }

    IEnumerator SetColor(SkinnedMeshRenderer renderer, Color color)
    {
        float rColor = 0.0f;

        while (true)
        {
            yield return null;

            rColor += Time.deltaTime;

            color.a = rColor;

            renderer.material.SetColor("_Color", new Color(color.r, color.g, color.b, rColor));

            if (rColor >= 255.0f) break;
        }
    }
}
