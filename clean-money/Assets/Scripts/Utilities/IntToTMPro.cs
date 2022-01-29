using UnityEngine;
using TMPro;

namespace cm.utilities
{
    [RequireComponent(typeof(TMP_Text))]
    public class IntToTMPro : MonoBehaviour
    {
        private TMP_Text text = null;

        private void Awake() => text = this.GetComponent<TMP_Text>();
        public void UpdateText(int value) => text.text = value.ToString();
    }
}