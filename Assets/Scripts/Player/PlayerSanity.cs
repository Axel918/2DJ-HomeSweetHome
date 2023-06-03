using UnityEngine;

public class PlayerSanity : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float defaultSanity;

    private float currentSanity;

    void OnEnable()
    {
        PlayerEvents.Instance.OnPlayerDamaged += TakeDamage;
    }

    void OnDisable()
    {
        PlayerEvents.Instance.OnPlayerDamaged -= TakeDamage;
    }

    void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        currentSanity = defaultSanity;
    }

    void TakeDamage(float damage)
    {
        currentSanity -= damage;

        Debug.Log(currentSanity);

        if (currentSanity <= 0f)
            OnDeath();
    }

    void OnDeath()
    {
        Debug.Log("Dead");
    }

    public float GetSanityRatio()
    {
        return currentSanity / defaultSanity;
    }
}