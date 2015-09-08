using System;

namespace WNP.Listener.ODataService.Services
{
    public interface IAutoConvertor
    {
        object Convert(object rawData, Type target);
    }
}