using System;

namespace AMSLLC.Listener.ODataService.Services
{
    public interface IAutoConvertor
    {
        object Convert(object rawData, Type target);
    }
}