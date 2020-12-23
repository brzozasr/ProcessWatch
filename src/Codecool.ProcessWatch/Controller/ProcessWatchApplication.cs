using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Codecool.ProcessWatch.Model;

namespace Codecool.ProcessWatch.Controller
{
    public static class ProcessWatchApplication
    {
        public static (int NumberOfPages, List<MemoryItemProcess> ProcessesList) ProcessesPagination(int pageSize, int pageNo)
        {
            var allMemoryItemProcesses = new DataHelper().GetAllMemoryItemProcesses();
            var lengthList = allMemoryItemProcesses.Count();

            if (IsPageSizeAndNoCorrect(pageSize, pageNo, lengthList))
            {
                double pages = (double) lengthList / pageSize;
                double numberOfPages = Math.Ceiling(pages);

                var processesPage = allMemoryItemProcesses.OrderByDescending(x => x.StartTime)
                    .Skip(pageNo - 1).Take(pageSize).ToList();

                return ((int) numberOfPages, processesPage);
            }

            var emptyList = new List<MemoryItemProcess>();
            return (0, emptyList);
        }

        private static bool IsPageSizeAndNoCorrect(int pageSize, int pageNo, int lengthList)
        {
            if (pageSize > 0 && pageNo > 0 && lengthList > 0)
            {
                double pages = (double) lengthList / pageSize;
                double numberOfPages = Math.Ceiling(pages);

                if ((int) numberOfPages >= pageNo)
                {
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}

