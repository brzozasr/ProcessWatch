using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Codecool.ProcessWatch.Model
{
    public class DataHelper
    {
        private readonly List<MemoryItemProcess> _processesList;

        internal DataHelper()
        {
            _processesList = new List<MemoryItemProcess>();
        }

        internal List<MemoryItemProcess> GetAllMemoryItemProcesses()
        {
            foreach (var process in Process.GetProcesses())
            {
                _processesList.Add(new MemoryItemProcess(process.Id));
            }

            return _processesList;
        }
    }
}
