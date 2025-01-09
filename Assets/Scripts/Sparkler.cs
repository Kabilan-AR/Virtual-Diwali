using UnityEngine;

public class Sparkler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject sparklerVFX;
   
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
            gameObject.tag = other.gameObject.tag;
            sparklerVFX.SetActive(true);
  
        }
    }
}
