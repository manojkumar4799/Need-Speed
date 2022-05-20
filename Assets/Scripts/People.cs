using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class People : MonoBehaviour
{
    public TextMeshProUGUI timeTracker;
    public float timer=20f;
    bool startTimer=false;
    public CarController carController;
    public bool EnteredCar = false;
   
    public void SetTimer()
    {
        startTimer = true;
    }
    private void Update()
    {
        if (startTimer&&timer>0)
        {
           StartCoroutine( Timer());
        }
        
        
    }

    IEnumerator Timer()
    {
        startTimer = false;
        yield return new WaitForSeconds(1);
        timer--;
        timeTracker.text = "Time left:00:" + timer;
        if (timer <= 0)
        {
            StartCoroutine(carController.CarBlast());
            SoundManager.Instance().PlayBGM(Sound.Gameover);
        }
        startTimer = true;
    }

    private void OnDestroy()
    {
        timeTracker.text = "Find people";
    }


    
}
    
    
   
