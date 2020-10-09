/*******************************************************
 * Copyright (C) 2017 Doron Weiss  - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of unity license.
 * 
 * See https://abnormalcreativity.wixsite.com/home for more info
 *******************************************************/

using Fila3dColouring.SOVariables.Enums;
using UnityEngine;

namespace Fila3dColouring.Config
{
    [System.Serializable]
    public class Config : AConfig
    {
        [SerializeField]
        private EnvironmentType enviroment;


        private new void Awake()
        {
            base.Awake();
            SetupSingelton();
        }


        #region  Singelton

        public static Config _instance;
        public static Config Instance { get { return _instance; } }

        private void SetupSingelton()
        {
            if (_instance != null)
            {
                Debug.LogError("Error in config. Multiple singeltons exists: " + _instance.name + " and now " + name);
            }
            else
            {
                _instance = this;
            }
        }
        
        #endregion
    }
}