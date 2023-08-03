using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public partial class Player
{
    public PlayerBaseData playerBaseData;
    public Rigidbody2D bulletRb;
    public Rigidbody2D playerRb;
    public Bullet bullet;

    // 임시 : 서버로부터 받아야됨
    [Header("User Setting")]
    [SerializeField] float playerSpeed;
    [SerializeField] public float playerMaxHp;
    [SerializeField] public float playerCurHp;
    public int playerAttackPower;
    public int playerLv;
    public int playerMaxExp;
    public int playerCurExp = 0;
    public int score = 0;
    public float playerMovementSpeed;
    public float projectileSpeed = 5f;
    public float rateOfFire = 0.3f;
    public float lastAttackTime = 0;

    Vector3 targetDirection;

    public bool IsDeath;

    private void InitComponent()
    {
        if (gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody))
            playerRb = rigidbody;
        else
            playerRb = gameObject.AddComponent<Rigidbody2D>();
        playerRb.gravityScale = 0;
    }

    private void InitSetting()
    {
        playerSpeed = 3f;

        // Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void InitPlayer()
    {
        // Test
        playerMaxHp = 100;
        playerCurHp = playerMaxHp;

        if (APIManager.Instacne.isLogin)
        {
            playerMaxHp = playerBaseData.hp;
            playerMaxHp = 100;
            playerCurHp = playerMaxHp;
            playerLv = playerBaseData.level;
            playerMaxExp = playerBaseData.exp;
            IsDeath = false;
        }
    }
}
