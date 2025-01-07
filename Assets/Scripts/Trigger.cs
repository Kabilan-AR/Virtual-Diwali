using UnityEngine;

public class Trigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [HideInInspector] public bool isFired;
    private float waitTimeToBurnTrigger = 2f;
    private float timer = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Fire"))
        {
            isFired = true;
        }
    }
}
