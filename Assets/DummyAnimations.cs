using System.Collections.Generic;
using UnityEngine;

public class DummyAnimations : MonoBehaviour
{
    Animator animator;
    CharacterController cc;

    public float speed = 5f;
    public float height = 2.5f;
    public float length = 25f;
    public float shootingSpeedModifier = 0.75f;
    bool dead = false;
    bool isShooting = false;

    public Transform source;
    LineRenderer lineRenderer;

    public List<Sound> sounds;
    public AudioManager audioManager;
    public AudioSource audioSource;
    public AudioSource audioSourceWalk;
    private double nextStartTime;

    private void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        lineRenderer = GetComponent<LineRenderer>();
        nextStartTime = AudioSettings.dspTime + audioSource.clip.length;
    }

    private void Update()
    {
        if (dead) return;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        isShooting = Input.GetKey(KeyCode.T);
        animator.SetBool("IsShooting", isShooting);


        Vector3 targetPosition = (transform.position + transform.rotation * Vector3.forward * length + Vector3.up * height).normalized * length;

        if (isShooting)
        {
            if (AudioSettings.dspTime == nextStartTime)
            {
                audioSource.PlayScheduled(nextStartTime);
                nextStartTime += audioSource.clip.length;
            }

            if (!audioSource.isPlaying)
                { audioSource.Play(); }
            RaycastHit hit;
            if (Physics.Raycast(source.transform.position, (transform.position + transform.rotation * Vector3.forward * length + Vector3.up * height).normalized, out hit, length))
            {
                targetPosition = hit.point;
            }
        }
        else
        {
            audioSource.Stop();
        }


        lineRenderer.enabled = isShooting;
        lineRenderer.SetPosition(0, source.transform.position);
        lineRenderer.SetPosition(1, targetPosition);

        if (Mathf.Abs(v) + Mathf.Abs(h) > 0.01f)
        {
            transform.LookAt(transform.position + new Vector3(h, 0, v));
        }

        cc.Move(new Vector3(h, 0, v) * Time.deltaTime * speed * (isShooting ? shootingSpeedModifier : 1));

        animator.SetBool("IsMoving", (Mathf.Abs(v) + Mathf.Abs(h) > 0.01f));

        if(Mathf.Abs(v) + Mathf.Abs(h) > 0.01f)
        {
            if(!audioSourceWalk.isPlaying)
                audioSourceWalk.Play();
        }
        else
            audioSourceWalk.Stop();


        if (Input.GetKey(KeyCode.Y))
        {
            audioManager.Play(sounds[1]);
            dead = true;
            animator.SetBool("IsDead", true);
        }
        
    }
}
