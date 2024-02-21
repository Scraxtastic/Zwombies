using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int bulletPoolSize;
    
    [Header("Tilemap")]
    [SerializeField] private Tilemap tilemap;

    [SerializeField] private TilemapHealth tilemapHealth; 


    [Header("DEBUG")] [SerializeField] private List<Bullet> bulletPool = new List<Bullet>();
    [SerializeField] private List<Bullet> bulletsInUse = new List<Bullet>();

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        EnsureInstance();
    }

    void OnEnable()
    {
        // Necessary to compile smoothly
        EnsureInstance();
    }

    private void EnsureInstance()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        if (Instance == this)
        {
            return;
        }
        Instance = this;
        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject bulletObj = Instantiate(bulletPrefab, transform);
            bulletObj.SetActive(false);
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            bulletPool.Add(bullet);
        }

        DontDestroyOnLoad(Instance.gameObject);
    }

    public Bullet GetBullet()
    {
        if (bulletPool.Count == 0)
        {
            GameObject createdBulletObj = Instantiate(bulletPrefab, transform);
            createdBulletObj.SetActive(true);
            Bullet createdBullet = createdBulletObj.GetComponent<Bullet>();
            bulletsInUse.Add(createdBullet);
            return createdBullet;
        }

        Bullet bullet = bulletPool[0];
        bulletPool.RemoveAt(0);
        bullet.gameObject.SetActive(true);
        bulletsInUse.Add(bullet);
        return bullet;
    }

    public void ReturnBulletToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bulletsInUse.Remove(bullet);
        bulletPool.Add(bullet);
    }
    
    public Tilemap GetTilemap()
    {
        return tilemap;
    }
    
    public TilemapHealth GetTilemapHealth()
    {
        return tilemapHealth;
    }
}