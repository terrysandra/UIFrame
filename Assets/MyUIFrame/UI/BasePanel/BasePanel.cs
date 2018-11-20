using UnityEngine;
using System.Collections;
using System;

public class BasePanel : MonoBehaviour
{

    CanvasGroup canvasGroup;
    Action CloseCallBack;
    private void Awake()
    {
        UIManager.Instance.AddDicPanle(name, this);
    }
    /// <summary>
    /// 界面被显示出来
    /// </summary>
    public virtual void OnEnter(object value, Action PanelExitCallBack)
    {
        if (!canvasGroup) canvasGroup = GetComponent<CanvasGroup>();
        CloseCallBack = PanelExitCallBack;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    /// <summary>
    /// 界面暂停
    /// </summary>
    public virtual void OnPause()
    {
        canvasGroup.interactable = false;
    }

    /// <summary>
    /// 界面继续
    /// </summary>
    public virtual void OnResume()
    {
        canvasGroup.interactable = true;
    }

    /// <summary>
    /// 界面不显示,退出这个界面，界面被关系
    /// </summary>
    public virtual void OnExit()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        CloseUICallBack();
    }

    /// <summary>
    /// 页面关掉后得回调
    /// </summary>
    private void CloseUICallBack()
    {
        if (CloseCallBack != null)
            CloseCallBack();
    }
}
