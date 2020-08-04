﻿using Scheduler.Views;
using Syncfusion.SfCalendar.XForms;
using System;
using Xamarin.Forms;

namespace Scheduler.Behaviours
{
    public class WeekViewBehavior : BehaviourBase<WeekView>
    {
        private SfCalendar calendar;
        private Grid grid;
        private double calendarWidth = 500;
        private int padding = 50;

        protected override void OnAttachedTo(WeekView bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.SizeChanged += Bindable_SizeChanged;
            this.calendar = bindable.Content.FindByName<Syncfusion.SfCalendar.XForms.SfCalendar>("calendar");
            this.grid = bindable.Content.FindByName<Grid>("grid");
            if (Device.RuntimePlatform == "UWP")
            {
                this.grid.HorizontalOptions = LayoutOptions.Center;
                this.grid.WidthRequest = this.calendarWidth;
                bindable.SizeChanged += this.Bindable_SizeChanged;
            }

            this.calendar.InlineViewMode = InlineViewMode.Agenda;
            this.calendar.MonthViewSettings.SelectionShape = SelectionShape.Circle;
            if (Device.RuntimePlatform == "UWP")
            {
                this.calendar.MaximumEventIndicatorCount = 1;
                this.calendar.MonthViewSettings.InlineTextColor = Color.Black;
                this.calendar.MonthViewSettings.DayHeaderTextColor = Color.Black;
            }

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                if (Device.RuntimePlatform == "Android")
                {
                    this.calendar.MonthViewSettings.SelectionRadius = 30;
                }
                else
                {
                    this.calendar.MonthViewSettings.SelectionRadius = 20;
                }
            }

            this.calendar.NumberOfWeeksInView = 1;
        }

        private void Bindable_SizeChanged(object sender, EventArgs e)
        {
            var height = (sender as ContentView).Content.Height;
            var width = (sender as ContentView).Content.Width;

            if (Device.RuntimePlatform == "UWP")
            {
                var sampleView = sender as WeekView;
                if (sampleView.Width < this.calendarWidth + padding)
                {
                    this.grid.WidthRequest = sampleView.Width - padding;
                }
            }

            if (height > width)
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        if (Device.Idiom == TargetIdiom.Phone)
                        {
                            this.calendar.AgendaViewHeight = height * 0.75;
                        }
                        else
                        {
                            this.calendar.AgendaViewHeight = height * 0.8;
                        }
                        break;
                    case Device.Android:
                        if (Device.Idiom == TargetIdiom.Phone)
                        {
                            var info = Device.Info;
                            var totalHeight = height * info.ScalingFactor;
                            totalHeight = totalHeight - (this.calendar.HeaderHeight * info.ScalingFactor) - (this.calendar.MonthViewSettings.DayHeight * info.ScalingFactor);
                            this.calendar.AgendaViewHeight = (totalHeight - (this.calendar.HeaderHeight * info.ScalingFactor)) / 2;
                        }
                        else
                        {
                            this.calendar.AgendaViewHeight = height * 0.35;
                        }
                        break;
                    case Device.UWP:
                        this.calendar.AgendaViewHeight = height * 0.75;
                        break;
                }
            }
            else
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        if (Device.Idiom == TargetIdiom.Phone)
                        {
                            this.calendar.AgendaViewHeight = height * 0.5;
                        }
                        else
                        {
                            this.calendar.AgendaViewHeight = width * 0.5;
                        }
                        break;
                    case Device.Android:
                        if (Device.Idiom == TargetIdiom.Phone)
                        {
                            var info = Device.Info;
                            var totalHeight = height * info.ScalingFactor;
                            totalHeight = totalHeight - (this.calendar.HeaderHeight * info.ScalingFactor) - (this.calendar.MonthViewSettings.DayHeight * info.ScalingFactor);
                            this.calendar.AgendaViewHeight = (totalHeight - (50 * info.ScalingFactor)) / 2;
                        }
                        else
                        {
                            this.calendar.AgendaViewHeight = height * 0.25;
                        }
                        break;
                    case Device.UWP:
                        this.calendar.AgendaViewHeight = height * 0.75;
                        break;
                }
            }
        }

        protected override void OnDetachingFrom(WeekView bindable)
        {
            base.OnDetachingFrom(bindable);
            if (Device.RuntimePlatform == "UWP")
            {
                bindable.SizeChanged -= this.Bindable_SizeChanged;
            }

            this.grid = null;
            this.calendar = null;
        }
    }
}
