using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
//循环列表
public class LoopList : MonoBehaviour
{
    private GComponent mainUI;                                              //主控件
    private GList list;                                                     //显示列表

    // Start is called before the first frame update
    void Start()
    {
        mainUI = GetComponent<UIPanel>().ui;
        list = mainUI.GetChild("n0").asList;
        list.SetVirtualAndLoop();                                           //必须设置为虚拟列表
        list.itemRenderer = RenderListItem;                                 //设置项的渲染函数
        list.numItems = 5;                                                  //设置项的数目
        list.scrollPane.onScroll.Add(DoSpecialEffect);                      //添加滚动列表事件
        DoSpecialEffect();                                                  //初始时首先调用一次事件
    }

    //产生滚动特效事件
    private void DoSpecialEffect()
    {
        //获取列表中中心位置
        float listCenter = list.scrollPane.posX + list.viewWidth / 2;
        //遍历每个已经渲染出来的子项
        //如果子项中心与列表中心的距离小于子项宽度的一半，就增大这个子项
        for (int i = 0; i < list.numChildren; ++i)
        {
            GObject item = list.GetChildAt(i);
            float HalfOfItemWidth = item.width / 2;
            float itemCenter = item.x + HalfOfItemWidth;
            //获取子项中心与列表中心的距离
            float distance = Mathf.Abs(listCenter - itemCenter);
            if (distance < HalfOfItemWidth)
            {
                //增大的时候注意，需要根据距离来缓慢增大，会有好的体验
                float distanceRange = 1 + (1 - distance / HalfOfItemWidth) * 0.2f;
                //改变缩放
                item.SetScale(distanceRange, distanceRange);
            }
            else
                item.SetScale(1, 1);
        }
    }
    //渲染每个子项的函数
    private void RenderListItem(int index, GObject obj)
    {
        GButton button = obj.asButton;
        //设置轴心
        button.SetPivot(0.5f, 0.5f);
        //设置图标
        button.icon = UIPackage.GetItemURL("LoopList2", "n" + (index + 1));
    }
}
