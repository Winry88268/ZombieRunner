using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 15;
    public int AmmoAmount { get { return ammoAmount; }  }

    public void IncreaseAmmo(int increase)
    {
        this.ammoAmount += Mathf.Clamp(increase, 0, 10);
    }

    public void DecreaseAmmo()
    {
        this.ammoAmount -= Mathf.Clamp(1, 0, 10);
    }
}
