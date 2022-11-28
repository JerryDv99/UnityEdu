using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointController : MonoBehaviour
{
    private void Start()
    {
        int Count = Random.Range(6, 10);

        for (int i = 0; i < Count; ++i)
        {
            GameObject Object = new GameObject(i.ToString());

            Object.transform.SetParent(this.transform);

            Object.AddComponent<Node>();

            if (i > 1)
            {
                Node FrontNode = transform.GetChild(i - 1).GetComponent<Node>();
                Node Node = transform.GetChild(i).GetComponent<Node>();

                FrontNode.next = Node;
                Node.next = transform.GetChild(0).GetComponent<Node>();
            }

            MyGizmo myGizmo = Object.AddComponent<MyGizmo>();
            myGizmo.color = Color.green;
        }        
    }
}
