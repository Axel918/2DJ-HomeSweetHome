using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotationSpeed;

    private MeshRenderer meshRenderer;

    private void OnEnable()
    {
        PlayerEvents.Instance.OnPlayerInsane += () => meshRenderer.enabled = true;
        PlayerEvents.Instance.OnPlayerStabilized += () => meshRenderer.enabled = false;
        GameEvents.Instance.OnLevelFailed += () => meshRenderer.enabled = false;
    }

    private void OnDisable()
    {
        PlayerEvents.Instance.OnPlayerInsane -= () => meshRenderer.enabled = true;
        PlayerEvents.Instance.OnPlayerStabilized -= () => meshRenderer.enabled = false;
        GameEvents.Instance.OnLevelFailed -= () => meshRenderer.enabled = false;
    }

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        meshRenderer.enabled = false;
    }


    private void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,
             Quaternion.LookRotation(target.position - transform.position) * Quaternion.Euler(0f, 90f, 0f), rotationSpeed * Time.deltaTime);
    }

}
