using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChangeSize))]
public class SizeChangeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var targ = target as ChangeSize;

        if (GUILayout.Button("change size"))
        {
            targ.FixSize(targ.size);
        }
    }
}
