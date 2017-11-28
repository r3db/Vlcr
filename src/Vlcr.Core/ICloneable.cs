using System;

namespace Vlcr.Core
{
    public interface ICloneable<out T>
    {
        T Clone();
    }
}
