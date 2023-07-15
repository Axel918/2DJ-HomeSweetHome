using System.Collections;
using UnityEngine;

public class IntroductionCutscene : MonoBehaviour
{
    [SerializeField] private GameObject mirrorCamera;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        mirrorCamera.SetActive(true);
        PlayerEvents.Instance.SetPlayerEnable(false);
        
        yield return new WaitForSeconds(3f);

        mirrorCamera.SetActive(false);
        PlayerEvents.Instance.SetPlayerEnable(true);
    }
}
