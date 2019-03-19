using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using DG.Tweening;
//增益Buff效果
public class Effect : MonoBehaviour
{
    private GComponent mainUI;                                          //主控件
    private GComponent addValueCom;                                     //增加数值控件
    private float startValue;                                           //开始数值
    private float endValue;                                             //结束数值

    // Start is called before the first frame update
    void Start()
    {
        //获取各个组件 
        mainUI = GetComponent<UIPanel>().ui;
        addValueCom = UIPackage.CreateObject("Effect", "AddValue").asCom;
        //设置增加数值控件标签的监听事件
        addValueCom.GetTransition("t0").SetHook("AddValue", AddAttackValue);
        //设置控制按钮的监听事件
        mainUI.GetChild("n0").onClick.Add(() => { PlayUI(addValueCom); });
    }

    //控制按钮的监听事件
    private void PlayUI(GComponent targetCom)
    {
        //按下按钮后，设置为不显示
        mainUI.GetChild("n0").visible = false;
        //产生增加数值控件
        GRoot.inst.AddChild(targetCom);
        //设置开始与结束数值
        startValue = 10000;
        int add = Random.Range(1000, 3000);
        endValue = startValue + add;
        //获取对应的动效，对控件中的对应数值进行赋值
        Transition t = targetCom.GetTransition("t0");
        targetCom.GetChild("n2").text = startValue.ToString();
        targetCom.GetChild("n5").text = add.ToString();
        t.Play(() =>
        {
            //播放动效后的回调函数，显示按钮，移除增加数值控件
            mainUI.GetChild("n0").visible = true;
            GRoot.inst.RemoveChild(targetCom);
        }
        );

    }

    //增加数值控件动效标签的监听事件
    private void AddAttackValue()
    {
        //调用DOTween插件的方法 
        DOTween.To(() => startValue, x => { addValueCom.GetChild("n2").text = Mathf.Floor(x).ToString(); }, endValue, 1.0f);
    }
}
