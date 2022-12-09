using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] 
public class TestController : MonoBehaviour
{
    private Node Target;
    public Node GetTarget() { return Target; }
    private int Index;
    

    // Start is called before the first frame update
    void Start()
    {
        Index = 1;

        Transform trans = transform.parent.transform;
        Target = trans.Find("Node_" + Index.ToString()).GetChild(0).GetComponent<Node>();

        //this.transform.position = Target.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Target.transform.position - transform.position;
        
        transform.position += (dir.normalized * Time.deltaTime * 5.0f);

        float fDistance = Vector3.Distance(transform.position, Target.transform.position);

        RaycastHit hit;

        /*
        */
        // 레이를 현재 오브젝트 위치에서 오브젝트 진행 방향으로 무한정 출력
        if (Input.GetKeyDown(KeyCode.Return))
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
            {
                if(Vector3.Distance(transform.position, hit.transform.position) <= 1.5f)
                    Target = NodeManager.GetNode(this.gameObject, hit);
            }

        Vector3 V1 = (Target.transform.position - transform.position).normalized;
        V1.y = 0;
        transform.LookAt(transform.position + V1);

        Debug.DrawRay(transform.position, Target.transform.position, Color.blue);

        if (fDistance < 0.05f)
            Target = Target.next;

    }
}
