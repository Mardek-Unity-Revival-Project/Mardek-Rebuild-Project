using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ColliderHelper : MonoBehaviour
{

    new Collider2D collider = null;
    ContactFilter2D filter = default;

    private void OnValidate()
    {
        collider = GetComponent<Collider2D>();
        if (collider)
        {
            filter.useLayerMask = true;
            LayerMask mask = Physics2D.GetLayerCollisionMask(gameObject.layer);
            filter.layerMask = mask;
        }
    }

    public void OffsetCollider(Vector2 offset)
    {
        if (collider)
            collider.offset = offset;
    }

    public List<Collider2D> Overlaping()
    {
        List<Collider2D> results = new List<Collider2D>();
        if (collider == null)
            return results;
        Collider2D[] colliders = new Collider2D[64];
        collider.OverlapCollider(filter, colliders);
        foreach (Collider2D c in colliders)
            if (c != null && c.isActiveAndEnabled)
                results.Add(c);
        return results;
    }
}
