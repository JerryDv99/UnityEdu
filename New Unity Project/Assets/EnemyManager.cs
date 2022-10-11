using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager Instance = null;

    public static EnemyManager GetInstance()
    {
        if (Instance == null)
            Instance = new EnemyManager();
        return Instance;
    }

    [SerializeField] private List<GameObject> EnemyList = new List<GameObject>();

    void AddEnemy(GameObject Obj)
    {
        EnemyList.Add(Obj);
    }
}
