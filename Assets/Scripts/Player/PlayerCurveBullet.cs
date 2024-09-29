using UnityEngine;

public class PlayerCurveBullet : MonoBehaviour
{
    public float duration;
    
    private float _elapsedTime = 0;
    private float _bezierPoint;
    
    private Vector2 _p0;
    private Vector2 _p1;
    private Vector2 _p2;
    void Start()
    {
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
    private void GenerateP2()
    {
        Vector2 mousePosition = Input.mousePosition;
        _p2 = Camera.main.ScreenToWorldPoint(mousePosition);
    }
    private void GenerateP1()
    {
        _p0 = transform.position;
        Vector2 direction = (_p2 - _p0).normalized;

        Vector2 perpendicular = new Vector2(-direction.y, direction.x);
        float distance = Random.Range(1f, 3f);
        _p1 = _p0 + (direction + perpendicular) * distance;
    }
    private Vector2 GetBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2)
    {
        p0 = _p0;  p1 = _p1;  p2 = _p2;
        return (1 - t) * (1 - t) * _p0 + 2 * (1 - t) * t * _p1 + t * t * _p2;
    }
}
