using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningUI : MonoBehaviour
{
    public Image largeDebrisWarning;
    public Image desertingWarning;

    private void Start()
    {
        DebrisSpawner.instance.largeDebrisSpawned.AddListener(() => { WarnDebris(); });
    }

    void WarnDebris()
    {
        largeDebrisWarning.gameObject.SetActive(true);
        StartCoroutine(DisableAfterTime(largeDebrisWarning.gameObject));
    }

    void WarnDeserting()
    {
        desertingWarning.gameObject.SetActive(true);
    }

    IEnumerator DisableAfterTime(GameObject gameObject)
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
