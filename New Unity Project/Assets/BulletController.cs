using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject Fx;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject, 0.05f);
        GameObject Obj = Instantiate(Fx);
        Obj.transform.position = transform.position;

        Obj.transform.LookAt(Camera.main.transform.position);
    }
}
