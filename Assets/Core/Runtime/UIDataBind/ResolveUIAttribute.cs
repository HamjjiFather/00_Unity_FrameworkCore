using System;

namespace KKSFramework.DataBind
{
    public interface IBinder
    {
        
    }
    
    
    [AttributeUsage(AttributeTargets.Field)]
    public class ResolveUIAttribute : Attribute
    {
        public readonly string Key;

        public ResolveUIAttribute (string key)
        {
            Key = key;
        }
    }
}