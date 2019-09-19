using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TrackMyMoney.Services.Contracts.Database
{
    public interface IQueryResultReader
    {
        object this[int index] { get; }
        bool Read();
    }
}
