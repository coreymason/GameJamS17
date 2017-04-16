using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// When projectile hits a target, score goes up
public class HitTarget : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Target"))
        {
            GameManager.current.RaiseScore();
            RecycleObject.Recycle(transform.parent.gameObject);
        }
    }
}
