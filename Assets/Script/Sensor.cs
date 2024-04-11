using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public SpriteRenderer sr;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sr.color = new Color32(255, 100, 100, 255);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sr.color = new Color32(255, 255, 255, 255);
    }
}
