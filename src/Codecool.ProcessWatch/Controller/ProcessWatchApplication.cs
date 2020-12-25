using System;
using System.Collections.Generic;
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

        public static (int NumberOfPages, List<MemoryItemProcess> ProcessesList) SelectProcessesByName(
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

        public static (int NumberOfPages, List<MemoryItemProcess> ProcessesList) SelectProcessesStartBeforeDate(
            int pageSize, int pageNo, int? minute = null, int? hour = null, int? day = null, int? month = null,
            int? year = null)
        {
            minute ??= DateTime.Now.Minute;
            hour ??= DateTime.Now.Hour;
            day ??= DateTime.Now.Day;
            month ??= DateTime.Now.Month;
            year ??= DateTime.Now.Year;

            var allMemoryItemProcesses = new DataHelper().GetAllMemoryItemProcesses();

            DateTime searchingDate;

            try
            {
                searchingDate = new DateTime(year.Value, month.Value, day.Value, hour.Value, minute.Value, 0);
            }
            catch (Exception)
            {
                searchingDate = DateTime.Today;
            }

            var searchedDatesList = allMemoryItemProcesses
                .Where(x => x.StartTime != null && x.StartTime.Value < searchingDate).ToList();


            return ProcessesPagination(pageSize, pageNo, searchedDatesList);
        }
        
        public static (int NumberOfPages, List<MemoryItemProcess> ProcessesList) SelectProcessesStartAfterDate(
            int pageSize, int pageNo, int? minute = null, int? hour = null, int? day = null, int? month = null,
            int? year = null)
        {
            minute ??= DateTime.Now.Minute;
            hour ??= DateTime.Now.Hour;
            day ??= DateTime.Now.Day;
            month ??= DateTime.Now.Month;
            year ??= DateTime.Now.Year;

            var allMemoryItemProcesses = new DataHelper().GetAllMemoryItemProcesses();

            DateTime searchingDate;

            try
            {
                searchingDate = new DateTime(year.Value, month.Value, day.Value, hour.Value, minute.Value, 0);
            }
            catch (Exception)
            {
                searchingDate = DateTime.Today;
            }

            var searchedDatesList = allMemoryItemProcesses
                .Where(x => x.StartTime != null && x.StartTime.Value > searchingDate).ToList();


            return ProcessesPagination(pageSize, pageNo, searchedDatesList);
        }
        
        public static (int NumberOfPages, List<MemoryItemProcess> ProcessesList) SelectProcessesStartAtDay(
            int pageSize, int pageNo, int day)
        {
            var allMemoryItemProcesses = new DataHelper().GetAllMemoryItemProcesses();

            int searchingDay;

            if (day > 0 && day <= 31)
            {
                searchingDay = day;
            }
            else
            {
                searchingDay = DateTime.Now.Day;
            }

            var searchedDatesList = allMemoryItemProcesses
                .Where(x => x.StartTime != null && x.StartTime.Value.Day == searchingDay).ToList();


            return ProcessesPagination(pageSize, pageNo, searchedDatesList);
        }
        
        public static (int NumberOfPages, List<MemoryItemProcess> ProcessesList) SelectProcessesStartAtMonth(
            int pageSize, int pageNo, int month)
        {
            var allMemoryItemProcesses = new DataHelper().GetAllMemoryItemProcesses();

            int searchingMonth;

            if (month > 0 && month <= 12)
            {
                searchingMonth = month;
            }
            else
            {
                searchingMonth = DateTime.Now.Month;
            }

            var searchedDatesList = allMemoryItemProcesses
                .Where(x => x.StartTime != null && x.StartTime.Value.Month == searchingMonth).ToList();


            return ProcessesPagination(pageSize, pageNo, searchedDatesList);
        }
        
        public static (int NumberOfPages, List<MemoryItemProcess> ProcessesList) SelectProcessesStartAtDate(
            int pageSize, int pageNo, int day, int month, int year)
        {
            var allMemoryItemProcesses = new DataHelper().GetAllMemoryItemProcesses();

            DateTime searchingDate;

            try
            {
                searchingDate = new DateTime(year, month, day);
            }
            catch (Exception)
            {
                searchingDate = DateTime.Today;
            }

            var searchedDatesList = allMemoryItemProcesses
                .Where(x => x.StartTime != null
                && x.StartTime.Value.Day == searchingDate.Day
                && x.StartTime.Value.Month == searchingDate.Month
                && x.StartTime.Value.Year == searchingDate.Year).ToList();


            return ProcessesPagination(pageSize, pageNo, searchedDatesList);
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