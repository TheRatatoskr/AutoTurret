using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneCounter : MonoBehaviour
{
    [SerializeField] private int numberToCount = 15;
    [SerializeField] private GameObject enterThePlayerTimeline;
    private int currentCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        currentCount++;
        Debug.Log("Counted");
        if (currentCount >= numberToCount)
        {
            enterThePlayerTimeline.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
