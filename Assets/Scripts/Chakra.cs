
using UnityEngine;

public class Chakra : MonoBehaviour
{
    private Rigidbody rb;

    private bool onGround = false;
    private bool ignitionStart = false;
    private Trigger trigger;
    void Start()
    {
        trigger = GetComponentInChildren<Trigger>();
     rb=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if(onGround)
        {
            Debug.Log("Starts spinning");
            rb.AddTorque(new Vector3(0, 0.07f, 0));
            
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
