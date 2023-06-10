using UnityEngine;

public class SpriteBillboarding : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform spriteTransform;             // Sprite Transform Reference
 
    private Vector3 cameraDirection;                                // Direction of the Main Camera

    void LateUpdate()
    {
        // Get Camera Direction
        cameraDirection = Camera.main.transform.forward;
        cameraDirection.y = 0f;

        // Rotate The Sprite to Face the Camera
        spriteTransform.LookAt(transform.position + cameraDirection);
    }
}
