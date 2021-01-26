using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Config params
    [Header("Player")]
    [SerializeField] float moveSpeed=10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 2500;
    //[SerializeField] int health;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip hitSFX;
    [SerializeField] AudioClip lifeSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.7f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float shootSFXVolume = 0.25f;
    int power = 0;

    Coroutine fireCoroutine;

    float xMin, xMax, yMin,yMax;


    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int nrPlayers = FindObjectsOfType<Player>().Length;
        if (nrPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    void Start () {
        SetUpMoveBoundaries();
	}

    void Update () { 
        Move();
        Fire();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            LifeShield lifeShield = other.gameObject.GetComponent<LifeShield>();
            if (lifeShield)
            {
                AudioSource.PlayClipAtPoint(lifeSFX, Camera.main.transform.position, deathSFXVolume);
                health += lifeShield.GetLife();
                Destroy(other.gameObject);
                return;
            }
            else
            {
                power++;
                AudioSource.PlayClipAtPoint(lifeSFX, Camera.main.transform.position, deathSFXVolume);
                Destroy(other.gameObject);
                return;
            }
        }  
      
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position, deathSFXVolume);
        damageDealer.Hit();
        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }

    public int GetHealth()
    {
        return health;
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);
            if (power > 0)
            {
                for (int i = 1; i <= power; i++)
                {
                    GameObject laser1 = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
                    laser1.GetComponent<Rigidbody2D>().velocity = new Vector2(-i, projectileSpeed);
                    GameObject laser2 = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
                    laser2.GetComponent<Rigidbody2D>().velocity = new Vector2(+i, projectileSpeed);
                }
            }
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);  
        var newYPos = Mathf.Clamp(transform.position.y + deltaY,yMin,yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
