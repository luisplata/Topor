using System.Collections;
using UnityEngine;

public class ExampleToLog : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool isSendLog = true;
    [SerializeField] private bool isSendLogError = true;
    [SerializeField] private bool isSendLogWarning = true;
    [SerializeField] private float timeToLog = 5f;
    void Start()
    {
        Debug.Log("This is a log message");
        StartCoroutine(SendLogAnyXSeconds());
        StartCoroutine(SendLogErrorAnyXSeconds());
        StartCoroutine(SendLogWarningAnyXSeconds());
    }

    private IEnumerator SendLogWarningAnyXSeconds()
    {
        while (true)
        {
            if (!isSendLogWarning)
            {
                yield return new WaitForSeconds(timeToLog);
            }
            else
            {
                Debug.LogWarning("This is a log warning message every 5 seconds");
                yield return new WaitForSeconds(timeToLog);   
            }
        }
    }

    private IEnumerator SendLogErrorAnyXSeconds()
    {
        while (true)
        {
            if (!isSendLogError)
            {
                yield return new WaitForSeconds(timeToLog);
            }
            else
            {
                Debug.LogError("This is a log error message every 5 seconds");
                yield return new WaitForSeconds(timeToLog);
            }
        }
    }

    private IEnumerator SendLogAnyXSeconds()
    {
        while (true)
        {
            if (!isSendLog)
            {
                yield return new WaitForSeconds(timeToLog);
            }
            else
            {
                Debug.Log("This is a log message every 5 seconds");
                yield return new WaitForSeconds(timeToLog);
            }
        }
    }
}
