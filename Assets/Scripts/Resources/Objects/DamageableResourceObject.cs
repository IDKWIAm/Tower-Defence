using System.Collections.Generic;
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
        /// The mapping between breaking stages and sprites.
        /// Also defines the amount of hits needed to break the object.
        /// </summary>
        [SerializeField]
        private List<Sprite> hitsToSpriteMapping;
        /// <summary>
        /// Invoked on hit.
        /// </summary>
        [SerializeField]
        private UnityEvent onHit;

        private int _hitsTaken;
        private SpriteRenderer _spriteRenderer;
        
        protected override void Awake()
        {
            base.Awake();
            onHit.AddListener(ProcessHit);
            onCollect.AddListener(KillYourself);
            
            _spriteRenderer = GetComponent<SpriteRenderer>();
            if (_spriteRenderer is not null)
            {
                _spriteRenderer.sprite = hitsToSpriteMapping[0];
            }
        }

        private void ProcessHit()
        {
            _hitsTaken++;
            if (_hitsTaken >= hitsToSpriteMapping.Count)
            {
                CollectResource();
                return;
            }
            
            if (_spriteRenderer is not null)
            {
                _spriteRenderer.sprite = hitsToSpriteMapping[_hitsTaken];
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