using UnityEngine;
using System.Collections;
using TowerDefence.Resources.Objects;

public class ResourceFinder : MonoBehaviour
{
    public float attackCooldown = 1f;
    public float searchRadius = 5f;
    [SerializeField] LayerMask resourceLayer;

    private Material _currentResourceMaterial;
    private const string thicknessProperty = "_Thickness";

    private DamageableResourceObject _currentResource;
    private LaserVisualizer _laserVisualizer;
    private Coroutine _attackCoroutine;
    private float _lastAttackTime;
    void Awake()
    {
        _laserVisualizer = GetComponentInChildren<LaserVisualizer>();
    }
    void Update()
    {
        GameObject nearestResource = FindNearestResource();
        if (nearestResource != null)
        {
            if (_currentResourceMaterial != nearestResource.GetComponent<Renderer>().material)
            {
                if (_currentResourceMaterial != null)
                {
                    _currentResourceMaterial.SetFloat(thicknessProperty, 0f);
                }
                _currentResourceMaterial = nearestResource.GetComponent<Renderer>().material;
                _currentResourceMaterial.SetFloat(thicknessProperty, 0.0156f);
            }
            _currentResource = nearestResource.GetComponent<DamageableResourceObject>();
            if (_laserVisualizer != null)
            {
                _laserVisualizer.ShowLaser(transform.position, nearestResource.transform.position);
            }
            if (Input.GetButton("Fire1"))
            {
                if (_attackCoroutine == null)
                {
                    _attackCoroutine = StartCoroutine(AttackCoroutine());
                }
            }
            else
            {
                if (_attackCoroutine != null)
                {
                    StopCoroutine(_attackCoroutine);
                    _attackCoroutine = null;
                }
                _laserVisualizer.HideLaser();
            }
        }
        else
        {
            if (_currentResourceMaterial != null)
            {
                _currentResourceMaterial.SetFloat(thicknessProperty, 0f);
                _currentResourceMaterial = null;
            }
            _currentResource = null;
            if (_laserVisualizer != null)
            {
                _laserVisualizer.HideLaser();
            }
        }
    }

    GameObject FindNearestResource()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, searchRadius, resourceLayer);

        GameObject nearestResource = null;
        float closestDistance = Mathf.Infinity;
        foreach (Collider2D collider in colliders)
        {
            float distance = Vector2.Distance(transform.position, collider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestResource = collider.gameObject;
            }
        }

        return nearestResource;
    }
    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            if (_currentResource != null)
            {
                Attack();
            }
            yield return new WaitForSeconds(attackCooldown);
        }
    }
    void Attack()
    {
        if (_currentResource != null)
        {
            _currentResource.Hit();
            _lastAttackTime = Time.time;
        }
    }
}
