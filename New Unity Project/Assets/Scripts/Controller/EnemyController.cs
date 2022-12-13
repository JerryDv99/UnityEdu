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


    public List<Vector3> VertexList = new List<Vector3>();
    public Vector3 TargetPosition;

    void Start()
    {
        Radius = 5.0f;

        Count = 20;
    }

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
              
        RayPoint();



        foreach (Vector3 Point in PointList)
            Debug.DrawLine(ray.origin, // 시작점
                ray.origin + (Point.normalized * Radius), // 도착점
                Color.red);
         
    }

    public void RayPoint()
    {
        //Ray rapPoint = new Ray(transform.position, transform.forward);
        // 마우스 포인터의 방향으로 레이를 발사
        Ray rapPoint = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        // 레이를 Infinity만큼 출력
        if (Physics.Raycast(rapPoint, out hit, Mathf.Infinity))
        {
            TargetPosition = hit.transform.position;

            MeshFilter filter = hit.transform.GetComponent<MeshFilter>();
            
            if (filter != null)
            {
                Mesh mesh = filter.mesh;

                if(mesh != null)
                {
                    Vector3[] vertices = new Vector3[mesh.vertices.Length];

                    for (int i = 0; i < mesh.vertices.Length; ++i)
                        vertices[i] = mesh.vertices[i];


                    List<Vector3> Temp = new List<Vector3>();

                    for (int i = 0; i < vertices.Length; ++i)
                    {
                        if (!Temp.Contains(vertices[i]) && hit.transform.position.y > vertices[i].y)
                            Temp.Add(vertices[i]);
                    }


                    // ========


                    VertexList.Clear();
                    Vector3[] BottomPoint = new Vector3[4];

                    for (int i = 0; i < BottomPoint.Length; ++i)
                    {
                        BottomPoint[i] = new Vector3(
                            Temp[i].x,// * hit.transform.lossyScale.x,
                            0.1f,
                            Temp[i].z);// * hit.transform.lossyScale.z);

                        Matrix4x4 RotationMatrix;
                        Matrix4x4 PositionMatrix;
                        Matrix4x4 ScaleMatrix;

                        Vector3 eulerAngles = hit.transform.eulerAngles * Mathf.Deg2Rad;

                        PositionMatrix = MathManager.Translate(hit.transform.position);

                        RotationMatrix = MathManager.RotationX(eulerAngles.x)
                            * MathManager.RotationY(eulerAngles.y)
                            * MathManager.RotationZ(eulerAngles.z);

                        ScaleMatrix = MathManager.Scale(hit.transform.lossyScale * 1.5f);

                        Matrix4x4 Matrix = PositionMatrix * RotationMatrix * ScaleMatrix;


                        VertexList.Add(Matrix.MultiplyPoint(BottomPoint[i]));
                    }
                }
                                
            }
        }
        else
            TargetPosition = new Vector3();
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < VertexList.Count; ++i)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(VertexList[i] + TargetPosition, 0.2f);            
        }        
    }
}
