using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponTypesEnum {
    Pistol = 1,
    Shotgun = 2,
    Riffle = 3,
}

public class BaseWeapon : MonoBehaviour
{
    public VFX fireVFX;
    public GameObject fakeFirePoint;
    public GameObject realFirePoint;
    public float recoilImpulse = 1f;
    public float fireRate = 0.4f;
    public float bulletsCapacity = 10;
    public float bulletsStock = 0;
    public float bulletsMagazine = 0;
    public float reloadTime = 1f;
    public string weaponName;
    public bool allowBurstShooting = false;
    private float timeToFire = 0;
    public float strifeRadius = 4f;
    private bool isBurst = false;
    protected float burstCounter;

    private Animator animator;

    protected virtual void Start() {
        animator = GetComponent<Animator>();
    }

    protected Quaternion GetStrifedRotation(float strife) {
        var rotation = realFirePoint.transform.rotation.eulerAngles;
        rotation += new Vector3(Random.Range(-strifeRadius, strife),
                                Random.Range(-strifeRadius, strife),
                                Random.Range(-strifeRadius, strife));
        return Quaternion.Euler(rotation);
    }

    private bool ReadyToFire() {
        if (Time.time < timeToFire || bulletsMagazine == 0 || (allowBurstShooting | !isBurst) == false)
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

    public virtual bool Fire(bool burstShooting=false) {
        if (!ReadyToFire()) {
            isBurst = false;
            burstCounter = 0;
            return false;
        }

        burstCounter++;
        if (!burstShooting) {
            burstCounter = 0;
            isBurst = false;
        }
        isBurst = true;
        
        StopCoroutine("DoRecoil");
        transform.localPosition = Vector3.zero;

        if (fireVFX)
            Instantiate(fireVFX, fakeFirePoint.transform.position, fakeFirePoint.transform.rotation);
        bulletsMagazine -= 1;
        timeToFire = Time.time + fireRate;
        return true;
    }

    protected virtual IEnumerator DoRecoil(float additionalRecoil=0) {
        float velocity = -(recoilImpulse + additionalRecoil) / 10;
        float resistanceAcceleration = 1f;
        for (; velocity < 0 || Vector3.Dot(Vector3.zero - transform.localPosition, transform.forward) > 0;) {
            velocity += resistanceAcceleration * Time.deltaTime;
            transform.position += transform.forward * velocity;
            yield return null;
        }
        transform.localPosition = Vector3.zero;
        if (bulletsMagazine == 0) {
            isBurst = false;
            Reload();
        }
    }

    public void Disable() {
        animator.Rebind();
        isBurst = false;
        gameObject.SetActive(false);
    }

    public void Enable() {
        gameObject.SetActive(true);
    }
}
