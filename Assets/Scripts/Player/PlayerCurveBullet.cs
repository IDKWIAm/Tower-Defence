using UnityEngine;

namespace TowerDefence.Player
{
    public class PlayerCurveBullet : MonoBehaviour
    {
        public float duration;

        private float _elapsedTime = 0;

        private Vector2 _p0;
        private Vector2 _p1;
        private Vector2 _p2;

        void Start()
        {
            GenerateP0();
            GenerateP2();
            GenerateP1();
        }

        void Update()
        {
            if (_elapsedTime < duration)
            {
                float t = _elapsedTime / duration;
                transform.position = GetBezierPoint(t, _p0, _p1, _p2);
                _elapsedTime += Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void GenerateP0()
        {
            _p0 = transform.position;
        }

        private void GenerateP2()
        {
            Vector2 mousePosition = Input.mousePosition;
            _p2 = Camera.main.ScreenToWorldPoint(mousePosition);
        }

        private void GenerateP1()
        {
            Vector2 direction = (_p2 - _p0).normalized;

            float angle = Random.Range(-45f, 45f);
            Vector2 perpendicular = Quaternion.Euler(0, 0, angle) * direction;

            float distance = Random.Range(1f, 3f);
            _p1 = _p0 + perpendicular * distance;
        }

        private Vector2 GetBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2)
        {
            return (1 - t) * (1 - t) * p0 + 2 * (1 - t) * t * p1 + t * t * p2;
        }
    }
}
