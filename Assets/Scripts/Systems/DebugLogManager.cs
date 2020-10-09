using UnityEngine;

namespace HangSengInteractiveWall.Systems
{
    public class HideDebugLog : MonoBehaviour
    {
        private void Awake()
        {
            // https://youtu.be/KErkmxbkBs8?t=462
#if UNITY_EDITOR
            Debug.unityLogger.logEnabled = true;
#else
            Debug.unityLogger.logEnabled = false;
#endif
        }
    }
}
