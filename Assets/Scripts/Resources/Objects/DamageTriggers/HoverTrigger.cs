using UnityEngine;

namespace TowerDefence.Resources.Objects.DamageTriggers
{
    public class HoverTrigger : DamageTrigger
    {
        [SerializeField]
        private float initialDelay;

        [SerializeField]
        private float triggerPeriod;

        private float _timer;

        private void OnMouseOver()
        {
            UpdateHover();
        }
        private void OnMouseEnter()
        {
            _timer = -initialDelay;
        }
        private void UpdateHover()
        {
            _timer += Time.deltaTime;
            // ReSharper disable once InvertIf
            // people need to be able to understand this code smh
            if (_timer >= triggerPeriod)
            {
                _timer = 0;
                Trigger();
            }
        }
    }
}