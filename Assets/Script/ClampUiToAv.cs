using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ClampUiToAv : MonoBehaviour
{
    public GameObject uiGauge;

    // Update is called once per frame
    void Update()
    {
        Vector3 uiPos = Camera.main.WorldToScreenPoint(transform.position);
        uiGauge.transform.position = uiPos;
    }
}
