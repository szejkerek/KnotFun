using System.Collections.Generic;
using UnityEngine;

public class AllyAgent : GoapAgent
{
    public AllyAgent(Transform player)
    {
        playerPosition = player;
    }
    public VoiceCommand StayCommandSO;
    public VoiceCommand FollowCommandSO;
    public VoiceCommand FleeCommandSO;
    public VoiceCommand NormalStanceCommandSO;
    public VoiceCommand AggressiveStanceCommandSO;
    public VoiceCommand DefensiveStanceCommandSO;
    public VoiceCommand AttackNormalCommandSO;
    public VoiceCommand AttackWeakCommandSO;
    public VoiceCommand AttackStrongCommandSO;
    [Space]
    private Transform playerPosition;
    [SerializeField] SpriteRenderer iconBack, iconFront;
    [SerializeField] Sprite neutral, go, flee, sword, shield;
    [SerializeField] Color weak, any, strong;
    [SerializeField] List<SoundOld> soundVariations;
    private int currentAttackTemp = 0;
    private int currentDefendTemp = 0;
    private Samurai thisSamurai;
    [Space]
    [SerializeField] float commandDetectionRange;
    [Space]
    [SerializeField] int defensiveStanceDefense;
    [SerializeField] int defensiveStanceAttack;
    [Space]
    [SerializeField] int offensiveStanceAttack;
    [SerializeField] int offensiveStanceDefense;


    //private enum  = false;

    protected override void Start()
    {
        base.Start();
        thisSamurai = GetComponent<Samurai>();
        //soundVariations = new List<Sound>();
    }
    protected override void Update()
    {

        base.Update();
        if (Input.GetKeyUp("1"))
        {
            PlayHaiSound();
            StayCommandSO.Execute();
        }
        else if (Input.GetKeyUp("2"))
        {
            PlayHaiSound();
            FollowCommandSO.Execute();
        }
        else if (Input.GetKeyUp("3"))
        {
            PlayHaiSound();
            FleeCommandSO.Execute();
        }
        if (Input.GetKeyUp("4"))
        {
            PlayHaiSound();
            NormalStanceCommandSO.Execute();
        }
        else if (Input.GetKeyUp("5"))
        {
            PlayHaiSound();
            AggressiveStanceCommandSO.Execute();
        }
        else if (Input.GetKeyUp("6"))
        {
            PlayHaiSound();
            DefensiveStanceCommandSO.Execute();
        }
        if (Input.GetKeyUp("7"))
        {
            PlayHaiSound();
            AttackNormalCommandSO.Execute();
        }
        else if (Input.GetKeyUp("8"))
        {
            PlayHaiSound();
            AttackWeakCommandSO.Execute();
        }
        else if (Input.GetKeyUp("9"))
        {
            PlayHaiSound();
            AttackStrongCommandSO.Execute();
        }
    }


    private void PlayHaiSound()
    {
        if (soundVariations.Count == 0) return;
        AudioManagerOld.Instance.PlayAtPosition(transform.position, soundVariations.SelectRandomElement());
    }


    protected override void SetupBeliefs()
    {
        base.SetupBeliefs();
        BeliefFactory factory = new BeliefFactory(this, beliefs);
        factory.AddLocationBelief("NearPlayer", 3f, FindAnyObjectByType<GameplayTeamManagement>().general.transform);
    }

    protected override void SetupActions()
    {
        base.SetupActions();

        actions.Add(new AgentAction.Builder("MoveToPlayer")
        .WithStrategy(new MoveStrategy(navMeshAgent, () =>
        playerPosition != null ? playerPosition.transform.position : GameObject.FindGameObjectWithTag("Player").transform.position, animator))
        .AddEffect(beliefs["NearPlayer"])
        .Build());


        actions.Add(new AgentAction.Builder("AttackEnemy")
            .WithStrategy(new AttackStrategy(0.5f, attackSensor, 10, animator, this))
            .AddPrecondition(beliefs["EnemyInAttackRange"])
            .AddEffect(beliefs["AttackingEnemy"])
            .Build());


    }

    protected override void SetupGoals()
    {
        base.SetupGoals();

        AttackCommand.OnAttackRecognized += AggressiveStanceCommand; // Kogeki (Kougeki) - Attack
        DefenseCommand.OnDefenseRecognized += DefensiveStanceCommand; // Mamoru - Defend
        AttackBigCommand.OnAttackBigRecognized += AttackStrongCommand; // bigu -> attack big (max Health enemy)
        AttackSmallCommand.OnAttackSmallRecognized += AttackWeakCommand; // smalu -> attack small (min Health enemy)
        MarchWanderCommand.onWanderRecognized += FleeCommand; // Gyoko (Gyoukou) - March
        FollowSupportCommand.onFollowRecognized += FollowCommand; // Hojo - Support
        StayIdleCommand.OnIdleBigRecognized += StayCommand; // Tome - Stop
        StanceNormalCommand.OnNormalStanceRecognized += NormalStanceCommand;// Nomaru - Normal (stance)
        NormalAttackCommand.OnAttackNormalRecognized += AttackNormalCommand;// Tatakau - Fight / Attack Normal

        StayCommand(null);
        NormalStanceCommand(null);
        AttackNormalCommand(null);

    }

