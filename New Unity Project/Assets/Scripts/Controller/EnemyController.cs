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

    // Start is called before the first frame update
    void Start()
    {
        Radius = 5.0f;

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
              
        RayPoint();



        foreach (Vector3 Point in PointList)
            Debug.DrawLine(ray.origin, // 시작점
                (Point.normalized * Radius) + ray.origin, // 도착점
                Color.red);
         
    }

    public void RayPoint()
    {
        //Ray rapPoint = new Ray(transform.position, transform.forward);
        Ray rapPoint = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

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

                    VertexList.Clear();
                    Vector3[] BottomPoint = new Vector3[4];
                    for (int i = 0; i < BottomPoint.Length; ++i)
                    {
                        BottomPoint[i] = new Vector3(
                            Temp[i].x,// * hit.transform.lossyScale.x,
                            0.1f,
                            Temp[i].z);// * hit.transform.lossyScale.z);

                        Matrix4x4 RotationMatrix;
                        Matrix4x4 PosMatrix;
                        Matrix4x4 ScaleMatrix;

                        Vector3 eulerAngles = hit.transform.eulerAngles * Mathf.Deg2Rad;

                        PosMatrix = Translate(hit.transform.position);

                        RotationMatrix = RotationX(eulerAngles.x)
                            * RotationY(eulerAngles.y)
                            * RotationZ(eulerAngles.z);

                        ScaleMatrix = Scale(hit.transform.lossyScale * 1.5f);

                        Matrix4x4 Matrix = PosMatrix * RotationMatrix * ScaleMatrix;


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

    public Matrix4x4 Translate(Vector3 position)
    {
        Matrix4x4 matrix = Matrix4x4.identity;

        // 00   01   02   03   x
        // 10   11   12   13   y
        // 20   21   22   23   z
        // 30   31   32   33   w
        //  x    y    z    w

        matrix.m03 = position.x;
        matrix.m13 = position.y;
        matrix.m23 = position.z;

        return matrix;
    }

    public Matrix4x4 RotationX(float _angle)
    {
        Matrix4x4 matrix = Matrix4x4.identity;

        // 00   01   02   03   x
        // 10   11   12   13   y
        // 20   21   22   23   z
        // 30   31   32   33   w
        //  x    y    z    w

        //  0    0    0   0
        //  0   cos -sin  0
        //  0   sin  cos  0
        //  0    0    0   0

        matrix.m11 = matrix.m22 = Mathf.Cos(_angle);
        matrix.m12 = -Mathf.Sin(_angle);
        matrix.m21 = Mathf.Sin(_angle);

        return matrix;
    }

    public Matrix4x4 RotationY(float _angle)
    {
        Matrix4x4 matrix = Matrix4x4.identity;

        // 00   01   02   03   x
        // 10   11   12   13   y
        // 20   21   22   23   z
        // 30   31   32   33   w
        //  x    y    z    w

        //  cos  0   sin  0
        //  0    1    0   0
        // -sin  0   cos  0
        //  0    0    0   1

        matrix.m00 = matrix.m22 = Mathf.Cos(_angle);
        matrix.m02 = Mathf.Sin(_angle);
        matrix.m20 = -Mathf.Sin(_angle);

        return matrix;
    }

    public Matrix4x4 RotationZ(float _angle)
    {
        Matrix4x4 matrix = Matrix4x4.identity;

        // 00   01   02   03   x
        // 10   11   12   13   y
        // 20   21   22   23   z
        // 30   31   32   33   w
        //  x    y    z    w

        //  cos -sin  0   0
        //  sin  cos  0   0
        //  0    0    1   0
        //  0    0    0   1

        matrix.m00 = matrix.m11 = Mathf.Cos(_angle);
        matrix.m01 = -Mathf.Sin(_angle);
        matrix.m10 = Mathf.Sin(_angle);

        return matrix;
    }

    public Matrix4x4 Scale(Vector3 _scale)
    {
        Matrix4x4 matrix = Matrix4x4.identity;

        // 00   01   02   03   x
        // 10   11   12   13   y
        // 20   21   22   23   z
        // 30   31   32   33   w
        //  x    y    z    w

        //  x    0    0   0
        //  0    y    0   0
        //  0    0    z   0
        //  0    0    0   1

        matrix.m00 = _scale.x;
        matrix.m11 = _scale.y;
        matrix.m22 = _scale.z;

        return matrix;  
    }
}
