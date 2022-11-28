using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FrustumLine : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField] private GameObject Target;

    [SerializeField] [Range(0.01f, 1.0f)] private float X, Y, W, H;

    [SerializeField] private Vector3[] Line = new Vector3[4];

    float Distance = 0.0f;

    [SerializeField] private List<GameObject> CullingList = new List<GameObject>();
    [SerializeField] private List<GameObject> CulledList = new List<GameObject>();
    [SerializeField] private List<MeshRenderer> RendererList = new List<MeshRenderer>();
    [SerializeField] private LayerMask Mask;


    private void Awake()
    {
        mainCamera = transform.GetComponent<Camera>();            
    }

    // Start is called before the first frame update
    void Start()
    {
        X = 0.47f;
        Y = 0.49f;
        W = 0.06f;
        H = 0.06f;

        Distance = 14.5f;
    }

    private void FixedUpdate()
    {
        mainCamera.CalculateFrustumCorners(
            new Rect(X, Y, W, H),
            mainCamera.farClipPlane, 
            Camera.MonoOrStereoscopicEye.Mono,
            Line);
        CullingList.Clear();

        foreach (Vector3 element in Line)
        {
            var wordSpacePos = mainCamera.transform.TransformVector(element).normalized;
            Debug.DrawRay(transform.position, wordSpacePos * Distance, Color.blue);

            Ray ray = new Ray(transform.position, wordSpacePos);

            RaycastHit[] hits = Physics.RaycastAll(ray, Distance, Mask);

            foreach (RaycastHit hit in hits)
            {
                if (!CullingList.Contains(hit.transform.gameObject))
                    CullingList.Add(hit.transform.gameObject);
                if (!CulledList.Contains(hit.transform.gameObject))
                    CulledList.Add(hit.transform.gameObject);
            }
        }

        RendererList.Clear();
        foreach(GameObject element in CulledList)
            if (!CullingList.Contains(element))
                FindRenderer(element);

        foreach (MeshRenderer renderer in RendererList)
        {
            renderer.material.shader = Shader.Find("Transparent/VertexLit");

            if (renderer != null)
            {
                if (renderer.material.HasProperty("_Color"))
                {
                    Color color = renderer.material.GetColor("_Color");
                    renderer.material.SetColor("_Color", new Color(color.r, color.g, color.b, color.a = 0.5f));
                    StartCoroutine(ReSetColor(renderer, color));
                }
            }
        }

        RendererList.Clear();
        foreach (GameObject element in CullingList)
            FindRenderer(element);

        foreach (MeshRenderer renderer in RendererList)
        {
            renderer.material.shader = Shader.Find("Transparent/VertexLit");

            if (renderer != null)
            {
                if (renderer.material.HasProperty("_Color"))
                {
                    Color color = renderer.material.GetColor("_Color");
                    StartCoroutine(SetColor(renderer, color));
                }
            }
        }
    }

    public void FindRenderer(GameObject _Obj)
    {
        int i = 0;       

        do
        {
            if (_Obj.transform.childCount > 0)
                FindRenderer(_Obj.transform.GetChild(i).gameObject);

            MeshRenderer meshRenderer = _Obj.transform.GetComponent<MeshRenderer>();

            if (meshRenderer != null)
                RendererList.Add(meshRenderer);

            i++;
        }
        while (_Obj.transform.childCount >= i);
    }

    IEnumerator SetColor(MeshRenderer _renderer, Color _color)
    {
        float time = 1.0f;

        while (time > 0.5f)
        {
            yield return null;

            time -= Time.deltaTime;

            _renderer.material.SetColor("_Color", new Color(_color.r, _color.g, _color.b, time));
        }
    }
    IEnumerator ReSetColor(MeshRenderer _renderer, Color _color)
    {
        float rColor = 0.0f;

        while (true)
        {
            yield return null;

            rColor += Time.deltaTime * 5;
            _color.a = rColor;
            _renderer.material.SetColor("_Color", new Color(_color.r, _color.g, _color.b, rColor));

            if (rColor >= 255.0f) break;
        }
    }
}

