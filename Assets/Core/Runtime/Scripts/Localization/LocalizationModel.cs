using System;
using System.Linq;

namespace KKSFramework.Localization
{
    [Serializable]
    public class LocalizationModel
    {
        public string key;

        public string[] args = new string[0];
        
        public object[] ToObjectArgs => Array.ConvertAll (args, x => x.ToString ()).ToArray ();
    }
}