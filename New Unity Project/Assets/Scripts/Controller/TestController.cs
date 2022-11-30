using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    [SerializeField] private Node Target;
    // Start is called before the first frame update
    void Start()
    {
        Transform trans = transform.parent.transform;
        Target = trans.Find("Node").GetChild(0).GetComponent<Node>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Target.transform.position - transform.position;
        
        transform.position += (dir.normalized * Time.deltaTime * 5.0f);
    }

    private void OnTrigetEnter(Collider other)
    {
        if(other.tag == "Node")
        {
            Target = Target.next;
        }
    }
}
