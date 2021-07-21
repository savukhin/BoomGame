using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoUI : MonoBehaviour
{
    public Text weaponNameText;
    public Text weaponMagazineText;
    public Text weaponStockText;
    private BaseWeapon currentWeapon;
    
    void Update() {
        if (currentWeapon) {
            weaponNameText.text = currentWeapon.weaponName;
            weaponMagazineText.text = currentWeapon.bulletsMagazine.ToString();
            weaponStockText.text = currentWeapon.bulletsStock.ToString();
        } else {
            weaponNameText.text = weaponMagazineText.text = weaponStockText.text = "";
        }
    }

    public void UpdateWeaponInfo(BaseWeapon weapon) {
        currentWeapon = weapon;
    }
}
