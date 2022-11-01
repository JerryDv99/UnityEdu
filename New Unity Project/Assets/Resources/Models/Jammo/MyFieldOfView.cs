using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MyFieldOfView))]
public class MyFieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        MyFieldOfView Target = (MyFieldOfView)target;

        Vector3 LeftPoint = new Vector3(
           Mathf.Sin(-Target.Angle * Mathf.Deg2Rad),
           0.0f,
           Mathf.Cos(-Target.Angle * Mathf.Deg2Rad));
        Handles.color = Color.red;
        Handles.DrawLine(Target.transform.position, Target.transform.position + LeftPoint * 10);

        Vector3 RightPoint = new Vector3(
           Mathf.Sin(Target.Angle * Mathf.Deg2Rad),
           0.0f,
           Mathf.Cos(Target.Angle * Mathf.Deg2Rad));

        Handles.DrawLine(Target.transform.position, Target.transform.position + RightPoint * 10);
    }
}

public class MyFieldOfView : MonoBehaviour
{
    [Header("시야 각도")]
    [Range(0.1f, 180.0f)]
    public float Angle;
    
    [Range(20, 100)]
    public int Count;

    private void Start()
    {
        Angle = 45.0f;
        Count = 25;
    }

    private void Update()
    {
        Angle = transform.eulerAngles.y - 45.0f;

        for (int i = 0; i < Count; ++i)
        {
            Debug.DrawLine(
                transform.position,
                new Vector3(0.0f, Angle - 45, 0.0f) * 10,                
                Color.blue);

            Angle += 90.0f / (Count - 1);

            Debug.Log(i + " : " + Angle);
        }
        
        /*
        PointList.Clear();

        for (int i = 0; i < Count; ++i)
        {
            PointList.Add(new Vector3(Mathf.Sin(Angle * Mathf.Deg2Rad), 0.0f, Mathf.Cos(Angle * Mathf.Deg2Rad)));

            Angle += 90.0f / (Count - 1);
        }

        float fAngle = 0.0f;

        Ray ray = new Ray();

        for (int i = 0; i < PointList.Count; ++i)
        {
            ray = new Ray(transform.position, PointList[i].normalized);
            RaycastHit hit;

            if (Physics.Raycast(ray.origin, PointList[i].normalized, out hit, Radius, TargetMask)) // out = 쓰기 전용
            {
                Debug.Log(PointList[i]);

                fAngle = Vector3.Angle(transform.forward, PointList[i]);

                fAngle *= (i > ((PointList.Count / 2) - 1)) ? -2 : 2;
            }
        }

        transform.Rotate(transform.up * fAngle * Time.deltaTime);

        transform.position += transform.forward * 5.0f * Time.deltaTime;

        foreach (Vector3 Point in PointList)
            Debug.DrawLine(ray.origin, // 시작점
                (Point.normalized * Radius) + ray.origin, // 도착점
                Color.red);
        */
    }
}