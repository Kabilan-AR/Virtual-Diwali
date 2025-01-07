using UnityEngine;

public class Chakra : MonoBehaviour
{
    public ParticleSystem sparkles;
    private ParticleSystemRenderer psRenderer;
    private AudioSource source;
    private int totalIgntionLevel = 3;
    private int currentIgntionLevel = 0;

    private float nextIgnitionLevelTimer = 0f;
    private float TimeForNextLevel = 2.3f;
    private bool reverseToDestroy = false;
    private bool destroyed = false;
    private Rigidbody rb;

    private bool onGround = false;
    private bool ignitionStart = false;
    private Trigger trigger;

    void Start()
    {
        psRenderer = sparkles.GetComponent<ParticleSystemRenderer>();
        trigger = GetComponentInChildren<Trigger>();
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 15f;
        rb.angularDrag = 0.5f;  // Add angular drag to slow down rotation naturally
        sparkles.gameObject.SetActive(false);
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        ignitionStart = trigger.isFired;

        if (ignitionStart)
        {
            sparkles.gameObject.SetActive(true);
        }

        if (!destroyed)
        {
            if (reverseToDestroy)
            {
                source.Stop();
                reverseTheList();
            }
            else if (onGround && ignitionStart)
            {
                source.Play();
                IgnitionForward();
            }
        }
        else
        {
            StopRotationGradually();
        }

        UpdateIgnitionLevel();
    }

    private void IgnitionForward()
    {
        sparkles.gameObject.SetActive(true);
        nextIgnitionLevelTimer += Time.deltaTime;

        if (nextIgnitionLevelTimer > TimeForNextLevel && !reverseToDestroy)
        {
            currentIgntionLevel += 1;
            nextIgnitionLevelTimer = 0f;
        }

        if (currentIgntionLevel >= totalIgntionLevel)
        {
            currentIgntionLevel = totalIgntionLevel - 1;
            reverseToDestroy = true;
        }
    }

    private void reverseTheList()
    {
        nextIgnitionLevelTimer += Time.deltaTime;

        sparkles.gameObject.SetActive(false);

        if (nextIgnitionLevelTimer > TimeForNextLevel)
        {
            nextIgnitionLevelTimer = 0f;
            currentIgntionLevel -= 1;
        }

        if (currentIgntionLevel == 0)
        {
            sparkles.gameObject.SetActive(false);
            destroyed = true;
        }
    }

    private void StopRotationGradually()
    {
        sparkles.gameObject.SetActive(false);
        if (rb.angularVelocity.magnitude > 0.1f && currentIgntionLevel==0)
        {
            rb.angularVelocity *= 0.98f;  // Gradually slow down the rotation
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;
        //    rb.angularVelocity = Vector3.zero;
        //    //rb.constraints = RigidbodyConstraints.FreezeRotation;  // Freeze the rotation once stopped
        }
    }

    private void UpdateIgnitionLevel()
    {
        if (ignitionStart && !reverseToDestroy)
        {
            // Adjust particle system settings based on ignition levels
            if (currentIgntionLevel == 0)
            {
                rb.AddTorque(Vector3.up * 5f);
                psRenderer.velocityScale = 0.05f;
                psRenderer.lengthScale = 1.0f;
            }
            else if (currentIgntionLevel == 1)
            {
                rb.AddTorque(Vector3.up * 10f);  // Increase torque at level 1
                psRenderer.velocityScale = 0.17f;
                psRenderer.lengthScale = 0.87f;
            }
            else if (currentIgntionLevel == 2)
            {
                rb.AddTorque(Vector3.up * 15f);  // Increase torque at level 2
                psRenderer.velocityScale = 0.25f;
                psRenderer.lengthScale = 0.87f;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Terrain")) return;
        onGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        onGround = false;
    }
}
