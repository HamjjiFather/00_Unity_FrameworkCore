using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace KKSFramework.DataBind.Methods
{
    public class MethodDelegates
    {
        protected readonly object[] Methods;

        
        public virtual Action this [int index] => (Action) Methods[index];
        

        public void Invoke ()
        {
            if (!Methods.Any ())
            {
                Debug.Log ("There is no bound methods");
                return;
            }
            
            Methods.Foreach (x => ((Action) x).Invoke ());
        }


        public MethodDelegates (params object[] methods)
        {
            Methods = methods;
        }
    }


    public class MethodDelegates<T> : MethodDelegates
    {
        public MethodDelegates (params object[] methods) : base (methods)
        {
        }
        
        
        public new Action<T> this [int index] => (Action<T>) Methods[index];


        public void Invoke (T value)
        {
            Methods.Foreach (x => ((Action<T>) x).Invoke (value));
        }


        public void Invoke (IReadOnlyList<T> value)
        {
            Methods.Foreach ((x, i) => ((Action<T>) x).Invoke (value[i]));
        }
    }


    public class MethodDelegates<T, T1> : MethodDelegates
    {
        public MethodDelegates (params object[] methods) : base (methods)
        {
        }
        
        
        public new Action<T, T1> this [int index] => (Action<T, T1>) Methods[index];


        public void Invoke (T value1, T1 value2)
        {
            Methods.Foreach (x => ((Action<T, T1>) x).Invoke (value1, value2));
        }


        public void Invoke (IReadOnlyList<T> value1, IReadOnlyList<T1> value2)
        {
            Methods.Foreach ((x, i) => ((Action<T, T1>) x).Invoke (value1[i], value2[i]));
        }
    }


    public class MethodDelegates<T, T1, T2> : MethodDelegates
    {
        public MethodDelegates (params object[] methods) : base (methods)
        {
        }

        
        public new Action<T, T1, T2> this [int index] => (Action<T, T1, T2>) Methods[index];
        

        public void Invoke (T value1, T1 value2, T2 value3)
        {
            Methods.Foreach (x => ((Action<T, T1, T2>) x).Invoke (value1, value2, value3));
        }
        

        public void Invoke (IReadOnlyList<T> value1, IReadOnlyList<T1> value2, IReadOnlyList<T2> value3)
        {
            Methods.Foreach ((x, i) => ((Action<T, T1, T2>) x).Invoke (value1[i], value2[i], value3[i]));
        }
    }


    public class MethodDelegates<T, T1, T2, T3> : MethodDelegates
    {
        public MethodDelegates (params object[] methods) : base (methods)
        {
        }
        
        
        public new Action<T, T1, T2, T3> this [int index] => (Action<T, T1, T2, T3>) Methods[index];


        public void Invoke (T value1, T1 value2, T2 value3, T3 value4)
        {
            Methods.Foreach (x => ((Action<T, T1, T2, T3>) x).Invoke (value1, value2, value3, value4));
        }


        public void Invoke (IReadOnlyList<T> value1, IReadOnlyList<T1> value2, IReadOnlyList<T2> value3,
            IReadOnlyList<T3> value4)
        {
            Methods.Foreach ((x, i) =>
                ((Action<T, T1, T2, T3>) x).Invoke (value1[i], value2[i], value3[i], value4[i]));
        }
    }


    public class MethodDelegates<T, T1, T2, T3, T4> : MethodDelegates
    {
        public MethodDelegates (params object[] methods) : base (methods)
        {
        }

        
        public new Action<T, T1, T2, T3, T4> this [int index] => (Action<T, T1, T2, T3, T4>) Methods[index];
        

        public void Invoke (T value1, T1 value2, T2 value3, T3 value4, T4 values5)
        {
            Methods.Foreach (x =>
                ((Action<T, T1, T2, T3, T4>) x).Invoke (value1, value2, value3, value4, values5));
        }


        public void Invoke (IReadOnlyList<T> value1, IReadOnlyList<T1> value2, IReadOnlyList<T2> value3,
            IReadOnlyList<T3> value4, IReadOnlyList<T4> values5)
        {
            Methods.Foreach ((x, i) =>
                ((Action<T, T1, T2, T3, T4>) x).Invoke (value1[i], value2[i], value3[i], value4[i],
                    values5[i]));
        }
    }


    public class FuncDelegates<T>
    {
        protected readonly object[] Methods;
        
        
        public new Func<T> this [int index] => (Func<T>) Methods[index];


        public IEnumerable<T> Invoke ()
        {
            return Methods.Select (x => ((Func<T>) x).Invoke ());
        }
        

        public FuncDelegates (params object[] methods)
        {
            Methods = methods;
        }
    }


    public class FuncDelegates<T, T1> : FuncDelegates<T>
    {
        public FuncDelegates (params object[] methods) : base (methods)
        {
        }
        
        
        public new Func<T, T1> this [int index] => (Func<T, T1>) Methods[index];


        public IEnumerable<T1> Invoke (T value)
        {
            return Methods.Select (x => ((Func<T, T1>) x).Invoke (value));
        }


        public IEnumerable<T1> Invoke (IReadOnlyList<T> value)
        {
            return Methods.Select ((x, i) => ((Func<T, T1>) x).Invoke (value[i]));
        }
    }


    public class FuncDelegates<T, T1, T2> : FuncDelegates<T>
    {
        public FuncDelegates (params object[] methods) : base (methods)
        {
        }
        
        
        public new Func<T, T1, T2> this [int index] => (Func<T, T1, T2>) Methods[index];


        public IEnumerable<T2> Invoke (T value1, T1 value2)
        {
            return Methods.Select (x => ((Func<T, T1, T2>) x).Invoke (value1, value2));
        }
        

        public IEnumerable<T2> Invoke (IReadOnlyList<T> value1, IReadOnlyList<T1> value2)
        {
            return Methods.Select ((x, i) => ((Func<T, T1, T2>) x).Invoke (value1[i], value2[i]));
        }
    }


    public class FuncDelegates<T, T1, T2, T3> : FuncDelegates<T>
    {
        public FuncDelegates (params object[] methods) : base (methods)
        {
        }
        
        
        public new Func<T, T1, T2, T3> this [int index] => (Func<T, T1, T2, T3>) Methods[index];


        public IEnumerable<T3> Invoke (T value1, T1 value2, T2 value3)
        {
            return Methods.Select (x => ((Func<T, T1, T2, T3>) x).Invoke (value1, value2, value3));
        }


        public IEnumerable<T3> Invoke (IReadOnlyList<T> value1, IReadOnlyList<T1> value2, IReadOnlyList<T2> value3)
        {
            return Methods.Select ((x, i) =>
                ((Func<T, T1, T2, T3>) x).Invoke (value1[i], value2[i], value3[i]));
        }
    }


    public class FuncDelegates<T, T1, T2, T3, T4> : FuncDelegates<T>
    {
        public FuncDelegates (params object[] methods) : base (methods)
        {
        }
        
        
        public new Func<T, T1, T2, T3, T4> this [int index] => (Func<T, T1, T2, T3, T4>) Methods[index];


        public IEnumerable<T4> Invoke (T value1, T1 value2, T2 value3, T3 value4)
        {
            return Methods.Select (x => ((Func<T, T1, T2, T3, T4>) x).Invoke (value1, value2, value3, value4));
        }


        public IEnumerable<T4> Invoke (IReadOnlyList<T> values1, IReadOnlyList<T1> value2, IReadOnlyList<T2> value3,
            IReadOnlyList<T3> value4)
        {
            return Methods.Select ((x, i) =>
                ((Func<T, T1, T2, T3, T4>) x).Invoke (values1[i], value2[i], value3[i], value4[i]));
        }
    }
}