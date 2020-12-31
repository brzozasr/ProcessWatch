using System;

namespace Codecool.ProcessWatch.GUI
{
    public static class GuiUtilities
    {
        internal static string SecondConverter(double seconds)
        {
            string txt = "";

            seconds = Math.Round(seconds, 3);

            if (seconds >= 60 * 60)
            {
                double h = Math.Floor(seconds / 60 / 60);
                double min = Math.Floor((seconds - 60 * 60 * h) / 60);
                double sec = Math.Floor(seconds) % 60;
                double ms = (seconds - (60 * 60 * h) - (min * 60) - sec) * 1000;
                txt = $"  s = {h} h {Math.Round(min)} min {sec} s {Math.Round(ms, 0)} ms";
            }
            else if (seconds >= 60)
            {
                double min = Math.Floor(Math.Floor(seconds) / 60);
                double sec = Math.Floor(seconds) % 60;
                double ms = (seconds - ((int) min * 60 + (int) sec)) * 1000;
                txt = $"  s = {Math.Round(min)} min {sec} s {Math.Round(ms, 0)} ms";
            }
            else if (seconds >= 1)
            {
                double s = Math.Floor(seconds);
                double ms = (seconds - s) * 1000;
                txt = $"  s = {s} s {Math.Round(ms, 0)} ms";
            }
            else if (seconds < 1)
            {
                double ms = seconds * 1000;
                txt = $"  s = {Math.Round(ms, 2)} ms";
            }

            return txt;
        }

        internal static string MegaByteConverter(double megaBytes)
        {
            string txt = "";

            if (megaBytes >= 1024 * 1024)
            {
                megaBytes = megaBytes / 1024 / 1024;
                txt = $"  MB = {Math.Round(megaBytes, 4)} TB";
            }
            else if (megaBytes >= 1024)
            {
                megaBytes = megaBytes / 1024;
                txt = $"  MB = {Math.Round(megaBytes, 4)} GB";
            }
            else if (megaBytes >= 1 && megaBytes <= 1024)
            {
                txt = $"  MB = {Math.Round(megaBytes, 4)} MB";
            }
            else if (megaBytes < 1)
            {
                megaBytes = megaBytes * 1024;
                txt = $"  MB = {Math.Round(megaBytes, 2)} kB";
            }

            return txt;
        }
    }
}