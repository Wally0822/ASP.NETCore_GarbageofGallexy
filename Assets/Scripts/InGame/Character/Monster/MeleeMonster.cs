using System.Collections;
using APIModels;
using UnityEngine;

public class MeleeMonster : MonsterBase
{
    private MonsterData_res[] meleeMonsterStatus;

    [SerializeField] private MonsterSFX monsterSFX; 
    [SerializeField] private bool isMeleeMonsterDead;
    
    #region unity event func

    protected override void Awake()
    {
        base.Awake();
        GetInitMonsterStatus();
        monsterSFX = gameObject.AddComponent<MonsterSFX>();

        isMeleeMonsterDead = false;
    }

    // stage 변경에 따른 Level별 능력치 부여 => 서버 정보 받아오기
    protected override void OnEnable()
    {
        base.OnEnable();
        isSelfDestruct = false;

        if (isMeleeMonsterDead == true)
        {
            SetMeleeMonsterStatus(stageNum);
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        isMeleeMonsterDead = true;
    }
    #endregion

    protected override void GetInitMonsterStatus()
    {
        meleeMonsterStatus = APIManager.Instance.GetValueByKey<MonsterData_res[]>(MasterDataDicKey.MeleeMonster.ToString());

        if (meleeMonsterStatus == null)
        {
            Debug.LogError("meleeMonsterStatus Data is Null");
        }
    }

    protected override void SetMonsterName()
    {
        MonsterName = "BasicMeleeMonster";
    }

    protected void SetMeleeMonsterStatus(int inputStageNum)
    {
        _monsterInfo.level = meleeMonsterStatus[inputStageNum].level;
        _monsterInfo.exp = meleeMonsterStatus[inputStageNum].exp;
        _monsterInfo.hp = meleeMonsterStatus[inputStageNum].hp;
        _monsterInfo.curHp = _monsterInfo.hp;
        _monsterInfo.speed = meleeMonsterStatus[inputStageNum].speed;
        _monsterInfo.rate_of_fire = meleeMonsterStatus[inputStageNum].rate_of_fire;
        _monsterInfo.projectile_speed = meleeMonsterStatus[inputStageNum].projectile_speed;
        _monsterInfo.collision_damage = meleeMonsterStatus[inputStageNum].collision_damage;
        _monsterInfo.score = meleeMonsterStatus[inputStageNum].score;
        _monsterInfo.ranged = meleeMonsterStatus[inputStageNum].ranged;
    }

    protected override void MonsterStatusUpdate()
    {
        //  Debug.LogError("MonsterStatusUpdate : " + stageNum);
        SetMeleeMonsterStatus(stageNum);
    }


    public override void Attack()
    {
        isSelfDestruct = true;

        PlayerHit();
        monsterSFX.AttackSFX();
        MonsterDeath(); // 자폭에 의한 공격은 보상 X
    }

    public override void Hit()
    {
        _monsterInfo.curHp -= player.playerAttackPower;
        SoundMgr.Instance.SFXPlay(EnumTypes.SFXType.MonsterHit);

        if (_monsterInfo.curHp <= 0)
        {
            player.Reward(_monsterInfo.exp, _monsterInfo.score);
            MonsterDeath();
        }
    }

    public void PlayerHit()
    {
        player.PlayerHit(_monsterInfo.collision_damage);
    }

    protected override IEnumerator State_Move()
    {
        // 추후 몬스터 별 이동속도 및 공격 범위 추가
        return base.State_Move();
    }
}