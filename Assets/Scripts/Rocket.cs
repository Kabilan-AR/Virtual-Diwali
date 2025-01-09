using UnityEngine;

public class Rocket : MonoBehaviour
{
    public ParticleSystem ExplosionVFX;
    public ParticleSystem TrailVFX;

    private Trigger trigger;
    private bool canLaunch;
    private bool launched = false;
    private bool destroyed = false;
    public float flyTime = 5f;
    private float timer = 0f;

    [SerializeField] private AudioClip launchSFX;
    [SerializeField] private AudioClip ExplosionSFX;
    private AudioSource source;

    private MeshRenderer renderer;
    private Rigidbody rb;
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        source=GetComponent<AudioSource>();
        trigger = GetComponentInChildren<Trigger>();
        renderer=GetComponentInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        canLaunch = trigger.isFired;
        if (canLaunch && !launched)
        {
            LaunchCountdown();
        }
        if(launched && !destroyed)
        {
            timer += Time.deltaTime;
            if(timer>flyTime)
            {
                rb.velocity = Vector3.zero;
                TrailVFX.gameObject.SetActive(false);
                explosionSequence();
            }
            else if(timer > 2f)
            {
                flyMechanism();
            }
        }
    }
    private void LaunchCountdown()
    {
        launched = true;
        TrailVFX.gameObject.SetActive(true);
        TrailVFX.startSpeed= timer*1.5f;
        if(!source.isPlaying)
        {
            source.clip = launchSFX;
            source.Play();
        }
        
    }
    private void flyMechanism()
    {
        rb.AddForce(Vector3.up * 5f, ForceMode.Acceleration);
        
    }
    private void explosionSequence()
    {
        renderer.enabled=false;
        source.clip = ExplosionSFX;
        source.Play();
        ExplosionVFX.gameObject.SetActive(true);
        ExplosionVFX.Play();
        destroyed = true;
    }
}
