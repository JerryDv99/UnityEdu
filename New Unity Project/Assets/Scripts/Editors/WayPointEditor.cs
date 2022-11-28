using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WayPointEditor : EditorWindow
{
    [MenuItem("Editor/WayPointEditor")]

    static void ShowWindows()
    {
        GetWindow(typeof(WayPointEditor));
    }

    public GameObject NodeList;

    private void OnGUI()
    {
        SerializedObject Obj = new SerializedObject(this);
        EditorGUILayout.PropertyField(Obj.FindProperty("NodeList"));

        if (NodeList != null && GUILayout.Button("Create"))
            CreateNode();
            

        Obj.ApplyModifiedProperties();
    }

    private void CreateNode()
    {
        GameObject Object = new GameObject(NodeList.transform.childCount.ToString());
        Object.transform.SetParent(NodeList.transform);

        Object.transform.position = new Vector3(25.0f, 0.0f, 25.0f);

        while (true)
        {
            


            break;
        }
    }
}
