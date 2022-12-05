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

    // 충돌이 감지되면 실행되는 충돌감지 함수, Collider 필요
    private void OnCollisionEnter(Collision collision)
    {
        // 코루틴 함수를 실행
        StartCoroutine(Create());
    }

    // GameObject의 모든 계층을 확인해 SkinnedMeshRenderer를 탐색
    void FindRenderer(GameObject _Obj)
    {
        for(int i = 0; i < _Obj.transform.childCount; ++i)
        {
            GameObject Obj = _Obj.transform.GetChild(i).gameObject;

            // 계층구조가 있으면 하위계층에서도 실행
            if (Obj.transform.childCount > 0)
                FindRenderer(Obj);

            SkinnedMeshRenderer renderer = Obj.transform.GetComponent<SkinnedMeshRenderer>();

            if (renderer != null)
                renderers.Add(renderer);
        }
    }

    IEnumerator Create()
    {        
        // 0.5초 후에 함수를 재실행
        yield return new WaitForSeconds(0.5f);

        while(true)
        {
            yield return new WaitForSeconds(10.0f);

           // GameObject Jammo = GameObject.FindWithTag("Jammo");
            GameObject Jammo = Resources.Load<GameObject>("Jammo");

            if (Jammo != null)
                continue;

            GameObject Obj = Instantiate(EnemyPrefab);

            // Obj.transform.parent = 계층구조를 설정해준다
            // parent = 부모를 설정
            Obj.transform.parent = this.transform.parent.transform;

            Obj.transform.tag = "Jammo";

            // TestController 스크립트 추가
            Obj.AddComponent<TestController>();

            Obj.transform.position = transform.position;

            // 생성된 오브젝트를 EnemyManager 클래스에 보관
            EnemyManager.Instance.AddObject(Obj);

            renderers.Clear();
            FindRenderer(Obj);

            // SkinnedMeshRenderer에 알파값을 주고 서서히 나타나게 한다
            foreach (SkinnedMeshRenderer renderer in renderers)
            {
                renderer.material.shader = Shader.Find("Transparent/VertexLit");

                if (renderer != null)
                {
                    if (renderer.material.HasProperty("_Color"))
                    {
                        Color color = renderer.material.GetColor("_Color");
                        // 투명으로 만든다
                        renderer.material.SetColor("_Color", new Color(color.r, color.g, color.b, color.a = 0.0f));

                        StartCoroutine(SetColor(renderer, color));
                    }
                }
            }
        }
                
    }

    // 서서히 나타나게 하는 구간
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
