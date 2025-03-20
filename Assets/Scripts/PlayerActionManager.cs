using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    private Camera mainCam; 
    private Vector3 mousePos;
    public GameObject grenade;
    public Transform grenadeTransform;
    public GameObject bullet;
    public GameObject landmine;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;
    public float bulletWait = 0.001f;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true; 
                timer = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && canFire)
        {
            FireGrenade();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && canFire)
        {
            StartCoroutine(FireBullets());
        }    
        if (Input.GetKeyDown(KeyCode.Alpha3) && canFire)
        {
            PlaceLandmine();
        }
    }

    public void FireGrenade()
    {
        canFire = false;
        Instantiate(grenade, grenadeTransform.position, Quaternion.identity);
    }

    public void PlaceLandmine()
    {
        canFire = false;
        Instantiate(landmine, grenadeTransform.position, Quaternion.identity);
    }
    
    public void UziShoot()
    {
        StartCoroutine(FireBullets());
    }

    IEnumerator FireBullets()
    {
        canFire = false;

        // Instantiating 20 bullets with a delay between each instantiation
        for (int i = 0; i < 20; i++)
        {
            Instantiate(bullet, grenadeTransform.position, transform.rotation);
            yield return new WaitForSeconds(bulletWait);
        }

        // Resetting the canFire flag after all bullets are fired
        canFire = true;
    }
}
