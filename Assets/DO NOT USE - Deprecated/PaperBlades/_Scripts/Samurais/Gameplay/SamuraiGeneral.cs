using System;
using UnityEngine;

public class SamuraiGeneral : Samurai
{
    public static Action<Samurai> OnDeath;
    [SerializeField] LayerMask m_Mask;
    private Camera mainCamera;

    public SoundOld attack;
    public SoundOld special;
    public SoundOld death;

    private void Awake()
    {

    }

    float cursorChangeCooldown = 0.2f;
    float cursorChangeTimer = 0f;
    private void Update()
    {
        cursorChangeTimer += Time.deltaTime;
        if (cursorChangeTimer >= cursorChangeCooldown)
        {
            TryToChangeCursor();
            cursorChangeTimer = 0f; // Reset the timer
        }


        if (Input.GetMouseButtonDown(0))
        {
            CastRayAndAttack();
        }
        if (Input.GetMouseButtonDown(1))
        {
            AudioManagerOld.Instance.PlayAtPosition(this.transform.position, special);
            UseSkill();
        }
    }

    private void CastRayAndAttack()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, 1500f, m_Mask);

        foreach (RaycastHit hit in hits)
        {
            IUnit unit = hit.collider.GetComponent<IUnit>();
            if (unit != null && !unit.IsAlly)
            {
                //is not stunned, 
                AudioManagerOld.Instance.PlayAtPosition(this.transform.position, attack);
                AttackTarget(unit);
                CursorManager.Instance.SetCooldownOnCursor(Character.Weapon.itemData.Cooldown.cooldowTime);
                CursorManager.Instance.SetCursorState(CursorState.Default);
                return;
            }
        }
    }

    private void TryToChangeCursor()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, 1500f, m_Mask);

        foreach (RaycastHit hit in hits)
        {
            IUnit unit = hit.collider.GetComponent<IUnit>();
            if (unit != null && !unit.IsAlly)
            {
                if (Vector3.Distance(AttackPoint.transform.position, unit.gameObject.transform.position) <= Character.Weapon.itemData.Range)
                {
                    if (!false) // if is not stunned && can attack again
                    {
                        if (Character.Weapon.itemData.Cooldown.IsOffCooldown())
                        {
                            CursorManager.Instance.SetCursorState(CursorState.Attack);
                        }
                        return;
                    }
                }
            }
        }
        CursorManager.Instance.SetCursorState(CursorState.Default);
    }

    protected override void OnSamuraiDeath()
    {
        //AudioManager.Instance.PlayAtPosition(this.transform.position, death);
        OnDeath?.Invoke(this);
    }
}
