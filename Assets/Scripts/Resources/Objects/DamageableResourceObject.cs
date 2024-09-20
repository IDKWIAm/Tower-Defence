using UnityEngine;
using UnityEngine.Events;

namespace TowerDefence.Resources.Objects
{
    /// <summary>
    /// Represents a resource object that can be damaged and destroyed after a certain number of hits.
    ///
    /// <seealso cref="ResourceObject"/>
    /// </summary>
    public class DamageableResourceObject : ResourceObject
    {
        /// <summary>
        /// The required number of hits before being destroyed.
        /// </summary>
        [SerializeField]
        private int requiredHits;

        /// <summary>
        /// Invoked on hit.
        /// </summary>
        [SerializeField]
        private UnityEvent onHit;

        protected override void Awake()
        {
            base.Awake();
            onHit.AddListener(ProcessHit);
            onCollect.AddListener(KillYourself);
        }

        private void ProcessHit()
        {
            requiredHits--;
            if (requiredHits <= 0)
            {
                CollectResource();
            }
        }
        private void KillYourself()
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// Triggers a hit to this object.
        /// </summary>
        public void Hit()
        {
            onHit.Invoke();
        }
    }
}