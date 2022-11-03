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
        Handles.color = Color.red;

        Handles.DrawWireArc(
            Target.transform.position,
            Target.transform.up,
            Target.transform.forward,
            360.0f,
            Target.Radius);
        
        foreach(Vector3 point in Target.ViewList)
        {
            Debug.DrawLine(Target.transform.position, point + Target.transform.position, Color.blue);
        }
         

        Vector3 LeftPoint = new Vector3(
           Mathf.Sin(-Target.Angle * Mathf.Deg2Rad),
           0.0f,
           Mathf.Cos(-Target.Angle * Mathf.Deg2Rad));

        Handles.DrawLine(Target.transform.position, Target.transform.position + LeftPoint * Target.Radius);

        Vector3 RightPoint = new Vector3(
           Mathf.Sin(Target.Angle * Mathf.Deg2Rad),
           0.0f,
           Mathf.Cos(Target.Angle * Mathf.Deg2Rad));

        Handles.DrawLine(Target.transform.position, Target.transform.position + RightPoint * Target.Radius);
    }
}

public class MyFieldOfView : MonoBehaviour
{
    [Header("시야 각도")]
    [Range(0.1f, 180.0f)]
    public float Angle;
    
    [Range(20, 200)]
    public int Count;

    [Range(10.0f, 50.0f)]
    public float Radius;

    [SerializeField] private LayerMask Mask;

    public List<Vector3> ViewList = new List<Vector3>();

    private MeshFilter meshFilter;
    private Mesh mesh;


    private void Awake()
    {
        meshFilter = transform.GetChild(0).GetComponent<MeshFilter>();
        mesh = new Mesh();
        mesh.name = "mesh";
        meshFilter.mesh = mesh;
    }

    private void Start()
    {
        Angle = 45.0f;
        Count = 25;
        Radius = 25.0f;
    }

    private void Update()
    {
        float fAngle = (-Angle);

        ViewList.Clear();
        for (int i = 0; i < Count; ++i)
        {
            Vector3 point = new Vector3(
                Mathf.Sin(fAngle * Mathf.Deg2Rad),
                0.0f,
                Mathf.Cos(fAngle * Mathf.Deg2Rad)).normalized;

            Ray ray = new Ray(transform.position, point);

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Radius, Mask))
                ViewList.Add(hit.point - transform.position);
            else
                ViewList.Add(point * Radius);

            fAngle += (Angle * 2) / (Count - 1);
        }

        // 삼각형을 그리기 위한 좌표 ( + 1 = 시작점을 위한 공간)
        Vector3[] vertices = new Vector3[ViewList.Count + 1];
        vertices[0] = Vector3.zero;
        int[] triangles = new int[((ViewList.Count - 2) * 3)];


        for (int i = 0; i < ViewList.Count; ++i)
            vertices[i + 1] = ViewList[i];

        for (int i = 0; i < ViewList.Count - 2; ++i)
        {
            for (int j = 0; j < 3; ++j)
                triangles[i * 3 + j] = (((i % 3 + j)%3) == 0 ? 0 : i + j);
        }

        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}