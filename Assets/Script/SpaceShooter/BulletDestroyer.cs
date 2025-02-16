using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            BulletPool.Instance.ReturnBullet("PlayerBullet", collision.gameObject);
        }
        if (collision.CompareTag("EnemyBullet"))
        {
            BulletPool.Instance.ReturnBullet("EnemyBullet", collision.gameObject);
        }
    }
}
