using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Player
{
    public override void Attack()
    {
<<<<<<< HEAD
        GameObject pullBullet = ObjectPooler.SpawnFromPool("Bullet2D", gameObject.transform.position);
        pullBullet.gameObject.SetActive(true);
        // Bullet에 발사 정보 전달
        if (pullBullet.TryGetComponent<Bullet>(out Bullet bull))
        {
            bullet = bull;
        }
        else
        {
            bullet = pullBullet.AddComponent<Bullet>();
        }

        bullet.SetShooter(gameObject);

        if (pullBullet.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            bulletRb = rb;
        }
        else
        {
            bulletRb = pullBullet.AddComponent<Rigidbody2D>();
        }

        // 속도 가중치는 서버 데이터 업로드 후 변경 (5 -> projectileSpeed)
        bulletRb.velocity = targetDirection * 5;
=======
            GameObject pullBullet = ObjectPooler.SpawnFromPool("Bullet2D", gameObject.transform.position);

            // Bullet에 발사 정보 전달
            if (pullBullet.TryGetComponent<Bullet>(out Bullet bull))
            {
                bullet = bull;
            }
            else
            {
                bullet = pullBullet.AddComponent<Bullet>();
            }

            bullet.SetShooter(gameObject);

            if (pullBullet.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                bulletRb = rb;
            }
            else
            {
                bulletRb = pullBullet.AddComponent<Rigidbody2D>();
            }

            // 속도 가중치는 서버 데이터 업로드 후 변경
            bulletRb.velocity = targetDirection * projectileSpeed;
>>>>>>> 7b445977e969774cac433a4390f64c66acca0c77
    }
    public void PlayerHit(int damageAmount)
    {
        playerCurHp -= damageAmount;

        Debug.Log($"Player Hit Cur HP : {playerCurHp}");

        if (playerCurHp <= 0)
        {
            Debug.Log("Player Die");
            Die();
        }
    }

    public void Reward(int exp)
    {
        playerCurExp += exp;

        if (playerCurExp >= playerMaxExp)
        {
            // Level Up
        }
    }
}
