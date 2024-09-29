using UnityEngine;
using UnityEngine.Events;

namespace TowerDefence.Resources.Modifiers
{
    /// <summary>
    /// 
    /// A <see cref="Modifier{TIn,TOut}"/> is a component that allows for transformation of values
    /// (typically received through <see cref="UnityEvent{T0}"/>).
    /// 
    /// In order to create a transformation chain, you have to put the <see cref="Apply"/> method into an event listener
    /// and the receiving method into the <see cref="onApply"/> event.
    /// 
    /// </summary>
    [RequireComponent(typeof(ResourceChangeListener))]
    public abstract class Modifier<TIn, TOut> : MonoBehaviour
    {
        /// <summary>
        /// Triggered on transformation
        /// </summary>
        [SerializeField]
        protected UnityEvent<TOut> onApply;

        protected ResourceChangeListener Listener;
        private void Awake()
        {
            Listener = GetComponent<ResourceChangeListener>();
        }

        /// <summary>
        /// Performs the transformation from <see cref="TIn"/> to <see cref="TOut"/>
        /// </summary>
        /// <param name="value">The value to be transformed</param>
        /// <returns>The transformed value</returns>
        protected abstract TOut Modify(TIn value);
        
        /// <summary>
        /// Triggers the transformation and invokes the <see cref="onApply"/> event with the transformed value
        /// </summary>
        /// <param name="input">Input value from event</param>
        public void Apply(TIn input)
        {
            onApply.Invoke(Modify(input));
        }
    }
}