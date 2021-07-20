using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : Character
{
    public HUDController HUD;
    public GameObject WeaponSpot;
    public BaseWeapon[] weaponPrefabs;
    private BaseWeapon currentWeapon;

    bool ChangeWeapon(int number) {
        if (weaponPrefabs.Length - 1 < number)
            return false;
        if (currentWeapon != null)
            Destroy(currentWeapon.gameObject);
        currentWeapon = Instantiate(weaponPrefabs[0], WeaponSpot.transform);
        return true;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ChangeWeapon(0);
        HUD.UpdateMaxHP(maxHealthPoints);
        HUD.UpdateHP(healthPoints);
    }

    void Fire () {
        if (currentWeapon)
            currentWeapon.Fire();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            Jump();
        if (Input.GetMouseButtonDown(0))
            Fire();
        MoveAndRotate();
    }

    private void MoveAndRotate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float angleToZ = transform.eulerAngles.y * Mathf.Deg2Rad;
		float angleToX = (360 - transform.eulerAngles.y) * Mathf.Deg2Rad;

		Vector3 direction = new Vector3((h*Mathf.Cos(angleToX) + v*Mathf.Sin(angleToZ)), 0f, (h*Mathf.Sin(angleToX) + v*Mathf.Cos(angleToZ))).normalized;
        MoveInDirection(direction);

        transform.Rotate(0f, Input.GetAxis("Mouse X") * 3f, 0f);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        HUD.UpdateHP(healthPoints);
    }
}
