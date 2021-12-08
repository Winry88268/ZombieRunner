using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;
    [SerializeField] Canvas reloadingWeaponCanvas;

    bool isReloading = false;
    public bool IsReloading { get { return this.isReloading; } }

    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoLoadedAmount;
        public int ammoTotalAmount;
        public int maxAmmo;
        public int maxMagazine;
    }

    public int GetAmmoLoadedAmount(AmmoType ammoType)
    {
        AmmoSlot ammoSlot = GetAmmoSlot(ammoType);
        return ammoSlot.ammoLoadedAmount;
    }

    public int GetAmmoTotalAmount(AmmoType ammoType)
    {
        AmmoSlot ammoSlot = GetAmmoSlot(ammoType);
        return ammoSlot.ammoTotalAmount;
    }

    public void IncreaseAmmo(int increase, AmmoType ammoType)
    {
        AmmoSlot ammoSlot = GetAmmoSlot(ammoType);
        ammoSlot.ammoTotalAmount += Mathf.Clamp(increase, 0, ammoSlot.maxAmmo);
    }

    public void DecreaseAmmo(AmmoType ammoType)
    {
        AmmoSlot ammoSlot = GetAmmoSlot(ammoType);
        ammoSlot.ammoLoadedAmount -= Mathf.Clamp(1, 0, ammoSlot.maxMagazine);
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach (AmmoSlot ammoSlot in this.ammoSlots)
        {
            if (ammoSlot.ammoType == ammoType)
            {
                return ammoSlot;
            }
        }
        return null;
    }

    public IEnumerator Reload(Weapon weapon)
    {
        if (this.isReloading) { yield break; }
        
        AmmoSlot ammoSlot = GetAmmoSlot(weapon.AmmoType);

        if (ammoSlot.ammoLoadedAmount < ammoSlot.maxMagazine && ammoSlot.ammoTotalAmount > 0)
        {
            this.isReloading = true;
            weapon.CanShoot(false);
            this.reloadingWeaponCanvas.enabled = true;

            yield return new WaitForSeconds(weapon.ReloadTime);

            int ammoChange = Mathf.Clamp(ammoSlot.maxMagazine - ammoSlot.ammoLoadedAmount, 0, ammoSlot.ammoTotalAmount);

            ammoSlot.ammoLoadedAmount += Mathf.Clamp(ammoChange, 0, ammoSlot.maxMagazine);
            ammoSlot.ammoTotalAmount -= Mathf.Clamp(ammoChange, 0, ammoSlot.maxMagazine);

            this.reloadingWeaponCanvas.enabled = false;
            this.isReloading = false;
            weapon.CanShoot(true);
        }
    }
}
