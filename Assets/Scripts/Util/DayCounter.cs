using TMPro;
using UnityEngine;

namespace TowerDefence
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class DayCounter : MonoBehaviour
    {
        private TMP_Text text;
        private int day = 1;

        private void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        public void AddDay()
        {
            day++;
            UpdateText();
        }

        private void UpdateText()
        {
            text.text = "Day: " + day;
        }
    }
}
