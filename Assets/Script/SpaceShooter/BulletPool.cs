using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    [System.Serializable]
    public class BulletType
    {
        public string tag; // "PlayerBullet" or "EnemyBullet"
        public GameObject prefab;
        public int poolSize = 20;
    }

    public List<BulletType> bulletTypes;
    private Dictionary<string, Queue<GameObject>> bulletPools = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        Instance = this;
        InitializePools();
    }

    private void InitializePools()
    {
        foreach (BulletType type in bulletTypes)
        {
            Queue<GameObject> bulletQueue = new Queue<GameObject>();
            for (int i = 0; i < type.poolSize; i++)
            {
                GameObject bullet = Instantiate(type.prefab);
                bullet.SetActive(false);
                bulletQueue.Enqueue(bullet);
            }
            bulletPools[type.tag] = bulletQueue;
            Debug.Log($"Initialized pool for {type.tag} with {type.poolSize} bullets.");
        }
    }

    public GameObject GetBullet(string tag, Vector2 position, Quaternion rotation)
    {
        if (bulletPools.ContainsKey(tag) && bulletPools[tag].Count > 0)
        {
            GameObject bullet = bulletPools[tag].Dequeue();
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;
            bullet.SetActive(true);
            Debug.Log($"Reusing {tag} bullet from pool.");
            return bullet;
        }
        else
        {
            BulletType bulletType = bulletTypes.Find(type => type.tag == tag);
            if (bulletType != null)
            {
                GameObject bullet = Instantiate(bulletType.prefab, position, rotation);
                Debug.LogWarning($"No bullets left in pool for {tag}, instantiating new one!");
                return bullet;
            }
        }

        Debug.LogError($"Bullet tag '{tag}' not found in pool!");
        return null;
    }

    public void ReturnBullet(string tag, GameObject bullet)
    {
        bullet.SetActive(false);
        if (bulletPools.ContainsKey(tag))
        {
            bulletPools[tag].Enqueue(bullet);
            Debug.Log($"{tag} bullet returned to pool. Current count: {bulletPools[tag].Count}");
        }
        else
        {
            Debug.LogError($"No pool found for bullet tag: {tag}! This bullet will be lost.");
        }
    }
}
