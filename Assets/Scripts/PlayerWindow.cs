using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
//玩家状态显示窗口
public class PlayerWindow : Window
{
    private GameObject player;                                                                  //需要显示的玩家

    public PlayerWindow(GameObject player)
    {
        this.player = player;
    }

    protected override void OnInit()
    {
        this.contentPane = UIPackage.CreateObject("PlayerView", "playerWindow").asCom;
        //获取需要显示的图形
        GGraph holder = contentPane.GetChild("n2").asGraph;
        //获取渲染好的纹理
        RenderTexture renderTexture = Resources.Load<RenderTexture>("FGUI/PlayerView/PlayerRT");
        //获取做好的材质
        Material material = Resources.Load<Material>("FGUI/PlayerView/PlayerMAT");
        //新建图片，将纹理与材质赋图片
        Image img = new Image();
        img.texture = new NTexture(renderTexture);
        img.material = material;
        //将图片赋予图形
        holder.SetNativeObject(img);
        //给左右旋转按钮添加监听事件
        this.contentPane.GetChild("n3").onClick.Add(RotateLeft);
        this.contentPane.GetChild("n4").onClick.Add(RotateRight);
    }
    //左边旋转按钮事件
    private void RotateLeft()
    {
        player.transform.Rotate(Vector3.up * 30, Space.World);
    }
    //右边旋转按钮事件
    private void RotateRight()
    {
        player.transform.Rotate(-Vector3.up * 30, Space.World);
    }
}
