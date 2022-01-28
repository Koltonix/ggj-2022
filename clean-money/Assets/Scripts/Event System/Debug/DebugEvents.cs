using UnityEngine;

namespace cm.Events
{
    [CreateAssetMenu(fileName = "Debug Tools", menuName = "ScriptableObjects/Tools/Debug-Tools")]
    public class DebugEvents : ScriptableObject
    {
        public void DebugMessage(string message) => Debug.Log(message);
    }
}