/*
 * 2018-9-24
 * 刘佳编写v1.0.0版本。适用于单层Panle，一层一层打开，一层一层关闭。
 * 避免界面混乱
 * 
 * v1.1.0
 * 增加了获取自定义类型的扩展
 * 
 * v1.2
 * 修改了传入参数。改为object类型
 */


using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }
    private Stack<BasePanel> panelStack;
    Dictionary<string, BasePanel> dicPanel;
    public void AddDicPanle(string key, BasePanel value)
    {
        if (dicPanel == null) dicPanel = new Dictionary<string, BasePanel>();
        if (dicPanel.ContainsKey(key))
        {
            Debug.Log("Panle重复：" + key);
            return;
        }
        dicPanel.Add(key, value);
    }
    public BasePanel GetPanle(string key)
    {
        if (dicPanel.ContainsKey(key))
            return dicPanel[key];
        else
        {
            Debug.LogError("输入Key错误");
            return null;
        }
    }
    /// <summary>
    /// 把某个页面入栈，  把某个页面显示在界面上
    /// </summary>
    public void PushPanel(string panelName, object arg = null, Action CallBack = null)
    {
        if (panelStack == null)
            panelStack = new Stack<BasePanel>();

        //判断一下栈里面是否有页面
        if (panelStack.Count > 0)
        {
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }
        dicPanel[panelName].OnEnter(arg, CallBack);
        panelStack.Push(dicPanel[panelName]);
    }
    /// <summary>
    /// 出栈 ，把页面从界面上移除
    /// </summary>
    public void PopPanel()
    {
        if (panelStack == null)
            panelStack = new Stack<BasePanel>();

        if (panelStack.Count <= 0) return;

        //关闭栈顶页面的显示
        BasePanel topPanel = panelStack.Pop();
        topPanel.OnExit();

        if (panelStack.Count <= 0) return;
        BasePanel topPanel2 = panelStack.Peek();
        topPanel2.OnResume();

    }

    public void CloseAllPanle()
    {
        while (panelStack.Count > 0)
        {
            PopPanel();
        }
    }

    public T GetClass<T>(string panelName)
    {
        return dicPanel[panelName].GetComponent<T>();
    }

}