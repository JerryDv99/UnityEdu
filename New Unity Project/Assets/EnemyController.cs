using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private List<Vector3> PointList = new List<Vector3>();

    private int Count;
    private float Radius;

    float Angle = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        //Count = 5;
        Radius = 15.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Angle = transform.eulerAngles.y - 45.0f;

        PointList.Clear();

        for (int i = 0; i < 6; ++i)
        {
            PointList.Add(new Vector3(Mathf.Sin(Angle * Mathf.Deg2Rad), 0.0f, Mathf.Cos(Angle * Mathf.Deg2Rad)));

            Angle += 18.0f;
        }

        float fAngle = 0.0f;
        for (int i = 0; i < PointList.Count; ++i)
        {
            RaycastHit hit;
            
            if (Physics.Raycast(transform.position, PointList[i].normalized, out hit, Radius)) // out = 쓰기 전용
            {               
                if (hit.transform.tag == "Jammo")
                {
                    fAngle = Vector3.Angle(transform.forward, PointList[i]);

                    if (i > 3) fAngle *= -1;

                    Debug.Log(fAngle);
                }
            }
        }

        transform.Rotate(transform.up * fAngle * Time.deltaTime);

        transform.position += transform.forward * 5.0f * Time.deltaTime;
        /*
         */
        foreach (Vector3 Point in PointList)
            Debug.DrawLine(transform.position, // 시작점
                (Point.normalized * Radius) + transform.position, // 도착점
                Color.red);
    }
}
