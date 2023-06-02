using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private NavMeshAgent navMeshAgent;
    private bool canMove;

    private void OnEnable()
    {
        PlayerEvents.Instance.OnSetPlayerMovement += value => canMove = value;
    }

    private void OnDisable()
    {
        PlayerEvents.Instance.OnSetPlayerMovement -= value => canMove = value;
    }


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        canMove = true;
        navMeshAgent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
            return;
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                navMeshAgent.SetDestination(hit.point);
                Debug.Log(hit.point);
            }
        }
    }
}