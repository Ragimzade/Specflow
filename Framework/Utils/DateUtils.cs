﻿using System;

 namespace Framework.Utils
{
    public static class DateUtils
    {
        public static DateTime GetCurrentDate()
        {
            return DateTime.Now;
        }   
        public static string GetTimeStamp()
        {
            return DateTime.Now.ToString("MM.dd.yyyy.HH.mm.ss");
        }
        
    }
}