
/*******************************************************
 * Copyright (C) 2017 Doron Weiss  - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of unity license.
 * 
 * See https://abnormalcreativity.wixsite.com/home for more info
 *******************************************************/
using UnityEditor;
using UnityEngine;

namespace Fila3dColouring.Config
{
    [CustomEditor(typeof(Config))]
    public class ConfigInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var script = target as AConfig;

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("_______Config Buttons_______", EditorStyles.boldLabel);

            EditorGUILayout.Separator();

            if (GUILayout.Button("Save to file"))
            {
                script.SaveToFile();
            }
            if (GUILayout.Button("Load from file"))
            {
                script.LoadToScript();
            }
            
            script.LoadSettingInEditorPlay = EditorGUILayout.Toggle("Load File On Play",
               script.LoadSettingInEditorPlay);

            script.AutoSave = EditorGUILayout.Toggle("Auto Save",
               script.AutoSave);
        }
    }
}
