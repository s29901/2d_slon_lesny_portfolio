using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class InfoTabManager : MonoBehaviour
{
 
    public GameObject duze_info;  
    public GameObject Player; 
    private Transform cam;

    private bool isInfoOpen = false;
    void Start()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (cam != null)
        {
            transform.position = new Vector3(cam.position.x, cam.position.y, 0f);
        }
    }


    // Вызывается, например, при нажатии на кнопку
    public void ToggleInfoTab()
    {
        Debug.Log("TAB TOGGLED!");
        isInfoOpen = !isInfoOpen;

        duze_info.SetActive(isInfoOpen);
        Player.SetActive(!isInfoOpen);
    }
   

}