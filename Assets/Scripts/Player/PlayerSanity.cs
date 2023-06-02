using UnityEngine;

public class PlayerSanity : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float defaultSanity;

    private float currentSanity;

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

        if (currentSanity <= 0f)
            OnDeath();
    }

    void OnDeath()
    {
        Debug.Log("Dead");
    }
}