using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternMiniGame : MonoBehaviour
{
    public static PatternMiniGame Instance;

    [SerializeField] Transform patternHolder;

    private GameObject[] currentPatternData;
    private PatternTrigger patternTrigger;
    private int currentPatternIndex;

    private List<GameObject> patterns = new();

    public bool CanDraw { get; private set; }

    #region Singleton
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        CanDraw = true;
    }

    public void Initialize(GameObject[] data, PatternTrigger reference)
    {
        CanDraw = true;
        currentPatternData = data;
        patternTrigger = reference;
        currentPatternIndex = 0;

        for (int i = 0; i < currentPatternData.Length; i++)
        {
           RectTransform go = Instantiate(data[i], Vector3.zero, Quaternion.identity, patternHolder.transform).GetComponent<RectTransform>();
           go.anchoredPosition3D = Vector3.zero;

           patterns.Add(go.gameObject);
        }

        ActivatePattern();
    }

    public void NextPattern()
    {
        currentPatternIndex++;

        if (currentPatternIndex >= currentPatternData.Length)
        {
            Debug.Log("FINISHED");
            return;
        }

        ActivatePattern();
    }

    void ActivatePattern()
    {
        for (int i = 0; i < patterns.Count; i++)
        {
            patterns[i].SetActive(i == currentPatternIndex);
        }
    }
}
