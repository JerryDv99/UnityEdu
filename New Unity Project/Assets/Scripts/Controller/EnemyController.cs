using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private LayerMask TargetMask;

    private List<Vector3> PointList = new List<Vector3>();

    private float Radius;

    float Angle;

    [Tooltip("장애물을 확인하는 가상의 라인 개수")]
    [Range(5, 30)]
    public int Count;

    // Start is called before the first frame update
    void Start()
    {
        Radius = 15.0f;

        Count = 20;
    }

    // Update is called once per frame
    void Update()
    {
        Angle = transform.eulerAngles.y - 45.0f;

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

                fAngle *= (i > ((PointList.Count / 2) -1)) ? -2 : 2;         
            }
        }

        transform.Rotate(transform.up * fAngle * Time.deltaTime);

        transform.position += transform.forward * 5.0f * Time.deltaTime;
       
        foreach (Vector3 Point in PointList)
            Debug.DrawLine(ray.origin, // 시작점
                (Point.normalized * Radius) + ray.origin, // 도착점
                Color.red);
    }
}
