using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public List<string> FurnitureList = new();

    public int CurrentLevel { get; set; } = 1;

    private void Awake()
    {
        Debug.Log("Data has been Cleared");
        ClearData();
    }

    #region ID Registration Functionc
    public void AddId(string id)
    {
        if (FurnitureList.Contains(id)) 
            return;

        FurnitureList.Add(id);
    }

    public void RemoveId(string id)
    {
        if (!FurnitureList.Contains(id)) 
            return;

        FurnitureList.Remove(id);
    }
    #endregion

    public void ClearData()
    {
        FurnitureList.Clear();
        CurrentLevel = 1;
    }
}