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

    #region ID Registration Functions
    /// <summary>
    /// Adds ID Furniture to the List
    /// </summary>
    /// <param name="id"></param>
    public void AddId(string id)
    {
        // Check if Registered ID is not a Duplicate
        if (FurnitureList.Contains(id)) 
            return;

        FurnitureList.Add(id);
    }

    /// <summary>
    /// Removed ID Furniture from the List
    /// </summary>
    /// <param name="id"></param>
    public void RemoveId(string id)
    {
        // Check if Registered ID is not a Duplicate
        if (!FurnitureList.Contains(id)) 
            return;

        FurnitureList.Remove(id);
    }
    #endregion

    /// <summary>
    /// Clears all Game Data for a Fresh Start
    /// </summary>
    public void ClearData()
    {
        FurnitureList.Clear();
        CurrentLevel = 1;
    }
}