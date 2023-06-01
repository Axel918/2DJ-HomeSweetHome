using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSanity : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float defaultSanity;

    private float currentSanity;

    void Awake()
    {
        currentSanity = defaultSanity;
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
