using TowerDefence.Resources.Objects;
using UnityEngine;

namespace TowerDefence.Player
{
    public class ResourceRenderer : MonoBehaviour
    {
        private Material _currentResourceMaterial;
        private const string _thicknessProperty = "_Thickness";

        public void HighlightResource(DamageableResourceObject resource)
        {
            if (resource != null)
            {
                var renderer = resource.GetComponent<Renderer>();
                if (_currentResourceMaterial != renderer.material)
                {
                    _currentResourceMaterial = renderer.material;
                    _currentResourceMaterial.SetFloat(_thicknessProperty, 0.0156f);
                }
            }
        }
        public void ClearHighlight()
        {
            _currentResourceMaterial?.SetFloat(_thicknessProperty, 0f);
            _currentResourceMaterial = null;
        }
    }
}

