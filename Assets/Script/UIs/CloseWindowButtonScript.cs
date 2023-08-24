using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWindowButtonScript : MonoBehaviour
{
    [SerializeField] GameObject window;
    [SerializeField]
    public void CloseWindow()
    {
        window.SetActive(false);
        Time.timeScale = 1; //in case some window stop time or slow time
    }
}
