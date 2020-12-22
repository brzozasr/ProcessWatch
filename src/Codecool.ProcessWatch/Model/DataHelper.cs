using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Codecool.ProcessWatch.Model
{
    public class DataHelper
    {
        private readonly List<MemoryItemProcess> _prosessesList;

        public DataHelper()
        {
            _prosessesList = new List<MemoryItemProcess>();
        }

        public List<MemoryItemProcess> GetMemoryItemProcessList()
        {
            foreach (var process in Process.GetProcesses())
            {
                _prosessesList.Add(new MemoryItemProcess(process.Id));
            }

            return _prosessesList;
        }
    }
}
