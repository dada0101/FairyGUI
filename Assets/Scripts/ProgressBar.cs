using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using System;
//进度条
public class ProgressBar : MonoBehaviour
{
    private GComponent mainUI;                                  //主控件
    private GProgressBar progressBar;                           //进度条
    private GComboBox comboBox;                                 //可选下拉框
    
    // Start is called before the first frame update
    void Start()
    {
        mainUI = GetComponent<UIPanel>().ui;
        progressBar = mainUI.GetChild("n0").asProgress;         
        //设置进度条在5s内到达100
        progressBar.TweenValue(100, 5);
        comboBox = mainUI.GetChild("n7").asComboBox;
        //设置可选下拉框的改变的监听事件
        comboBox.onChanged.Add(SetCompleteTime);
    }
    //设置进度条完成的时间
    private void SetCompleteTime()
    {
        //将进度条归零
        progressBar.value = 0;
        //根据下拉框选择的值决定进度条完成的时间 
        progressBar.TweenValue(100, Convert.ToInt32(comboBox.value));
    }
}
