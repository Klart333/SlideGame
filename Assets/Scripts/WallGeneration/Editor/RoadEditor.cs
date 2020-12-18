using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(WallCreator))]
public class RoadEditor : Editor
{
    WallCreator creator;

    private void OnSceneGUI()
    {
        if (creator.autoUpdate && Event.current.type == EventType.Repaint)
        {
            creator.UpdateRoad();
        }
    }

    private void OnEnable()
    {
        creator = (WallCreator)target;
    }
}
