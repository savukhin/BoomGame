using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : Character
{
    public HUDController HUD;
    public GameObject head;
    public GameObject WeaponSpot;
    public BaseWeapon[] weaponPrefabs;
    public List<BaseWeapon> weapons = new List<BaseWeapon>();
    private BaseWeapon currentWeapon;
    public GameObject firePoint;
    private bool canFire = false;

    bool ChangeWeapon(int number) {
        if (weapons.Count - 1 < number || currentWeapon == weapons[number])
            return false;
        StopCoroutine("ChangeWeaponAnimation");
        StartCoroutine(ChangeWeponAnimation(number));
        return true;
    }

    void ReloadWeapon() {
        if (!currentWeapon)
            return;
        currentWeapon.Reload();
    }

    IEnumerator ChangeWeponAnimation(int number) {
        canFire = false;
        var downPostion = -0.8f;
        if (currentWeapon != null) {
            for (; currentWeapon.transform.localPosition.y > -downPostion;) {
                currentWeapon.transform.localPosition += Vector3.down * 4 * Time.deltaTime;
                yield return null;
            }
            currentWeapon.Disable();
        }

        currentWeapon = weapons[number];
        currentWeapon.Enable();
        HUD.UpdateWeaponInfo(currentWeapon);
        currentWeapon.transform.localPosition += Vector3.up * downPostion;
        for (; currentWeapon.transform.localPosition.y < 0;) {
            currentWeapon.transform.localPosition += Vector3.up * 4 * Time.deltaTime;
            yield return null;
        }
        currentWeapon.transform.localPosition = Vector3.zero;
        canFire = true;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < weaponPrefabs.Length; i++)
            AddWeapon(weaponPrefabs[i]);
        ChangeWeapon(0);
        HUD.UpdateMaxHP(maxHealthPoints);
        HUD.UpdateHP(healthPoints);
    }

    void Fire (bool burstShooting=false) {
        if (currentWeapon && canFire)
            currentWeapon.Fire(burstShooting);
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeWeapon(2);
        if (Input.GetKeyDown(KeyCode.R))
            ReloadWeapon();
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            Jump();
        if (Input.GetMouseButtonDown(0))
            Fire();
        else if (Input.GetMouseButton(0))
            Fire(true);
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

    public override void Heal(float heal)
    {
        base.Heal(heal);
        HUD.UpdateHP(healthPoints);
    }

    public BaseWeapon FindWeaponType(BaseWeapon objective) {
        foreach (var weapon in weapons) {
            if (weapon.weaponName == objective.weaponName)
                return weapon;
        }
        return null;
    }

    public BaseWeapon FindWeaponType(string objective) {
        foreach (var weapon in weapons) {
            if (weapon.weaponName == objective)
                return weapon;
        }
        return null;
    }

    public void AddBullets(string weapon, int count) {
        var existing = FindWeaponType(weapon);
        if (existing)
            existing.bulletsStock += count;
    }

    public void AddWeapon(BaseWeapon weapon) {
        var existing = FindWeaponType(weapon);
        if (existing) {
            existing.bulletsStock += weapon.bulletsStock;
        } else {
            weapons.Add(Instantiate(weapon, WeaponSpot.transform));
            weapons[weapons.Count - 1].gameObject.SetActive(false);
            weapons[weapons.Count - 1].realFirePoint = firePoint;
            ChangeWeapon(weapons.Count - 1);
        }
    }

    protected override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
        if (collider.GetComponent<Drop>()) {
            var drop = collider.GetComponent<Drop>();
            drop.Activate(this);
            Destroy(drop.gameObject);
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        var collider = collision.collider;
        if (collider.GetComponent<Drop>()) {
            var drop = collider.GetComponent<Drop>();
            drop.Activate(this);
            Destroy(drop.gameObject);
        }
    }

}
