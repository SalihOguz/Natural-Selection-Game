using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject SpeedupButton;

    public void Speedup()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 5;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
