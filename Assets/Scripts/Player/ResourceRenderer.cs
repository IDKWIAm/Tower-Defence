using TowerDefence.Resources.Objects;
using UnityEngine;

namespace Player
{
    public class ResourceRenderer : MonoBehaviour
    {
        private Material _currentResourceMaterial;
        private const string thicknessProperty = "_Thickness";

        public void HighlightResource(DamageableResourceObject resource)
        {
            if (resource != null)
            {
                var renderer = resource.GetComponent<Renderer>();
                if (_currentResourceMaterial != renderer.material)
                {
                    if (_currentResourceMaterial != null)
                    {
                        _currentResourceMaterial.SetFloat(thicknessProperty, 0f);
                    }
                    _currentResourceMaterial = renderer.material;
                    _currentResourceMaterial.SetFloat(thicknessProperty, 0.0156f);
                }
            }
        }

        public void ClearHighlight()
        {
            if (_currentResourceMaterial != null)
            {
                _currentResourceMaterial.SetFloat(thicknessProperty, 0f);
                _currentResourceMaterial = null;
            }
        }
    }
}

