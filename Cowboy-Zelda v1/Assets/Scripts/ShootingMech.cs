﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMech : MonoBehaviour
{
    public float offset;
    public float bulletCount;

    public int maxAmmo = 3;
    private int currentAmmo;
    public float reloadTime = 2f;
    private bool isReloading = false;

    public GameObject bulletPrefab;
    public Transform shotPoint;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject == null)
        {
            FindObjectOfType<GameManager>().EndGame();
        }

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal")*2, Input.GetAxis("Vertical")*2, 0.0f);//moves character

        //animation of sprites
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        transform.position = transform.position + movement * Time.deltaTime;

        if (isReloading)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButtonDown(0)){//button press to shoot
            GameObject projectile = Instantiate(bulletPrefab, shotPoint.position, Quaternion.FromToRotation(new Vector3(1, 0, 0), movement));
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.cowboy = gameObject;
            currentAmmo--;
        }



        //Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;//makes bullet follow mouse
        //float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f,0f,rotZ + offset);



        /*if(Input.GetAxis("Horizontal") != 0){
            if(Input.GetAxis("Horizontal") > 0){
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }   else{
            transform.rotation = Quaternion.Euler(0, -180, 0);
            }
        }   else if(Input.GetAxis("Vertical") != 0){
                if(Input.GetAxis("Vertical") >= 0){
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }   else{
                    transform.rotation = Quaternion.Euler(-180, 0, 0);
                }
        }*/

    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

}
