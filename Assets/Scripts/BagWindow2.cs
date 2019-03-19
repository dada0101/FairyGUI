using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
//背包窗口
public class BagWindow2 : Window
{
    private GList list;                                                         //背包物品列表
    private GButton playerView;                                                 //背包物品显示按钮

    public BagWindow2(GButton targetButton)
    {
        playerView = targetButton;
    }
    //重写父类方法
    protected override void OnInit()
    {
        //设置contentPane
        this.contentPane = UIPackage.CreateObject("Bag2", "BagWindow").asCom;
        list = this.contentPane.GetChild("ItemList").asList;
        //设置列表子项渲染方法
        list.itemRenderer = RenderListItem;
        //设置子项数目
        list.numItems = 20;
        //对每个子项进行监听事件的绑定
        for (int i = 0; i < list.numItems - 10; ++i)
        {
            GButton button = list.GetChildAt(i).asButton;
            button.onClick.Add(() => { ClickItem(button); });
        }
    }
    //列表子项渲染方法
    private void RenderListItem(int index, GObject obj)
    {
        GButton button = obj.asButton;
        //设置子项的icon和title
        button.icon = UIPackage.GetItemURL("Bag2", "i" + index);
        button.title = index.ToString();
    }
    //子项监听方法，将当前子项的值赋予物品显示按钮
    private void ClickItem(GButton button)
    {
        playerView.icon = button.icon;
        playerView.title = button.title;
    }
}
