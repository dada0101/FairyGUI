using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using DG.Tweening;

//摇杆
//事件收发类
public class JoyStick : EventDispatcher
{
    //事件的监听者
    public EventListener onMove { get; private set; }//设置了一个安全权限
    public EventListener onEnd { get; private set; }

    //mainUI里的对象
    private GButton joystickButton;                                                 //摇杆按钮
    private GObject thumb;                                                          //杆子
    private GObject touchArea;                                                      //触摸面积
    private GObject center;                                                         //摇杆中心

    //摇杆的属性
    private float initX;                                                            //初始位置
    private float initY;
    private float startStageX;                                                      //开始触摸位置
    private float startStageY;
    private float lastStageX;                                                       //上一帧触摸位置
    private float lastStageY;
    private int touchID;                                                            //判断当前的触摸状态
    public int Radius { get; set; }                                                 //最大移动范围
    private GTweener tweener;

    public JoyStick(GComponent mainUI)
    {
        //创建监听者
        onMove = new EventListener(this, "onMove");
        onEnd = new EventListener(this, "onEnd");

        joystickButton = mainUI.GetChild("Joystick").asButton;
        //自己控制按钮状态
        joystickButton.changeStateOnClick = false;
        thumb = joystickButton.GetChild("thumb");
        touchArea = mainUI.GetChild("JoystickTouchArea");
        center = mainUI.GetChild("JoystickCenter");
        //设置初始状态
        initX = center.x + center.width / 2;
        initY = center.y + center.height / 2;
        touchID = -1;
        Radius = 150;
        //设置触摸面积的监听方法
        touchArea.onTouchBegin.Add(OnTouchBegin);
        touchArea.onTouchMove.Add(OnTouchMove);
        touchArea.onTouchEnd.Add(OnTouchEnd);

    }

    //开始触摸
    private void OnTouchBegin(EventContext context)
    {
        if (touchID == -1)//第一次触摸
        {
            InputEvent inputEvent = (InputEvent)context.data;
            touchID = inputEvent.touchId;

            if (tweener != null)
            {
                tweener.Kill();//杀死上一个动画
                tweener = null;
            }
            //将世界坐标转化为FGUI坐标
            Vector2 localPos = GRoot.inst.GlobalToLocal(new Vector2(inputEvent.x, inputEvent.y));
            float posX = localPos.x;
            float posY = localPos.y;
            //注意此处为单选按钮，需要设置单选属性 选中设为true
            joystickButton.selected = true;

            lastStageX = posX;
            lastStageY = posY;
            startStageX = posX;
            startStageY = posY;
            //设置新的摇杆按钮与中心位置
            center.visible = true;
            center.SetXY(posX - center.width / 2, posY - center.height / 2);
            joystickButton.SetXY(posX - joystickButton.width / 2, posY - joystickButton.height / 2);

            float deltaX = posX - initX;
            float deltay = posY - initY;
            //计算旋转角度
            float degrees = Mathf.Atan2(deltay, deltaX) * 180 / Mathf.PI;
            thumb.rotation = degrees + 90;
            //上下文获取触摸状态
            context.CaptureTouch();
        }
    }

    //移动触摸
    private void OnTouchMove(EventContext context)
    {
        InputEvent inputEvent = (InputEvent)context.data;
        //触摸中
        if (touchID != -1 && inputEvent.touchId == touchID)
        {
            //计算FGUI中的触摸坐标
            Vector2 localPos = GRoot.inst.GlobalToLocal(new Vector2(inputEvent.x, inputEvent.y));
            float posX = localPos.x;
            float posY = localPos.y;

            lastStageX = posX;
            lastStageY = posY;
            //计算X与Y坐标相对上一帧偏移量
            float deltaX = posX - startStageX;
            float deltaY = posY - startStageY;
            //计算旋转角度
            float rad = Mathf.Atan2(deltaY, deltaX);
            float degree = rad * 180 / Mathf.PI;
            thumb.rotation = degree + 90;

            //设置范围 不要超过最大范围X与Y的值
            float maxX = Radius * Mathf.Cos(rad);
            float maxY = Radius * Mathf.Sin(rad);
            if (Mathf.Abs(deltaX) > Mathf.Abs(maxX))
            {
                deltaX = maxX;
            }
            if (Mathf.Abs(deltaY) > Mathf.Abs(maxY))
            {
                deltaY = maxY;
            }
            //计算按钮所在位置
            float buttonX = startStageX + deltaX;
            float buttonY = startStageY + deltaY;

            joystickButton.SetXY(buttonX - joystickButton.width / 2, buttonY - joystickButton.height / 2);
            //回调函数
            onMove.Call(degree);
        }
    }

    //结束触摸
    private void OnTouchEnd(EventContext context)
    {
        InputEvent inputEvent = (InputEvent)context.data;
        //触摸中
        if (touchID != -1 && inputEvent.touchId == touchID)
        {
            //设置为结束触摸
            touchID = -1;
            //将杆子旋转180度 
            thumb.rotation = thumb.rotation + 180;
            //隐藏摇杆中心
            center.visible = false;
            //启动移动动画 设置目标位置与移动时间
            tweener = joystickButton.TweenMove(new Vector2(initX - joystickButton.width / 2, initY - joystickButton.height / 2), 0.3f).OnComplete(() =>
            {
                //结束的回调方法
                tweener = null;
                joystickButton.selected = false;
                thumb.rotation = 0;
                center.visible = true;
                center.SetXY(initX - center.width / 2, initY - center.height / 2);
            }
                );
        }
        //回调函数
        onEnd.Call();
    }
}
