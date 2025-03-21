using System;
using System.Collections;
using UnityEngine;

namespace TowerDefence.DayNight
{
    public class WorldTime : MonoBehaviour
    {
        public event EventHandler<TimeSpan> WorldTimeChanged;
        [SerializeField] private float _dayLenght;
        private TimeSpan _currentTime;
        private float _minuteLength => _dayLenght / WorldTimeConstants.MinutesInDay;

        private void Start()
        {
            StartCoroutine(AddMinute());
        }
        private IEnumerator AddMinute()
        {
            _currentTime += TimeSpan.FromMinutes(1);
            WorldTimeChanged?.Invoke(this, _currentTime);
            yield return new WaitForSeconds(_minuteLength);
            StartCoroutine(AddMinute());
        }
    }
}
