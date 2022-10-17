using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [HideInInspector] public static ObjectManager Instance = null;

    //[HideInInspector] private List<GameObject> ObjectList = new List<GameObject>();

    [HideInInspector] public Dictionary<string, GameObject> ObjectList = new Dictionary<string, GameObject>();
    private ObjectManager() { }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        GameObject[] Objects = Resources.LoadAll<GameObject>("Prefabs/Objects");

        foreach (GameObject Element in Objects)
            ObjectList.Add(Element.name, Element);

        DontDestroyOnLoad(this);
    }
}
