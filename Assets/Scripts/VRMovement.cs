using UnityEngine;

public class VRMovement : MonoBehaviour
{
    public float moveSpeed = 3f; // Speed of player movement
    public Transform playerCamera; // Reference to the player's camera (for forward direction)
    public OVRInput.Controller controller = OVRInput.Controller.LTouch; // Default to Left Controller (can also use RTouch for right)

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>(); // Ensure this object has a CharacterController component
        if (playerCamera == null)
        {
            playerCamera = Camera.main.transform; // Assign the main camera if none is set
        }
    }

    void Update()
    {
        Vector2 thumbstickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controller);

        // Get movement direction based on player's current forward direction
        Vector3 moveDirection = playerCamera.forward * thumbstickInput.y + playerCamera.right * thumbstickInput.x;
        moveDirection.y = 0; // Ignore vertical movement

        // Move the player using the CharacterController
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}
