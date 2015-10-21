namespace AMSLLC.Listener.ApplicationService
{
    using System.Collections.Generic;

    public class DataPropertyInfo
    {
        private readonly string _property;

        public DataPropertyInfo(string property, bool isList)
        {
            this._property = property;
            this.IsList = isList;
        }

        public bool IsList { get; }

        public object GetValue(object owner)
        {
            var expandoDictionary = owner as IDictionary<string, object>;
            if (expandoDictionary != null)
            {
                return expandoDictionary[this._property];
            }
            else
            {
                var propInfo = owner.GetType().GetProperty(this._property);
                return propInfo.GetValue(owner);
            }
            // throw new InvalidOperationException($"Cannot find property {_property} or owner is not dynamic");
        }

        public void SetValue(object owner, object value)
        {
            var expandoDictionary = owner as IDictionary<string, object>;
            if (expandoDictionary != null)
            {
                expandoDictionary[this._property] = value;
            }
            else
            {
                var propInfo = owner.GetType().GetProperty(this._property);
                propInfo.SetValue(owner, value); ;
                //throw new InvalidOperationException($"Cannot find property {_property} or owner is not dynamic");
            }
        }
    }
}
