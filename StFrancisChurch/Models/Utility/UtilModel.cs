﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessObject.DataModel;

namespace StFrancisChurch.Models.Utility
{
    public class UtilModel
    {

    }

    public class SelectItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SelectItemValue
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class ReturnData
    {
        public bool HasValue { get; set; }
        public string Message { get; set; }
    }

    public class SacramentIndex
    {
        public int Baptism { get; set; }
        public int Communion { get; set; }
        public int Matrimony { get; set; }
        public int Confirmation { get; set; }
    }

    public class FamilyMembers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string Relationship { get; set; }
        public int MemberId { get; set; }
    }

    public class MembersSociety
    {
        public int Id { get; set; }
        public string Society { get; set; }
        public string Position { get; set; }
        public string AppointDate { get; set; }
        public int MemberId { get; set; }
    }

    public class ParishEventUI
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreateDate { get; set; }
        public string EventImage { get; set; }
    }

    public class OutStation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string DisplayName { get; set; }
        public string Contact { get; set; }
        public string CreateDate { get; set; }
    }

    public class MassSchedules
    {
        public string StationName { get; set; }
        public DaysOfTheWeek DayOfTheWeek { get; set; }
        public string Time { get; set; }
    }
    
    public class ScheduleModel
    {
        public int Station { get; set; }
        public string Sunday { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
    }

    public class DailyVerseModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Quote { get; set; }
        public int Active { get; set; }
    }
}