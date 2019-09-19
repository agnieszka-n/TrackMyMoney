using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyMoney.Services.Contracts.Database;

namespace TrackMyMoney.Services.Database
{
    public class QueryResultReader: IQueryResultReader
    {
        private readonly DbDataReader internalReader;
        public object this[int index] => internalReader[index];

        public QueryResultReader(DbDataReader internalReader)
        {
            this.internalReader = internalReader;
        }

        public bool Read()
        {
            return internalReader.Read();
        }
    }
}
