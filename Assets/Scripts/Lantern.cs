using UnityEngine;

public class Lantern : MonoBehaviour
{
    private float currentHeight;
    private float lerpTime;
    void Start()
    {
        currentHeight = gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        lerpTime += Time.deltaTime;
        if(lerpTime>0.3f)
        {
            float newY = Mathf.Lerp(currentHeight - 3, currentHeight + 3, Mathf.PingPong(lerpTime, 1));
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
       
    }
}
