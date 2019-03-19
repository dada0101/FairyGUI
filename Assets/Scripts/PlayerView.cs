using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
//玩家状态显示
public class PlayerView : MonoBehaviour
{
    private GComponent mainUI;
    private PlayerWindow playerWindow;                                  //玩家显示窗口
    public GameObject player;                                           //需要显示的玩家
    // Start is called before the first frame update
    void Start()
    {
        mainUI = GetComponent<UIPanel>().ui;
        playerWindow = new PlayerWindow(player);
        //对玩家头像按钮添加监听事件，显示玩家状态
        mainUI.GetChild("n0").onClick.Add(() => { playerWindow.Show(); });
        
    }   
}
