using System.Collections.Generic;
using System.Diagnostics;
using Codecool.ProcessWatch.Model;

namespace Codecool.ProcessWatch.Controller
{
    public class ProcessWatchApplication
    {
        private List<MemoryItemProcess> prosessesList = new List<MemoryItemProcess>();

        // public List<MemoryItemProcess> GetProcessesList()
        // {
        //     foreach (Process process in Process.GetProcesses())
        //     {
        //         prosessesList.Add(new MemoryItemProcess().GetMemoryItemProcessById(process.Id));
        //     }
        //     return prosessesList;
        // }
    }
}

