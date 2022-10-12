using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [HideInInspector] public static EnemyManager Instance = null;

    // �ܺο����� ���� ����
    private EnemyManager() { }

    private void Awake()
    {
        // ���忡 �� ���ӿ�����Ʈ EnemyList ����
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
