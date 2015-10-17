namespace AMSLLC.Listener.ApplicationService
{
    using System.Collections.Generic;

    public class DataPropertyInfo
    {
        private readonly string _property;

        public DataPropertyInfo(string property, bool isList)
        {
            _property = property;
            IsList = isList;
        }

        public bool IsList { get; }

        public object GetValue(object owner)
        {
            var expandoDictionary = owner as IDictionary<string, object>;
            if (expandoDictionary != null)
            {
                return expandoDictionary[_property];
            }
            else
            {
                var propInfo = owner.GetType().GetProperty(_property);
                return propInfo.GetValue(owner);
            }
            // throw new InvalidOperationException($"Cannot find property {_property} or owner is not dynamic");
        }

        public void SetValue(object owner, object value)
        {
            var expandoDictionary = owner as IDictionary<string, object>;
            if (expandoDictionary != null)
            {
                expandoDictionary[_property] = value;
            }
            else
            {
                var propInfo = owner.GetType().GetProperty(_property);
                propInfo.SetValue(owner, value); ;
                //throw new InvalidOperationException($"Cannot find property {_property} or owner is not dynamic");
            }
        }
    }
}
