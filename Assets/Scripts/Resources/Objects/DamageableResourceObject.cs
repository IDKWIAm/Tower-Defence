using UnityEngine;
using UnityEngine.Events;

namespace TowerDefence.Resources.Objects
{
    public class DamageableResourceObject : ResourceObject
    {
        [SerializeField]
        private int requiredHits;

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

        public void Hit()
        {
            onHit.Invoke();
        }
    }
}