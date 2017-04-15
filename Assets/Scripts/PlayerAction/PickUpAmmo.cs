using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Picks up ammo
public class PickUpAmmo : MonoBehaviour
{
    [SerializeField] int ammo = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerManager.current.AddAmmo(ammo);
        RecycleObject.Recycle(transform.parent.gameObject);
    }   // OnTriggerEnter2D
}
