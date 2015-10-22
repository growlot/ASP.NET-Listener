using System;

namespace AMSLLC.Listener.ODataService.Actions.Attributes
{
    /// <summary>
    /// This attribute is used to define custom prefix for generated entity action (AMSLLC.Listener.{Prefix}_{ActionName}).
    /// By default prefix is class name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ActionPrefixAttribute : Attribute
    {
        public string Prefix { get; }

        public ActionPrefixAttribute(string prefix)
        {
            Prefix = prefix;
        }
    }
}