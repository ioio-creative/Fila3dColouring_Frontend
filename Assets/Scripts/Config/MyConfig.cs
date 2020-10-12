using UnityEngine;

namespace Fila3dColouring.Config
{
    [CreateAssetMenu(menuName = "Fila3dColouring/Config/My Config")]
    public class MyConfig : ScriptableObject
    {
        #region constants

        private const string FileName = "config.json";
        private const string FolderPath = "Data/";

        #endregion


        [SerializeField]
        private bool isLoadFromFile;


        #region ScriptableObject hooks

        private void Awake()
        {
            
        }

        #endregion


        private void LoadFromFile()
        {

        }
    }
}