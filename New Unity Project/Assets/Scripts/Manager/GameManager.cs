using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance = null;

    // �ܺο����� ���� ����
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
            // Update�� ������ ������ ����, IEnumerator ���� x
            // == yield return new WaitForSeconds(Time.detaTime);
            yield return null;

            GameObject Obj = Instantiate(JammoPointObject);

            Obj.transform.name = Count.ToString();

            Obj.transform.parent = GameObject.Find("Jammo").transform;

            if (1 <= Count) break;

            ++Count;
        }
    }
}
