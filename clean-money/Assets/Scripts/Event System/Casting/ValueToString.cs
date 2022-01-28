using UnityEngine;

namespace cm.Events
{
    public class ValueToString : MonoBehaviour
    {
        [SerializeField]
        private StringEvent onConversion;

        [SerializeField]
        private string concatenateBefore = "";
        [SerializeField]
        private string concatenateAfter = "";

        public void InvokeConversion(int value) => onConversion?.Invoke(concatenateBefore + value.ToString() + concatenateAfter);
        public void InvokeConversion(float value) => onConversion?.Invoke(concatenateBefore + value.ToString() + concatenateAfter);
        public void InvokeRoundedConversion(float value) => onConversion?.Invoke(concatenateBefore + Mathf.RoundToInt(value).ToString() + concatenateAfter);
    }
}