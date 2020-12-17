using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SimpleMovement))]
public class SimpleMovementEditor : Editor
{
    private SimpleMovement simpleMovement;

    private void OnEnable()
    {
        simpleMovement = (SimpleMovement)target; 
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (simpleMovement.repeating)
        {
            float localMeme = EditorGUILayout.FloatField(simpleMovement.repeatDelay);
            simpleMovement.repeatDelay = localMeme;
        }
    }
}
