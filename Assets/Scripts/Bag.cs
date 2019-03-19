using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class Bag : MonoBehaviour
{
    private GComponent mainUI;                                                      //主要组件
    private GButton playerViewer;                                                   //物品显示按钮
    private BagWindow2 bagWindow;                                                   //背包窗口
    
    // Start is called before the first frame update
    void Start()
    {
        mainUI = GetComponent<UIPanel>().ui;
        playerViewer = mainUI.GetChild("playerView").asButton;                      
        playerViewer.onClick.Add(UseItem);                                          //为物品显示按钮添加监听事件
        bagWindow = new BagWindow2(playerViewer);
        bagWindow.SetXY(121, 63);
        mainUI.GetChild("bagButton").onClick.Add(() => { bagWindow.Show(); });      //为背包按钮添加监听事件，使背包窗口显示出来
    }
    //使用背包物品方法
    private void UseItem()
    {
        //将按钮清空
        playerViewer.icon = null;
        playerViewer.title = "空白";
    }
}
