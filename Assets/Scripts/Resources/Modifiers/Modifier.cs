using UnityEngine;
using UnityEngine.Events;

namespace TowerDefence.Resources.Modifiers
{
    [RequireComponent(typeof(ResourceChangeListener))]
    public abstract class Modifier<TIn, TOut> : MonoBehaviour
    {
        [SerializeField]
        protected UnityEvent<TOut> onApply;

        protected ResourceChangeListener Listener;
        private void Awake()
        {
            Listener = GetComponent<ResourceChangeListener>();
        }

        protected abstract TOut Modify(TIn value);
        public void Apply(TIn input)
        {
            onApply.Invoke(Modify(input));
        }
    }
}