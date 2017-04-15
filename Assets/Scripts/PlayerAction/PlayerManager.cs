using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player Manager
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager current;

    private int ammo = 0;

    private void Awake()
    {
        current = this;
    }   // Awake

    public int GetAmmo()
    {
        return ammo;
    }   // GetAmmo

    public void AddAmmo(int num)
    {
        ammo += num;
    }   // AddAmmo

    public void SubtractAmmo(int num)
    {
        ammo -= num;
    }   // SubtractAmmo
}
