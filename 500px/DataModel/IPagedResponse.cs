﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Picfinity.DataModel
{
  public interface IPagedResponse<T>
    {
        IEnumerable<T> Items { get; }
        int VirtualCount { get; }
        int rpp { get; set; }
    }
}
