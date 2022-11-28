using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance = null;

    // 외부에서의 생성 차단
    private GameManager() { }
    private GameObject JammoPointObject;
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

            Obj.transform.position = new Vector3(
                Random.Range(-25.0f, 25.0f), 
                Random.Range( 10.0f, 20.0f),             
                Random.Range(-25.0f, 25.0f));

            Obj.AddComponent<WayPointController>();

            Obj.transform.name = Count.ToString();

            Obj.transform.parent = GameObject.Find("PointList").transform;

            if (10 <= Count) break;

            ++Count;
        }
    }
}
