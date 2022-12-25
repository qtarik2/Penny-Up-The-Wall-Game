using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogText : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(TurnOfObject());
    }
    private IEnumerator TurnOfObject()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