    private void OnDestroy()
    {
        AttackCommand.OnAttackRecognized -= AggressiveStanceCommand; // Kogeki (Kougeki) - Attack
        DefenseCommand.OnDefenseRecognized -= DefensiveStanceCommand; // Mamoru - Defend
        AttackBigCommand.OnAttackBigRecognized -= AttackStrongCommand; // bigu -> attack big (max Health enemy)
        AttackSmallCommand.OnAttackSmallRecognized -= AttackWeakCommand; // smalu -> attack small (min Health enemy)
        MarchWanderCommand.onWanderRecognized -= FleeCommand; // Gyoko (Gyoukou) - March
        FollowSupportCommand.onFollowRecognized -= FollowCommand; // Hojo - Support
        StayIdleCommand.OnIdleBigRecognized -= StayCommand; // Tome - Stop
        StanceNormalCommand.OnNormalStanceRecognized -= NormalStanceCommand;// Nomaru - Normal (stance)
        NormalAttackCommand.OnAttackNormalRecognized -= AttackNormalCommand;// Tatakau - Fight / Attack Normal
    }

    public void ResetStance()
    {
        thisSamurai.temporaryStats.Damage -= currentAttackTemp;
        thisSamurai.temporaryStats.Armor -= currentDefendTemp;
        currentDefendTemp = 0;
        currentAttackTemp = 0;
    }

    private bool NearPlayer(float range)
    {
        if (playerPosition == null)
        {
            playerPosition = FindAnyObjectByType<SamuraiGeneral>().transform;
        }
        if (Vector3.Distance(playerPosition.position, transform.position) <= range)
        {
            return true;
        }
        else return false;
    }

    public void AggressiveStanceCommand(AttackCommand command)
    {
        if (!NearPlayer(commandDetectionRange)) return;
        iconFront.sprite = sword;
        if (thisSamurai == null) return;
        ResetStance();
        thisSamurai.temporaryStats.Damage += offensiveStanceAttack;
        thisSamurai.temporaryStats.Armor += offensiveStanceDefense;

        currentAttackTemp = offensiveStanceAttack;
        currentDefendTemp = offensiveStanceDefense;
    }

    public void DefensiveStanceCommand(DefenseCommand command)
    {
        if (!NearPlayer(commandDetectionRange)) return;
        iconFront.sprite = shield;
        if (thisSamurai == null) return;
        ResetStance();
        thisSamurai.temporaryStats.Damage += defensiveStanceAttack;
        thisSamurai.temporaryStats.Armor += defensiveStanceDefense;

        currentAttackTemp = defensiveStanceAttack;
        currentDefendTemp = defensiveStanceDefense;

    }
    public void NormalStanceCommand(StanceNormalCommand command)
    {
        if (!NearPlayer(commandDetectionRange)) return;
        iconFront.sprite = null;
        if (thisSamurai == null) return;
        ResetStance();
    }
    public void AttackWeakCommand(AttackSmallCommand command) // Kogeki chisai (Kougeki chiisaii) / Kogeki sumoru -> attack small (min Health enemy)
    {
        if (!NearPlayer(commandDetectionRange)) return;
        iconBack.color = weak;
        SetupSensors(Sensor.TargetingMode.Weakest);

    }
    public void AttackStrongCommand(AttackBigCommand command) // Kogeki Oki (Kougeki ookii) / Kogeki biggu -> attack big (max Health enemy)
    {
        if (!NearPlayer(commandDetectionRange)) return;
        iconBack.color = strong;
        SetupSensors(Sensor.TargetingMode.Strongest);
    }
    public void AttackNormalCommand(NormalAttackCommand command)
    {
        if (!NearPlayer(commandDetectionRange)) return;
        iconBack.color = any;
        SetupSensors(Sensor.TargetingMode.Normal);
    }
    private void SetupSensors(Sensor.TargetingMode mode)
    {
        if (!NearPlayer(commandDetectionRange)) return;
        attackSensor.targetingMode = mode;
        chaseSensor.targetingMode = mode;
    }
    public void FleeCommand(MarchWanderCommand command) // Gyoko (Gyoukou) - March FLEEEE
    {
        if (!NearPlayer(commandDetectionRange)) return;
        iconBack.sprite = flee;

        if (CommadExists("GroupUp"))
        {
            goals.Remove(GetGoal("GroupUp"));
        }


        if (!CommadExists("GroupUp"))
        {
            goals.Add(new AgentGoal.Builder("Wander")
            .WithPriority(5)
            .WithDesiredEffect(beliefs["AgentMoving"])
            .Build());

            goals.Add(new AgentGoal.Builder("GroupUp")
            .WithPriority(10)
            .WithDesiredEffect(beliefs["NearPlayer"])
            .Build());

        }
    }
    public void StayCommand(StayIdleCommand command) // Tome - Stop
    {
        if (!NearPlayer(commandDetectionRange)) return;
        iconBack.sprite = neutral;
        Debug.Log("STAY!");
        if (CommadExists("GroupUp"))
        {
            goals.Remove(GetGoal("GroupUp"));
        }
        if (CommadExists("Wander"))
        {
            goals.Remove(GetGoal("Wander"));
        }
    }
    public void FollowCommand(FollowSupportCommand command) // Hojo (Hojou) - Support
    {
        if (!NearPlayer(commandDetectionRange)) return;
        iconBack.sprite = go;
        Debug.Log("FOLLOW!");
        if (CommadExists("GroupUp"))
        {
            goals.Remove(GetGoal("GroupUp"));
        }
        if (CommadExists("Wander"))
        {
            goals.Remove(GetGoal("Wander"));
        }

        if (CommadExists("GroupUp")) return;


        goals.Add(new AgentGoal.Builder("GroupUp")
            .WithPriority(3)
            .WithDesiredEffect(beliefs["NearPlayer"])
            .Build());
    }

    private bool CommadExists(string name)
    {
        bool exists = false;
        foreach (AgentGoal g in goals)
        {
            if (g.Name == name)
            {
                exists = true;
                break;
            }
        }

        return exists;
    }

    private AgentGoal GetGoal(string name)
    {
        foreach (AgentGoal g in goals)
        {
            if (g.Name == name)
            {
                return g;
            }
        }

        return null;
    }

}
