using TowerDefence.Resources.Objects.DamageTriggers.MouseUtils;
using UnityEngine;

namespace TowerDefence.Resources.Objects.DamageTriggers
{
    public class ClickTrigger : DamageTrigger
    {
        [SerializeField]
        private MouseButton mouseButton;

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(mouseButton.ToInt()))
            {
                Trigger();
            }
        }
    }
}