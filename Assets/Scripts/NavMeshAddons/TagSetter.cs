using Pathfinding;
using UnityEngine;

namespace TowerDefence.NavMeshAddons
{
    public class TagSetter : MonoBehaviour
    {
        [SerializeField] private Collider2D _coll;
        [SerializeField] private Collider2D _tagColl;

        public void SetTag(int tag)
        {
            Bounds bounds = _tagColl.bounds;
            bounds.Expand(Vector3.forward * 1000);
            var guo = new GraphUpdateObject(bounds);

            guo.modifyTag = true;
            guo.setTag = tag;
            AstarPath.active.UpdateGraphs(guo);
        }

        public void UpdateNeighbors()
        {
            CastRay(Vector2.up, transform.localScale.y);
            CastRay(Vector2.right, transform.localScale.x);
            CastRay(Vector2.down, transform.localScale.y);
            CastRay(Vector2.left, transform.localScale.x);

            float cornerScale = transform.localScale.y + transform.localScale.y;

            CastRay(Vector2.up + Vector2.right, cornerScale);
            CastRay(Vector2.up + Vector2.left, cornerScale);
            CastRay(Vector2.down + Vector2.right, cornerScale);
            CastRay(Vector2.down + Vector2.left, cornerScale);
        }

        private void CastRay(Vector2 direction, float scale)
        {
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.useTriggers = false;
            RaycastHit2D[] hits = new RaycastHit2D[3];
            var result = _coll.Raycast(direction, contactFilter, hits, scale / 2 + 0.25f);
            
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform == null) continue;

                if (hit.transform.gameObject.TryGetComponent<TagSetter>(out TagSetter tagSetter))
                {
                    tagSetter.SetTag(1);
                }
            }
        }

        // Visualization of ray ends
        //private void OnDrawGizmos()
        //{
        //    Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.up * transform.localScale.y / 2 + Vector2.up / 4);
        //    Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x / 2 + Vector2.right / 4);
        //    Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.down * transform.localScale.y / 2 + Vector2.down / 4);
        //    Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.left * transform.localScale.x / 2 + Vector2.left / 4);
        //
        //    Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.up * transform.localScale.y / 2 + Vector2.up / 4 + Vector2.right * transform.localScale.x / 2 + Vector2.right / 4);
        //    Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.y / 2 + Vector2.right / 4 + Vector2.down * transform.localScale.x / 2 + Vector2.down / 4);
        //    Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.down * transform.localScale.y / 2 + Vector2.down / 4 + Vector2.left * transform.localScale.x / 2 + Vector2.left / 4);
        //    Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.left * transform.localScale.y / 2 + Vector2.left / 4 + Vector2.up * transform.localScale.x / 2 + Vector2.up / 4);
        //}
    }
}
