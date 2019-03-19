using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

//摇杆主要控制
public class JoyStickMain : MonoBehaviour
{
    private GComponent mainUI;
    private JoyStick joyStick;                                      //摇杆
    private GTextField gTextField;                                  //角度文本

    // Start is called before the first frame update
    void Start()
    {
        mainUI = GetComponent<UIPanel>().ui;
        joyStick = new JoyStick(mainUI);
        gTextField = mainUI.GetChild("n4").asTextField;
        //为摇杆移动和结束添加事件
        joyStick.onMove.Add(JoyStickMove);
        joyStick.onEnd.Add(JoyStickEnd);
    }

    //摇杆移动事件 显示旋转的角度
    private void JoyStickMove(EventContext context)
    {
        float degree = (float)context.data + 90;
        gTextField.text = degree.ToString();
    }
    //摇杆结束事件 将显示文本置空
    private void JoyStickEnd()
    {
        gTextField.text = "";
    }
}
