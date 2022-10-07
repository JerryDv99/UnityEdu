using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testcase2 : MonoBehaviour
{
    float Angle = 50.0f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), Angle);
    }
}
