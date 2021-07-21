using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    public VFX fireVFX;
    public GameObject firePoint;
    public float recoilImpulse = 2f;
    public float fireRate = 0.4f;
    public float bulletsCapacity = 10;
    public float bulletsStock = 0;
    public float bulletsMagazine = 0;
    public float reloadTime = 2f;
    public string weaponName;
    private float timeToFire = 0;
    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }

    private bool ReadyToFire() {
        if (Time.time < timeToFire || bulletsMagazine == 0)
            return false;
        return true;
    }

    public virtual void Reload() {
        if (bulletsStock == 0 || bulletsMagazine == bulletsCapacity)
            return;
        
        timeToFire = Time.time + reloadTime;
        StartCoroutine(ReloadProcess());
    }

    IEnumerator ReloadProcess() {
        if (animator)
            animator.PlayInFixedTime("Reload", -1, reloadTime);
        
        yield return new WaitForSeconds(reloadTime);

        var restored = bulletsCapacity - bulletsMagazine;
        if (restored > bulletsStock)
            restored = bulletsStock;
        bulletsStock -= restored;
        bulletsMagazine += restored;
        animator.Play("IDLE");
    }

    public virtual bool Fire() {
        if (!ReadyToFire())
            return false;
        
        StopCoroutine("DoRecoil");
        transform.localPosition = Vector3.zero;

        Instantiate(fireVFX, firePoint.transform.position, firePoint.transform.rotation);
        bulletsMagazine -= 1;
        timeToFire = Time.time + fireRate;
        return true;
    }

    protected virtual IEnumerator DoRecoil() {
        float velocity = -recoilImpulse / 10;
        float resistanceAcceleration = 1f;
        for (; velocity < 0 || Vector3.Dot(Vector3.zero - transform.localPosition, transform.forward) > 0;) {
            velocity += resistanceAcceleration * Time.deltaTime;
            transform.position += transform.forward * velocity;
            yield return null;
        }
        transform.localPosition = Vector3.zero;
        if (bulletsMagazine == 0)
            Reload();
    }

    public void Disable() {
        animator.Rebind();
        gameObject.SetActive(false);
    }

    public void Enable() {
        gameObject.SetActive(true);
    }
}
