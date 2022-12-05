using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [Range(-100.0f, 100.0f)]
    private float Height;

    private Vector3 StartNode;
    private Vector3 EndNode;


    [SerializeField] private List<Vector3> Points = new List<Vector3>();

    private void Start()
    {
        Height = 0.0f;

        StartNode = new Vector3(-70.0f, 0.0f, 0.0f);
        EndNode = new Vector3(-60.0f, 0.0f, 0.0f);

        BezierList();
    }

    void Update()
    {
        for (int i = 0; i < Points.Count - 1; ++i)
            Debug.DrawLine(Points[i], Points[i + 1], Color.green);        
    }

    public void BezierList()
    {
        Vector3 temp, dest;

        Height = Vector3.Distance(StartNode, EndNode);

        temp = new Vector3(StartNode.x, StartNode.y, StartNode.z + Height);
        dest = new Vector3(EndNode.x, EndNode.y, EndNode.z + Height);

        Vector3[] Nodes = new Vector3[5];

        float ratio = 0.0f;

        Points.Clear();

        Points.Add(StartNode);
        while (ratio < 1.0f)
        {
            ratio += Time.deltaTime;

            Nodes[0] = Vector3.Lerp(StartNode, temp, ratio);
            Nodes[1] = Vector3.Lerp(temp, dest, ratio);
            Nodes[2] = Vector3.Lerp(dest, EndNode, ratio);
            Nodes[3] = Vector3.Lerp(Nodes[0], Nodes[1], ratio);
            Nodes[4] = Vector3.Lerp(Nodes[1], Nodes[2], ratio);

            Points.Add(
                Vector3.Lerp(Nodes[3], Nodes[4], ratio));

        }
        Points.Add(EndNode);
    }

    // 모든 노드의 거리의 합을 반환하는 함수
    public static float NodeResult(List<Node> nodes)
    {
        // 노드가 100개 이상이면 계산을 하지 않는다
        if (nodes.Count >= 100)
            return 0.0f;

        float fDistance = .0f;

        // 모든 노드를 확인하면서 합을 구한다
        for (int i = 0; i < nodes.Count - 1; ++i)
        {
            fDistance = Vector3.Distance(
                nodes[i].transform.position, 
                nodes[i + 1].transform.position);
        }

        // 누적된 합 반환
        return fDistance;
    }

    // 레이의 높이보다 낮은 버텍스를 확인하여 반환하는 함수
    public static List<Vector3> GetVertices(GameObject Object)
    {
        List<Vector3> VertexList = new List<Vector3>();

        MeshFilter filter = Object.transform.GetComponent<MeshFilter>();

        if (filter != null)
        {
            // MeshFilter의 mesh 탐색
            Mesh mesh = filter.mesh;

            if (mesh != null)
            {
                /*
                Vector3[] vertices = new Vector3[mesh.vertices.Length];

                for (int i = 0; i < mesh.vertices.Length; ++i)
                    vertices[i] = mesh.vertices[i];
                */

                // 조건 1. 모든 Vertex를 확인
                // 조건 2. 중복된 Vertex 배제
                for (int i = 0; i < mesh.vertices.Length; ++i)
                {
                    // 조건에 맞는 모든 Vertex를 VertexList에 담는다
                    // Object.transform.position.y의 위치가 최하위 위치일 수 있으므로 변경 필요
                    if (!VertexList.Contains(mesh.vertices[i]) && /**/Object.transform.position.y > mesh.vertices[i].y)
                        VertexList.Add(mesh.vertices[i]);
                }
            }
        }
        

        // VertexList 반환
        return VertexList;
    }

    public static List<Vector3> GetNode(Vector3 front, Vector3 back, GameObject Object)
    {
        List<Vector3> Vertices = GetVertices(Object);

        /*
        for (int i = 0; i < 10; ++i)
            Vector3.Lerp(front, back, i);
        */

        List<Vector3> nodes = new List<Vector3>();

        //nodes.Add(front);

        {
            nodes.Add(new Vector3());
            nodes.Add(new Vector3());
        }

        //nodes.Add(back);

        return nodes;
    }
}
