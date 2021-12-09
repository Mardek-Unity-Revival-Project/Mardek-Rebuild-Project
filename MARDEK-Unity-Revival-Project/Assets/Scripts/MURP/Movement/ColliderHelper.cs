using System.Collections.Generic;
using UnityEngine;

namespace MURP.Movement
{
    [RequireComponent(typeof(Collider2D))]
    public class ColliderHelper : MonoBehaviour
    {
        Collider2D _collider = null;
        ContactFilter2D filter = default;
        List<Collider2D> results = new List<Collider2D>();
        Collider2D[] colliders = new Collider2D[8];

        private void Awake()
        {
            InitializeFields();
        }

        void InitializeFields()
        {
            _collider = GetComponent<Collider2D>();
            if (_collider)
            {
                filter.useLayerMask = true;
                LayerMask mask = Physics2D.GetLayerCollisionMask(gameObject.layer);
                filter.layerMask = mask;
            }
        }

        public void OffsetCollider(Vector2 offset)
        {
            if (_collider)
                _collider.offset = offset;
        }

        public List<Collider2D> Overlaping()
        {
            results.Clear();
            if (_collider == null)
                return results;
            System.Array.Clear(colliders, 0, colliders.Length);
            _collider.OverlapCollider(filter, colliders);
            foreach (Collider2D c in colliders)
                if (c != null && c.isActiveAndEnabled)
                    results.Add(c);
            return results;
        }
    }
}