using UnityEngine;
using UnityEngine.Events;

namespace TowerDefence.Resources.Modifiers
{
    public abstract class Modifier<TIn, TOut> : MonoBehaviour
    {
        [SerializeField]
        protected UnityEvent<TOut> onApply;

        protected abstract TOut Modify(TIn value);
        public void Apply(TIn input)
        {
            onApply.Invoke(Modify(input));
        }
    }
}