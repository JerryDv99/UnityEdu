using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance = null;

    // 외부에서의 생성 차단
    private GameObject JammoPointObject;
    private GameManager() { }

    private void Awake()
    {
        JammoPointObject = Resources.Load("Points/JammoPoint") as GameObject;
        new GameObject("PointList");

        if (Instance == null)
            Instance = this;

        DontDestroyOnLoad(this);
    }

    private int Count = 0;

    // Start is called before the first frame update
    IEnumerator Start()
    {        
        Count = 1;

        while (true)
        {
            // Update와 동일한 시점에 실행, IEnumerator 남발 x
            // == yield return new WaitForSeconds(Time.detaTime);
            yield return null;

            GameObject Obj = Instantiate(JammoPointObject);

            Obj.AddComponent<WayPointController>();

            Obj.transform.name = "Node_" + Count.ToString();

            Obj.transform.parent = GameObject.Find("PointList").transform;

            if (1 <= Count) break;

            ++Count;
        }
    }
}
