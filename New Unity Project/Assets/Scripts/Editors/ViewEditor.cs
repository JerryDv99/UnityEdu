using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ViewEditor : EditorWindow
{
    [MenuItem("Editor/FieldOfView")]
    static void ShowWindow()
    {
        GetWindow(typeof(ViewEditor)).Show();
    }

    public GameObject Object = null;

    private void OnGUI()
    {
        SerializedObject Obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(Obj.FindProperty("Object"));

        GUILayout.BeginHorizontal();


        if (Object != null)
            if (GUILayout.Button("Button", GUILayout.Width(300), GUILayout.Height(23),
                GUILayout.MinWidth(250), GUILayout.MinHeight(20),
                GUILayout.MaxWidth(350), GUILayout.MaxHeight(25)))
            {
                if (Object.GetComponent<MyFieldOfView>() == null)
                    Object.AddComponent<MyFieldOfView>();
                Object = null;
            }
        GUILayout.EndHorizontal();

        Obj.ApplyModifiedProperties();
    }
}
