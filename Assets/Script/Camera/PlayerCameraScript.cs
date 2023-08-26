using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCameraScript : MonoBehaviour
{
    [Range(1f, 10f)][SerializeField] float zoomValue = 4;
    [SerializeField] CinemachineVirtualCamera playerCamera;
    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            zoomValue += 0.5f;
            zoomValue = Mathf.Clamp(zoomValue, 1f, 10f);
            playerCamera.m_Lens.OrthographicSize = zoomValue;
        } 
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            zoomValue -= 0.5f;
            zoomValue = Mathf.Clamp(zoomValue, 1f, 10f);
            playerCamera.m_Lens.OrthographicSize = zoomValue;
        }
    }
}
