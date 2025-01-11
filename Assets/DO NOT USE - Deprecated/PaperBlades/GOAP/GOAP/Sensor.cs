using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))] // every sensor has to have a collider of some sort
public class Sensor : MonoBehaviour
{
    public enum TargetingMode { Normal, Strongest, Weakest };


    [SerializeField] float detectionRadius = 5.0f; // radius of the sensor
    [SerializeField] float timerInterval = 1.0f; // how often to check the sensor (instead of every frame)
    [SerializeField] List<string> targetTags;
    [SerializeField] List<GameObject> targetsInRange;
    [SerializeField] public TargetingMode targetingMode = TargetingMode.Normal;


    SphereCollider detectionRange;

    public event Action OnTargetChanged = delegate { };

    public Vector3 TargetPosition => target ? target.transform.position : Vector3.zero;
    public bool IsTargetInRange => TargetPosition != Vector3.zero;

    public GameObject target;
    Vector3 lastKnownPosition;
    CountdownTimer timer;

    void Awake()
    {
        if (targetTags == null) targetTags = new List<string>();
        targetsInRange = new List<GameObject>();
        detectionRange = GetComponent<SphereCollider>();
        detectionRange.isTrigger = true;
        detectionRange.radius = detectionRadius;
    }

    void Start()
    {
        timer = new CountdownTimer(timerInterval);
        timer.OnTimerStop += () =>
        {
            UpdateTargetPosition();
            timer.Start();
        };
        timer.Start();
    }

    private void Update()
    {
        timer.Tick(Time.deltaTime);
    }

    void UpdateTargetPosition()
    {
        if (targetsInRange.Count == 0) target = null;
        else
        {
            GameObject currentTarget = targetsInRange[0];
            float minDistance = 0;

            switch (targetingMode)
            {
                case TargetingMode.Strongest:
                    currentTarget = GetStrongest();
                    break;
                case TargetingMode.Weakest:
                    currentTarget = GetStrongest(false);
                    break;
                default:
                    List<GameObject> toDelete = new List<GameObject>();
                    foreach (GameObject t in targetsInRange)
                    {
                        if (t == null)
                        {
                            toDelete.Add(t);
                            continue;
                        }
                        float currDistance = Vector3.Distance(transform.position, t.transform.position);
                        if (currDistance < minDistance)
                        {
                            minDistance = currDistance;
                            currentTarget = t;
                        }
                    }
                    foreach (GameObject t in toDelete)
                    {
                        targetsInRange.Remove(t);
                    }
                    break;

            }

            target = currentTarget;
        }

        if (IsTargetInRange && (lastKnownPosition != TargetPosition || lastKnownPosition != Vector3.zero))
        {
            lastKnownPosition = TargetPosition;
            OnTargetChanged.Invoke(); // target moved or new target
        }
    }

    //trigger collider for the player (JUST the player)
    void OnTriggerEnter(Collider other)
    {
        bool hasPropperTag = false;
        foreach (string s in targetTags) if (other.CompareTag(s)) hasPropperTag = true;

        if (!hasPropperTag) { return; }

        if (!targetsInRange.Contains(other.gameObject)) targetsInRange.Add(other.gameObject);

        UpdateTargetPosition();
    }

    //trigger collider for the player (JUST the player)
    void OnTriggerExit(Collider other)
    {
        bool hasPropperTag = false;
        foreach (string s in targetTags) if (other.CompareTag(s)) hasPropperTag = true;

        if (!hasPropperTag) { return; }

        if (targetsInRange.Contains(other.gameObject)) targetsInRange.Remove(other.gameObject);

        UpdateTargetPosition();
    }

    private void OnDrawGizmos() // visible when target is in range
    {
        Gizmos.color = IsTargetInRange ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private GameObject GetStrongest(bool getWeakestInstead = false)
    {
        float targetHealth = getWeakestInstead ? float.MaxValue : 0;
        GameObject targetGameObject = null;

        foreach (GameObject g in targetsInRange)
        {
            if (g == null) continue;

            if (g.TryGetComponent(out IUnit unit) || (g.transform.parent != null && g.transform.parent.TryGetComponent(out unit)))
            {
                float unitHealth = unit.GetHealth();

                bool shouldUpdateTarget = getWeakestInstead
                    ? unitHealth < targetHealth || targetHealth == float.MaxValue
                    : unitHealth > targetHealth;

                if (shouldUpdateTarget)
                {
                    targetGameObject = g;
                    targetHealth = unitHealth;
                }
            }
        }

        return targetGameObject;
    }


}
