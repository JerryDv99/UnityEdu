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

        Obj.ApplyModifiedProperties();
    }
}
