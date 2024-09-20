using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence.Towers.Placement
{
    public class TowerCardPrefab : MonoBehaviour
    {
        [SerializeField]
        private Image icon;
        [SerializeField]
        private TMP_Text costText;

        public void SetupValues(Sprite sprite, string formattedCost)
        {
            icon.sprite = sprite;
            costText.text = formattedCost;
        }
    }
}
