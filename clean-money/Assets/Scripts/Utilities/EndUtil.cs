using UnityEngine;
using TMPro;
using cm.gameplay;

namespace gm.utilities
{
    public class EndUtil : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text finalScore = null;

        [SerializeField]
        private GameMode gm = null;

        public void UpdateText()
        {
            finalScore.text = "You embezzled $" + gm.money.ToString();
        }
    }
}