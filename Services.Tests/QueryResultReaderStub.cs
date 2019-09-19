﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyMoney.Services.Contracts.Database;

namespace TrackMyMoney.Services.Tests
{
    internal class QueryResultReaderStub : IQueryResultReader
    {
        private int currentRowIndex = -1;
        private readonly int columnsCount;
        private readonly object[] values;

        public object this[int index] => values[currentRowIndex * columnsCount + index];

        public QueryResultReaderStub(int columnsCount, object[] values)
        {
            this.columnsCount = columnsCount;
            this.values = values;
        }

        public bool Read()
        {
            currentRowIndex++;
            var rowsCount = values.Length / columnsCount;
            return currentRowIndex < rowsCount;
        }
    }
}
