using UnityEngine;
using System;

public class UIEvents : MonoBehaviour
{
    public static UIEvents Instance;

    #region Singleton
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion
}
