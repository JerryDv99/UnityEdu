using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    // private 요소를 인스펙터뷰에 나타냄
    [SerializeField] private GameObject EnemyPrefab;

    //[SerializeField] private SkinnedMeshRenderer renderer = null;
    private List<string> names = new List<string>();

    private GameObject Obj;

    // 충돌이 감지되면 실행되는 충돌감지 함수, Collider 필요
    private void OnCollisionEnter(Collision collision)
    {
        // 코루틴 함수를 실행
        StartCoroutine(Create());
    }

    IEnumerator Create()
    {        
        // 0.5초 후에 함수를 재실행
        yield return new WaitForSeconds(0.5f);

        Obj = Instantiate(EnemyPrefab);

        Obj.transform.position = transform.position;

        // Obj.transform.parent = 계층구조를 설정해준다
        // parent = 부모를 설정
        Obj.transform.parent = this.transform;

        // 생성된 오브젝트를 EnemyManager 클래스에 보관
        EnemyManager.Instance.AddObject(Obj);

        // 알파값을 적용할 오브젝트 이름을 받아온다====
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
