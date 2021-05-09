using System;
using KKSFramework;
using KKSFramework.DataBind;
using KKSFramework.DataBind.Methods;
using UnityEngine;

public class SomePage : MonoBehaviour, IResolveTarget
{
    #region Fields & Property

#pragma warning disable CS0649

    [Resolver]
    private MethodDelegates _methods;

    [Resolver]
    private MethodDelegates<int> _methodBs;
    
    [Resolver]
    private MethodDelegates<int, int> _methodCs;

    [Resolver]
    private FuncDelegates<int> _funcs;
    
    [Resolver]
    private FuncDelegates<int, int> _funcBs;

    [Resolver]
    private FuncDelegates<string, SomeData> _funcSome;
    
    [Resolver]
    private Action _method;
    
    [Resolver]
    private Action<int> _methodB;

    [Resolver]
    private Action<int, int> _methodC;

    [Resolver]
    private Func<int> _func;

    [Resolver]
    private Func<int, int> _funcBb;

#pragma warning restore CS0649

    #endregion


    #region UnityMethods

    private void Start ()
    {
        _methods.Invoke ();
        _methodBs.Invoke(12);
        _methodBs.Invoke (new [] { 0, 1, 2, 3 });
        _methodCs.Invoke (new [] { 0, 1, 2, 3 }, new [] { 0, 1, 2, 3 });
        
        _funcs.Invoke ().Foreach (x => Debug.Log (x));
        _funcBs.Invoke(new []{0, 1, 2, 3}).Foreach (x => Debug.Log (x));
        _funcSome.Invoke(new []{ "very", "good", "nice", "zzang" }).Foreach (x => Debug.Log (x.Good));
        
        _method.Invoke ();
        _methodB.Invoke (1);
        _methodC.Invoke (1, 2);
        
        Debug.Log (_func.Invoke());
        Debug.Log (_funcBb.Invoke (1));
    }

    #endregion


    #region Methods


    #endregion


    #region EventMethods

    #endregion
}