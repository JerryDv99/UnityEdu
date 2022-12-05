using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] 
public class TestController : MonoBehaviour
{
    private Node Target;
    private int Index;
    

    // Start is called before the first frame update
    void Start()
    {
        Transform trans = transform.parent.transform;
        Target = trans.Find("Node_" + Index.ToString()).GetChild(0).GetComponent<Node>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Target.transform.position - transform.position;
        
        transform.position += (dir.normalized * Time.deltaTime * 5.0f);

        float fDistance = Vector3.Distance(transform.position, Target.transform.position);

        //if (hit)
        //NodeManager.GetNode(Target, Target.next, hit.gameobject);

        if(fDistance < 0.05f)
            Target = Target.next;
    }
}
