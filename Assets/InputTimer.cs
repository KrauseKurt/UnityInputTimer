using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InputTimer : MonoBehaviour
{
    public Image kbdButton;
    public TMP_Text txtDownTime;
    public TMP_Text txtUpTime;
    public TMP_Text txtDownAvg;
    public TMP_Text txtDownMax;
    public TMP_Text txtDownMin;
    public TMP_Text txtUpAvg;
    public TMP_Text txtUpMax;
    public TMP_Text txtUpMin;
    
    public List<float> downTimings = new List<float>();
    public List<float> upTimings = new List<float>();

    private float keyDownTime = 0f;
    private float keyUpTime = 0f;
    private float downAvg;
    private float downMax;
    private float downMin;
    private float upAvg;
    private float upMax;
    private float upMin;
    
    private bool holding;
    private bool started;

    private void Start()
    {
        kbdButton.color = Color.white;
    }

    private void Update()
    {
        if (started && !holding)
        {
            keyUpTime = keyUpTime + Time.deltaTime;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            kbdButton.color = Color.green;
            if(started)
                RegisterUpTime();
            TimeDown();
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            TimeDown();
        }
        
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            kbdButton.color = Color.white;
            TimeUp();
        }
        
        UpdateUI();
    }

    private void TimeDown()
    {
        if (!holding)
        {
            ResetTimings();
            holding = true;

            if (!started) started = true;

        }
        else
        {
            keyDownTime = keyDownTime + Time.deltaTime;
        }
    }
    
    private void TimeUp()
    {
        if (holding)
        {
            holding = false;
        }
        
        downTimings.Add(keyDownTime);
        downAvg = CalculateAvg(downTimings);
        downMin = CalculateMin(downTimings);
        downMax = CalculateMax(downTimings);

        ResetTimings();
    }

    private void UpdateUI()
    {
        txtDownTime.text = "Down: " + keyDownTime;
        txtUpTime.text = "Up: " + keyUpTime;
        txtDownAvg.text = "Down Avg: " + downAvg;
        txtDownMax.text = "Down Max: " + downMax;
        txtDownMin.text = "Down Min: " + downMin;
        txtUpAvg.text = "Up Avg: " + upAvg;
        txtUpMax.text = "Up Max: " + upMax;
        txtUpMin.text = "Up Min: " + upMin;
    }

    private void RegisterUpTime()
    {
        upTimings.Add(keyUpTime);
        upAvg = CalculateAvg(upTimings);
        upMax = CalculateMax(upTimings);
        upMin = CalculateMin(upTimings);
    }

    private void ResetTimings()
    {
        keyDownTime = 0f;
        keyUpTime = 0f;
    }

    private float CalculateAvg(List<float> newList)
    {
        float result = 0f;
        int lenght = newList.Count;
        if (lenght == 0) return result;
        float sum = 0f;

        foreach (var time in newList)
        {
            sum = sum + time;
        }

        result = sum / lenght;
        return result;
    }

    private float CalculateMin(List<float> newList)
    {
        float result = 0f;

        result = newList.Min();
        return result;
    }
    
    private float CalculateMax(List<float> newList)
    {
        float result = 0f;

        result = newList.Max();
        return result;
    }
}
