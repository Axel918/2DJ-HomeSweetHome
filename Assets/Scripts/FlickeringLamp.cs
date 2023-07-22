using System.Collections;
using UnityEngine;

public class FlickeringLamp : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float minTriggerTime = 1f;                                 // Minimum Trigger Time
    [SerializeField] private float maxTriggerTime = 2f;                                 // Maximum Trigger Time
    
    private bool isFlickering = true;                                                   // Indicates if Lamp is Flickering
    private Animator animator;                                                          // Animator Component Reference
    
    void Awake()
    {
        animator = GetComponent<Animator>();    
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (isFlickering)
        {
            float randomTriggerTime = Random.Range(minTriggerTime, maxTriggerTime);
            
            yield return new WaitForSeconds(randomTriggerTime);

            animator.SetTrigger("isFlickering1");
        }
    }
}
