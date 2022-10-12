using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [HideInInspector] public static EnemyManager Instance = null;

    // 외부에서의 생성 차단
    private EnemyManager() { }

    private void Awake()
    {
        // 월드에 빈 게임오브젝트 EnemyList 생성
        new GameObject("EnemyList");

        if (Instance == null)
            Instance = this;
    }

    private List<GameObject> EnemyList = new List<GameObject>();
       
    public void AddObject(GameObject Obj)
    {
        EnemyList.Add(Obj);
    }
}
