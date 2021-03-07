using Cysharp.Threading.Tasks;
using UnityEngine;

namespace KKSFramework.Navigation
{
    public abstract class ElementBase : IElementBase
    {
        public abstract void SetElement ();
    }

    public abstract class ElementBase<T> : MonoBehaviour, IElementBase<T>
    {
        public abstract T ElementData { get; set; }

        
        public abstract void SetElement (T elementData);
    }

    
    public abstract class TaskElementBase : ITaskElementBase
    {
        public abstract UniTask SetElementAsync ();
    }
    
    
    public abstract class TaskElementBase<T> : MonoBehaviour, ITaskElementBase<T>
    {
        public abstract T ElementData { get; set; }
        
        
        public abstract UniTask SetElementAsync (T elementData);
    }
    

    public interface IElementBase
    {
        void SetElement ();
    }

    
    public interface IElementBase<T>
    {
        T ElementData { get; set; }

        
        void SetElement (T elementData);
    }

    
    public interface ITaskElementBase
    {
        UniTask SetElementAsync ();
    }
    
    
    public interface ITaskElementBase<T>
    {
        T ElementData { get; set; }

        
        UniTask SetElementAsync (T elementData);
    }
}