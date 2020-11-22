using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Debugger))]
public class DebugEditor : Editor
{
    Debugger debugger;

    private void OnEnable()
    {
        debugger = (Debugger)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Get Skin"))
        {
            Skin skin = new Skin();
            foreach (Skin localSkin in GetSkin.allSkins)
            {
                if (localSkin.name == debugger.SkinToAdd)
                {
                    skin = localSkin;
                }
            }

            if (skin.name != "")
            {
                SaveSkin.SaveASkin(skin);
            }
        }

        if (GUILayout.Button("Sort Skins"))
        {
            SaveSkin.SortSkins();
        }

        if (GUILayout.Button("Get 10 Lootboxes"))
        {
            LootBoxAmount.SetLootBoxAmount(10);
        }
    }

}
