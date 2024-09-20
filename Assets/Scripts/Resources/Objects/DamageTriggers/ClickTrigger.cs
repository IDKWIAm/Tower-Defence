using TowerDefence.Resources.Objects.DamageTriggers.MouseUtils;
using UnityEngine;

namespace TowerDefence.Resources.Objects.DamageTriggers
{
    /// <summary>
    /// Triggered when a <see cref="mouseButton"/> is clicked over this object.
    /// <seealso cref="MouseButtonExtensions"/>
    /// <seealso cref="MouseButton"/>
    /// </summary>
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