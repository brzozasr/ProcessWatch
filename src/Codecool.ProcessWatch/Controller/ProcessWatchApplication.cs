using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using Codecool.ProcessWatch.Model;

namespace Codecool.ProcessWatch.Controller
{
    public static class ProcessWatchApplication
    {
        public static (int NumberOfPages, List<MemoryItemProcess> ProcessesList) AllProcesses(
            int pageSize, int pageNo)
        {
            var allMemoryItemProcesses = new DataHelper().GetAllMemoryItemProcesses();

            return ProcessesPagination(pageSize, pageNo, allMemoryItemProcesses);
        }

        public static (int NumberOfPages, List<MemoryItemProcess> ProcessesList) SelectProcessesByNamePagination(
            int pageSize, int pageNo, string searchedString)
        {
            var allMemoryItemProcesses = new DataHelper().GetAllMemoryItemProcesses();

            var search = searchedString.ToLower();

            string pattern = @"" + search;
            Regex rgx = new Regex(pattern);

            var searchedProcesses = allMemoryItemProcesses
                .Where(x => !string.IsNullOrEmpty(x.ProcessName) && rgx.IsMatch(x.ProcessName.ToLower())).ToList();

            return ProcessesPagination(pageSize, pageNo, searchedProcesses);
        }

        private static (int NumberOfPages, List<MemoryItemProcess> ProcessesList) ProcessesPagination(
            int pageSize, int pageNo, List<MemoryItemProcess> listProcesses)
        {
            var lengthList = listProcesses.Count();

            if (IsPageSizeAndNoCorrect(pageSize, pageNo, lengthList))
            {
                double pages = (double) lengthList / pageSize;
                double numberOfPages = Math.Ceiling(pages);

                var processesPage = listProcesses.OrderByDescending(x => x.StartTime)
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