using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSLLC.Core
{
    public interface IDependencyInjectionAdapter
    {
        TType ResolveType<TType>();
        object ResolveType(Type type);
        TType ResolveNamed<TType>(string name);
    }
}
