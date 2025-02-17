using DependencyInjection; // https://github.com/adammyhre/Unity-Dependency-Injection-Lite
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
//[RequireComponent(typeof(AnimationController))]
public class GoapAgent : MonoBehaviour
{
    [Header("Sensors")]
    [SerializeField] protected Sensor chaseSensor; // wide range => can chase
    [SerializeField] protected Sensor attackSensor; // small range => close enough to attack

    [SerializeField] TMP_Text text;


    protected BeliefFactory beliefFactory;

    // References to the components of this particular agent
    public NavMeshAgent navMeshAgent;
    //AnimationController animations;
    Rigidbody rb;


    protected Animator animator;
    CountdownTimer statsTimer;

    GameObject target;
    Vector3 destination;

    AgentGoal lastGoal; // ensures we are not trying to achieve the same goal twice in a row
    public AgentGoal currentGoal;
    public ActionPlan actionPlan; // stack of actions
    public AgentAction currentAction; // pop from the stack and save here

    public Dictionary<string, AgentBeliefs> beliefs;
    public HashSet<AgentAction> actions;
    public HashSet<AgentGoal> goals;

    [Inject] GoapFactory gFactory;
    IGoapPlanner gPlanner;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        //animations = GetComponent<AnimationController>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        gPlanner = new GoapPlanner();
        //gPlanner = gFactory.CreatePlanner();
    }

    protected virtual void Start()
    {
        beliefFactory = new BeliefFactory(this, beliefs);
        SetupBeliefs();
        SetupActions();
        SetupGoals();
    }

    protected virtual void SetupBeliefs()
    {
        beliefs = new Dictionary<string, AgentBeliefs>();
        BeliefFactory factory = new BeliefFactory(this, beliefs);

        factory.AddBelief("Nothing", () => false);
        factory.AddBelief("AgentIdle", () => !navMeshAgent.hasPath);
        factory.AddBelief("AgentMoving", () => navMeshAgent.hasPath);

        factory.AddSensorBelief("EnemyInChaseRange", chaseSensor);
        factory.AddSensorBelief("EnemyInAttackRange", attackSensor);

        factory.AddBelief("AttackLoaded", () => false);
        factory.AddBelief("AttackingEnemy", () => false);

        // HEALTH
        //factory.AddBelief("AgentHealthLow", () => Health < 30);
        //factory.AddBelief("AgentIsHealthy", () => Health >= 50);

        // STAMINA
        //factory.AddBelief("AgentStaminaLow", () => stamina < 10);
        //factory.AddBelief("AgentIsRested", () => stamina >= 60);

        // CHECK IF WE ARE AT THE DESIRED LOCATION
        //factory.AddLocationBelief("AgentAtDoorOne", 3f, doorOnePosition);
        //factory.AddLocationBelief("AgentAtDoorTwo", 3f, doorTwoPosition);
        //factory.AddLocationBelief("AgentAtRestingPosition", 3f, restingPosition);
        //factory.AddLocationBelief("AgentAtFoodShack", 3f, foodShack);


    }

    protected virtual void SetupActions()
    {
        actions = new HashSet<AgentAction>();

        //actions.Add(new AgentAction.Builder("Relax")
        //    .WithStrategy(new IdleStrategy(5))
        //    .AddEffect(beliefs["Nothing"])
        //    .Build());

        //actions.Add(new AgentAction.Builder("Wander Around")
        //    .WithStrategy(new WanderStrategy(navMeshAgent, 10))
        //    .AddEffect(beliefs["AgentMoving"])
        //    .Build());


        actions.Add(new AgentAction.Builder("ChaseEnemy")
            .WithStrategy(new MoveStrategy(navMeshAgent, () => beliefs["EnemyInChaseRange"].Location, animator))
            .AddPrecondition(beliefs["EnemyInChaseRange"])
            .AddEffect(beliefs["EnemyInAttackRange"])
            .Build());

        actions.Add(new AgentAction.Builder("Wander Around")
            .WithStrategy(new WanderStrategy(navMeshAgent, 10, animator))
            .AddEffect(beliefs["AgentMoving"])
            .Build());

        actions.Add(new AgentAction.Builder("Relax")
            .WithStrategy(new IdleStrategy(1, animator))
            .AddEffect(beliefs["Nothing"])
            .Build());


        // HEAL
        //actions.Add(new AgentAction.Builder("MoveToEatingPosition")
        //    .WithStrategy(new MoveStrategy(navMeshAgent, () => foodShack.position))
        //    .AddEffect(beliefs["AgentAtFoodShack"])
        //    .Build());

        //actions.Add(new AgentAction.Builder("Eat") // Assume that we are healthy after 8 seconds
        //    .WithStrategy(new IdleStrategy(8))  // Later replace with a Command
        //    .AddPrecondition(beliefs["AgentAtFoodShack"])
        //    .AddEffect(beliefs["AgentIsHealthy"])
        //    .Build());

        // Go through doors
        //actions.Add(new AgentAction.Builder("MoveToDoorOne")
        //    .WithStrategy(new MoveStrategy(navMeshAgent, () => doorOnePosition.position))
        //    .AddEffect(beliefs["AgentAtDoorOne"])
        //    .Build());

        //actions.Add(new AgentAction.Builder("MoveToDoorTwo")
        //    .WithStrategy(new MoveStrategy(navMeshAgent, () => doorTwoPosition.position))
        //    .AddEffect(beliefs["AgentAtDoorTwo"])
        //    .Build());

        // REST 
        //actions.Add(new AgentAction.Builder("MoveFromDoorOneToRestArea")
        //    .WithCost(2)
        //    .WithStrategy(new MoveStrategy(navMeshAgent, () => restingPosition.position))
        //    .AddPrecondition(beliefs["AgentAtDoorOne"])
        //    .AddEffect(beliefs["AgentAtRestingPosition"])
        //    .Build());

        //actions.Add(new AgentAction.Builder("MoveFromDoorTwoRestArea")
        //    .WithStrategy(new MoveStrategy(navMeshAgent, () => restingPosition.position))
        //    .AddPrecondition(beliefs["AgentAtDoorTwo"])
        //    .AddEffect(beliefs["AgentAtRestingPosition"])
        //    .Build());

        //actions.Add(new AgentAction.Builder("Rest")
        //    .WithStrategy(new IdleStrategy(8))
        //    .AddPrecondition(beliefs["AgentAtRestingPosition"])
        //    .AddEffect(beliefs["AgentIsRested"])
        //    .Build());

        // CHASE / ATTACK 
        //.WithStrategy(new AttackStrategy(animations))
    }

    protected virtual void SetupGoals()
    {
        goals = new HashSet<AgentGoal>();

        goals.Add(new AgentGoal.Builder("SeekAndDestroy")
            .WithPriority(5)
            .WithDesiredEffect(beliefs["AttackingEnemy"])
            .Build());

        goals.Add(new AgentGoal.Builder("ChillOut")
            .WithPriority(1)
            .WithDesiredEffect(beliefs["Nothing"])
            .Build());

        goals.Add(new AgentGoal.Builder("Wander")
            .WithPriority(1)
            .WithDesiredEffect(beliefs["AgentMoving"])
            .Build());



        //// HEAL
        //goals.Add(new AgentGoal.Builder("KeepHealthUp")
        //    .WithPriority(3)
        //    .WithDesiredEffect(beliefs["AgentIsHealthy"])
        //    .Build());

        // REST
        //goals.Add(new AgentGoal.Builder("KeepStaminaUp")
        //    .WithPriority(2)
        //    .WithDesiredEffect(beliefs["AgentIsRested"])
        //    .Build());

        // ATTACK
    }

    //void SetupTimers()
    //{ // update the upgradableStats every 2s
    //  statsTimer = new CountdownTimer(2f);
    //  statsTimer.OnTimerStop += () => {
    //    UpdateStats();
    //    statsTimer.Start();
    //  };
    //  statsTimer.Start();
    //}

    //// TODO move to upgradableStats system
    //void UpdateStats()
    //{
    //  //stamina += InRangeOf(restingPosition.position, 3f) ? 20 : -10;
    //  //Health += InRangeOf(foodShack.position, 3f) ? 20 : -5;
    //  //stamina = Mathf.Clamp(stamina, 0, 100);
    //  //Health = Mathf.Clamp(Health, 0, 100);
    //}

    // heal only in range of a certain location
    bool InRangeOf(Vector3 pos, float range) => Vector3.Distance(transform.position, pos) < range;

    // reevaluate the action or entire plan based on the data from the wider sensor
    void OnEnable() => chaseSensor.OnTargetChanged += HandleTargetChanged;
    void OnDisable() => chaseSensor.OnTargetChanged -= HandleTargetChanged;

    void HandleTargetChanged()
    { // with no action and no goal the planner will start creating a new plan
      //Debug.Log("Target changed, clearing current action and goal");
      // Force the planner to re-evaluate the plan
        currentAction = null;
        currentGoal = null;
    }

    protected virtual void Update()
    {
        if (text != null && currentGoal != null && currentAction != null)
        {
            text.text = currentGoal.Name + ", " + currentAction.Name;
        }
        //statsTimer.Tick(Time.deltaTime);
        //animations.SetSpeed(navMeshAgent.velocity.magnitude);

        // OnUpdate the plan and current action if there is one
        if (currentAction == null)
        {
            //Debug.Log("Calculating any potential new plan");
            CalculatePlan();

            if (actionPlan != null && actionPlan.Actions.Count > 0) // we have a plan and actions to perform
            {
                navMeshAgent.ResetPath();

                currentGoal = actionPlan.AgentGoal;
                //Debug.Log(gameObject.name + $" goal: {currentGoal.Name} with {actionPlan.Actions.Count} actions in plan");
                currentAction = actionPlan.Actions.Pop();
                //Debug.Log($"Popped action: {currentAction.Name}");

                // Verify all precondition effects are true
                if (currentAction.Preconditions.All(b => b.Evaluate()))
                {
                    currentAction.Start();
                    //Debug.LogError("ATTACK!!!");
                }
                else
                {
                    //Debug.Log("Preconditions not met, clearing current action and goal");
                    currentAction = null;
                    currentGoal = null;
                }
            }
        }

        // If we have a current action, execute it
        if (actionPlan != null && currentAction != null)
        {
            currentAction.Update(Time.deltaTime);

            if (currentAction.Complete)
            {
                //Debug.Log($"{currentAction.Name} complete");
                currentAction.Stop();
                currentAction = null;

                if (actionPlan.Actions.Count == 0)
                {
                    //Debug.Log("Plan complete");
                    lastGoal = currentGoal;
                    currentGoal = null;
                }
            }
        }
    }

    void CalculatePlan()
    {
        var priorityLevel = currentGoal?.Priority ?? 0;

        HashSet<AgentGoal> goalsToCheck = goals;

        // we have a current goal, so we only want to check goals with higher priority
        if (currentGoal != null)
        {
            //Debug.Log("Current goal exists, checking goals with higher priority");
            goalsToCheck = new HashSet<AgentGoal>(goals.Where(g => g.Priority > priorityLevel));
        }

        var potentialPlan = gPlanner.Plan(this, goalsToCheck, lastGoal);
        if (potentialPlan != null)
        {
            actionPlan = potentialPlan;
        }
    }
}