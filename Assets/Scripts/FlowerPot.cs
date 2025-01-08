using UnityEngine;

public class FlowerPot : MonoBehaviour
{
    public ParticleSystem[] ignitionLevels;
    private int total = 4;
    private int currentLevel = -1;
    private Trigger trigger;
    private bool canStart;
    private bool destroyed = false;

    private float totalLifetime = 10f;
    private float timeForEachLevel = 2f;
    private float timer = 0f;

    private AudioSource source;
    int i;
    void Start()
    {
        trigger = GetComponentInChildren<Trigger>();source=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        canStart = trigger.isFired;
        if (canStart && !destroyed)
        {
            timer += Time.deltaTime;
            launchFountain();
        }
        if(destroyed)
        {
            timer += Time.deltaTime;
            destructionProcess();
        }
    }
    private void launchFountain()
    {
        if (currentLevel == -1)
        {
            source.Play();
            currentLevel = 0;
        }
        if(currentLevel == 0)
        {
            ignitionLevels[currentLevel].gameObject.SetActive(true);
            Debug.Log("Level" + currentLevel);
            ignitionLevels[currentLevel+1].gameObject.SetActive(true);
            Debug.Log("Level" + currentLevel);
            if(timer>timeForEachLevel)
            {
                currentLevel=2;
                Debug.Log("Level" + currentLevel);
                timer = 0f;
            }
        }
        if(currentLevel ==2)
        {
            ignitionLevels[0].startSpeed = 6f;
            ignitionLevels[2].gameObject.SetActive(true);
            if (timer > timeForEachLevel)
            {
                currentLevel++;
                timer = 0f;
            }
        }
        if(currentLevel == 3)
        {
            ignitionLevels[0].startSpeed =9f;
            ignitionLevels[3].gameObject.SetActive(true);
            if (timer > timeForEachLevel)
            {
                destroyed = true;
                i = 3;
                timer = 0f;
            }
        }
        
    }
    private void destructionProcess()
    {
        ignitionLevels[0].startSpeed -= 0.7f;
        if (timer > 0.45f && i>=0)
        {
           
            ignitionLevels[i--].gameObject.SetActive(false);
            
                
            timer = 0f;
        }
        else
        {
            source.Stop();
        }
    }
}
    
