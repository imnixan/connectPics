using System.Collections;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI counter;

    private float levelTime;
    private WaitForSecondsRealtime waitForSeconds;

    public void StartTime()
    {
        levelTime = 0;
        waitForSeconds = new WaitForSecondsRealtime(1);
        StartCoroutine(CountTime());
    }

    IEnumerator CountTime()
    {
        while (true)
        {
            levelTime++;
            counter.text = string.Format(
                "{0:d2}:{1:d2}",
                (int)(levelTime / 60),
                (int)(levelTime % 60)
            );
            yield return waitForSeconds;
        }
    }

    public float GetFinishTime()
    {
        StopAllCoroutines();
        return levelTime;
    }
}
