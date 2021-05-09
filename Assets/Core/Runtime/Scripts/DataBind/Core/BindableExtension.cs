using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KKSFramework.DataBind.Methods;
using UnityEngine;

namespace KKSFramework.DataBind
{
    public static class BindableExtension
    {
        /// <summary>
        ///     Return Methods.
        /// </summary>
        public static object ReturnMethods (IEnumerable<Component> targetComponents, Component targetComponent,
            string methodName)
        {
            var methodInfo = targetComponent.GetType ().GetMethod (methodName);
            if (methodInfo is null) return null;

            var parameterTypes = methodInfo.GetParameters ().Select (x => x.ParameterType).ToArray ();
            var returnType = methodInfo.ReturnType;

            // 반환값이 없는 함수.
            if (returnType == typeof (void))
            {
                Type toMethodDelegatesGenericType;
                var delegates = targetComponents.Select (comp => GetActionDelegate (methodInfo, comp, parameterTypes))
                    .ToArray ();

                switch (parameterTypes.Length)
                {
                    case 0:
                        return new MethodDelegates (delegates);

                    case 1:
                        toMethodDelegatesGenericType = typeof (MethodDelegates<>);
                        break;

                    case 2:
                        toMethodDelegatesGenericType = typeof (MethodDelegates<,>);
                        break;

                    case 3:
                        toMethodDelegatesGenericType = typeof (MethodDelegates<,,>);
                        break;

                    case 4:
                        toMethodDelegatesGenericType = typeof (MethodDelegates<,,,>);
                        break;

                    case 5:
                        toMethodDelegatesGenericType = typeof (MethodDelegates<,,,,>);
                        break;

                    default:
                        return null;
                }

                var makeGeneric = toMethodDelegatesGenericType.MakeGenericType (parameterTypes);
                return Activator.CreateInstance (makeGeneric, delegates);
            }


            Type toFuncDelegateGenericType;
            var funcs = targetComponents.Select (comp => GetFuncDelegate (methodInfo, comp, returnType, parameterTypes))
                .ToArray ();

            switch (parameterTypes.Length)
            {
                case 0:
                    toFuncDelegateGenericType = typeof (FuncDelegates<>);
                    break;

                case 1:
                    toFuncDelegateGenericType = typeof (FuncDelegates<,>);
                    break;

                case 2:
                    toFuncDelegateGenericType = typeof (FuncDelegates<,,>);
                    break;

                case 3:
                    toFuncDelegateGenericType = typeof (FuncDelegates<,,,>);
                    break;

                case 4:
                    toFuncDelegateGenericType = typeof (FuncDelegates<,,,,>);
                    break;

                default:
                    return null;
            }

            var makeFuncDelegateGeneric =
                toFuncDelegateGenericType.MakeGenericType (GetGenericTypes (returnType, parameterTypes));
            return Activator.CreateInstance (makeFuncDelegateGeneric, funcs);
        }


        /// <summary>
        ///     Return Method.
        /// </summary>
        public static object ReturnMethod (Component targetComponent, string methodName)
        {
            var methodInfo = targetComponent.GetType ().GetMethod (methodName);
            if (methodInfo is null) return null;

            var parameterTypes = methodInfo.GetParameters ().Select (x => x.ParameterType).ToArray ();
            var returnType = methodInfo.ReturnType;
            return returnType == typeof (void)
                ? GetActionDelegate (methodInfo, targetComponent, parameterTypes)
                : GetFuncDelegate (methodInfo, targetComponent, returnType, parameterTypes);
        }


        /// <summary>
        ///     Return action delegate.
        /// </summary>
        private static object GetActionDelegate (MethodInfo methodInfo, Component targetComponent,
            Type[] parameterTypes)
        {
            Type genericType;

            switch (parameterTypes.Length)
            {
                case 0:
                    return new Action (() => methodInfo.Invoke (targetComponent, null));

                case 1:
                    genericType = typeof (Action<>);
                    break;

                case 2:
                    genericType = typeof (Action<,>);
                    break;

                case 3:
                    genericType = typeof (Action<,,>);
                    break;

                case 4:
                    genericType = typeof (Action<,,,>);
                    break;

                case 5:
                    genericType = typeof (Action<,,,,>);
                    break;

                default:
                    return null;
            }

            var makeGeneric = genericType.MakeGenericType (parameterTypes);
            return Delegate.CreateDelegate (makeGeneric, targetComponent, methodInfo);
        }


        /// <summary>
        ///     Return func delegate.
        /// </summary>
        private static object GetFuncDelegate (MethodInfo methodInfo, Component targetComponent, Type returnType,
            IReadOnlyCollection<Type> parameterTypes)
        {
            Type genericType;
            switch (parameterTypes.Count)
            {
                case 0:
                    genericType = typeof (Func<>);
                    break;

                case 1:
                    genericType = typeof (Func<,>);
                    break;

                case 2:
                    genericType = typeof (Func<,,>);
                    break;

                case 3:
                    genericType = typeof (Func<,,,>);
                    break;

                case 4:
                    genericType = typeof (Func<,,,,>);
                    break;

                default:
                    return null;
            }

            var makeGeneric = genericType.MakeGenericType (GetGenericTypes (returnType, parameterTypes));
            return Delegate.CreateDelegate (makeGeneric, targetComponent, methodInfo);
        }


        private static Type[] GetGenericTypes (Type returnType, IEnumerable<Type> parameterTypes)
        {
            return parameterTypes.Append (returnType).ToArray ();
        }
    }
}