using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public ProgressBar HPBar;
    public WeaponInfoUI weaponInfo;

    public void UpdateWeaponInfo(BaseWeapon weapon) {
        weaponInfo.UpdateWeaponInfo(weapon);
    }

    public void UpdateHP(float HP) {
        HPBar.current = HP;
    }

    public void UpdateMaxHP(float max) {
        HPBar.max = max;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
