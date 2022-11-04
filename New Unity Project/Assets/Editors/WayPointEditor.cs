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
        {
            GameObject Object = new GameObject("Node");
            
            Object.transform.parent = NodeList.transform;
            Node node = Object.AddComponent<Node>();

            Object.AddComponent<MyGizmo>();
            MyGizmo myGizmo = Object.AddComponent<MyGizmo>();
            myGizmo.color = Color.green;
        }


        Obj.ApplyModifiedProperties();
    }
}
