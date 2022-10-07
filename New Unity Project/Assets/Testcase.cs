using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testcase : MonoBehaviour
{
    float Angle;
    private void Start()
    {
        Angle = 5.0f;
    }
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(
            transform.position, transform.up, Angle * Time.deltaTime);
    }
}
