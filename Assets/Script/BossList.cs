using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossList : MonoBehaviour
{
    public static BossList instance;

    public GameObject[] boss;
    public int bossIdx = 0;

    public Sprite[] bossImages;

    public string[] bossNames = { "Dengert", "locked"};
    

    void Start()
    {
        if (instance != this && instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }
}
