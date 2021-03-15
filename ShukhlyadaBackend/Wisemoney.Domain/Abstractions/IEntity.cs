using System;
using System.Collections.Generic;
using System.Text;

namespace Shukhlyada.Domain.Abstractions
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
