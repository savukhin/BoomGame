using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : Character
{
    public HUDController HUD;
    public GameObject WeaponSpot;
    public BaseWeapon[] weaponPrefabs;
    private BaseWeapon currentWeapon;
    private bool canFire = false;

    bool ChangeWeapon(int number) {
        if (weaponPrefabs.Length - 1 < number)
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
            Destroy(currentWeapon.gameObject);
        }
        currentWeapon = Instantiate(weaponPrefabs[number], WeaponSpot.transform);
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
        ChangeWeapon(0);
        HUD.UpdateMaxHP(maxHealthPoints);
        HUD.UpdateHP(healthPoints);
    }

    void Fire () {
        if (currentWeapon && canFire)
            currentWeapon.Fire();
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

    protected override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
        if (collider.GetComponent<Drop>()) {
            var drop = collider.GetComponent<Drop>();
            //if (drop)
        }
    }
}
