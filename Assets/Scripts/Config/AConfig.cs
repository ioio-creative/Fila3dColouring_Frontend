/*******************************************************
 * Copyright (C) 2017 Doron Weiss  - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of unity license.
 * 
 * See https://abnormalcreativity.wixsite.com/home for more info
 *******************************************************/
using UnityEngine;
using System.IO;

namespace Fila3dColouring.Config
{
    [System.Serializable]
    public abstract class AConfig : MonoBehaviour
    {
        public const string FileName = "config.json";
        public const string FolderPath = "Data/";
        public const string SettingFolderPath = "Assets/" + FolderPath;

        public const string SettingPath = SettingFolderPath + FileName; // Assets/Data/config.json";

        private bool _loadSettingInEditorPlay = true;
        public bool LoadSettingInEditorPlay 
        {
            get
            {
                return _loadSettingInEditorPlay;
            }
            set
            {
                _loadSettingInEditorPlay = value; 
            }
        }

        private bool _autoSave = true;
        public bool AutoSave
        { 
            get
            { 
                return _autoSave; 
            }
            set
            {
                _autoSave = value; 
            }
        }


        protected void Reset()
        {
            if (FindObjectsOfType<AConfig>().Length != 1)
            {
                Debug.LogError("Too many config singeltons in scene. Destroy one");
            }
            else
            {
                name = "ConfigSingleton";
                Debug.Log("Create config GameObject");
            }

            LoadEditorSetting();
#if UNITY_EDITOR
            SetScriptAwakeOrder<AConfig>(short.MinValue);
#endif
        }

        protected void OnValidate()
        {
#if UNITY_EDITOR
            if (AutoSave && Application.isPlaying == false)
            {
                //Debug.Log("Auto save " + Application.isPlaying + " " );
                SaveToFile(false);
            }
#endif
        }


        // Use this for initialization
        protected void Awake()
        {
            try
            {
#if UNITY_EDITOR
                LoadEditorSetting();
#else
			var fileDestination = Path.Combine(Application.dataPath, "../");
			fileDestination = Path.Combine(fileDestination, FileName);
			fileDestination = Path.GetFullPath(fileDestination);
			Debug.Log (name + " Load from " + fileDestination);
			LoadSetting(fileDestination, this);
#endif
                var t = JsonUtility.ToJson(this);
                Debug.Log("Setting is: " + t);
            }
            catch (System.Exception e)
            {
                Debug.Log(name + " Error with loading scripts " + e);
            }
        }


        #region Inside Unity

        private void LoadEditorSetting()
        {
            if (LoadSettingInEditorPlay)
            {
                var filePath = Path.Combine(Application.dataPath, FolderPath + FileName);
                LoadSetting(filePath);
            }
        }

        private string GetJsonFromFile(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            return null;
        }

        private void LoadSetting(string path, bool log = false)
        {
            var json = GetJsonFromFile(path);

            if (json != null)
            {
                if (log)
                {
                    Debug.Log("Load file " + json);
                }

                try
                {
                    JsonUtility.FromJsonOverwrite(json, this);
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error with overwriting config file " + e);
                }
            }
            else
            {
                Debug.LogError("Failed loading config of " + name + " from " + path);
            }
        }

        #endregion
        
        
        #region Unity editor

#if UNITY_EDITOR
        public static void SetScriptAwakeOrder<T>(short num)
        {
            string scriptName = typeof(T).Name;
            SetScriptAwakeOrder(scriptName, num);
        }

        public static void SetScriptAwakeOrder(string scriptName, short num)
        {
            foreach (var monoScript in UnityEditor.MonoImporter.GetAllRuntimeMonoScripts())
            {
                if (monoScript.name == scriptName)
                {
                    var exeOrder = UnityEditor.MonoImporter.GetExecutionOrder(monoScript);
                    if (exeOrder != num)
                    {
                        //Debug.Log("Change script " + monoScript.name + " old " + exeOrder + " new " + num);
                        UnityEditor.MonoImporter.SetExecutionOrder(monoScript, num);
                    }
                    break;
                }
            }
        }
#endif
        public void LoadToScript()
        {
            var filePath = Path.Combine(Application.dataPath, FolderPath + FileName);
            LoadSetting(filePath, true);
        }

        public void SaveToFile(bool forceWrite = true)
        {
            var filePath = Path.Combine(Application.dataPath, FolderPath + FileName);
            var loaded = GetJsonFromFile(filePath);
            string json = null;
            
            try
            {
                json = JsonUtility.ToJson(this, true);

            }
            catch (System.Exception e)
            {
                Debug.LogError("Error with loading config file: " + e);
            }

            if (forceWrite || loaded != json || json == null)
            {
                WriteToFile(json);
            }
        }

        private static void WriteToFile(string json)
        {
            Debug.Log("Save config file at " + SettingPath + " with data: " + json);

            if (!Directory.Exists(SettingFolderPath))
            {
                Directory.CreateDirectory(SettingFolderPath);
            }
            
            using (var fs = File.CreateText(SettingPath))
            {
                fs.Write(' ');
            }

            File.WriteAllText(SettingPath, json);
        }

        #endregion
    }
}
