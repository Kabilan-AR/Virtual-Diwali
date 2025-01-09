using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworksBox : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject fireworksBox;
    private Trigger trigger;
    private float TimeToStop = 10f;
    float timer = 0f;

    private float soundTimer=1f;
    private AudioSource source;
    [HideInInspector]public bool canLaunch;
    private bool bufferFinish = false;
    private bool isFinished = false;
    void Start()
    {
        trigger=GetComponentInChildren<Trigger>();
        source=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        canLaunch = trigger.isFired;
        if(canLaunch && !bufferFinish)
        {
            timer += Time.deltaTime;
            if(timer>2f)
            {
                timer = 0f;
                bufferFinish = true;
            }
        }
        if(canLaunch && !isFinished && bufferFinish)
        {
            timer += Time.deltaTime;
            soundTimer += Time.deltaTime;
            fireworksBox.SetActive(true);
        }
        if(timer>TimeToStop)
        {
            fireworksBox.SetActive(false);
            isFinished = true;
        }
        if(soundTimer>2f)
        {
            soundTimer = 0f;
            source.Play();
        }
    }
    
}
