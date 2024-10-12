using UnityEngine;

public class ResourceFinder : MonoBehaviour
{
    public float searchRadius = 5f;
    public LayerMask resourceLayer;
    
    private Material currentResourceMaterial;
    private const string thicknessProperty = "_Thickness";

    void Update()
    {
        GameObject nearestResource = FindNearestResource();
        if (nearestResource != null)
        {
            if (currentResourceMaterial != nearestResource.GetComponent<Renderer>().material)
            {
                if (currentResourceMaterial != null)
                {
                    currentResourceMaterial.SetFloat(thicknessProperty, 0f);
                }
                currentResourceMaterial = nearestResource.GetComponent<Renderer>().material;
                currentResourceMaterial.SetFloat(thicknessProperty, 0.0156f);
            }
        }
        else
        {
            if (currentResourceMaterial != null)
            {
                currentResourceMaterial.SetFloat(thicknessProperty, 0f);
                currentResourceMaterial = null;
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
}
