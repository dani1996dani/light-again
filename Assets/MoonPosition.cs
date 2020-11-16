using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonPosition : MonoBehaviour
{
    void Update()
    {
        Vector3 moonWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.085f, Screen.height * 0.85f, 5));
        transform.position = moonWorldPoint;
    }
}
