using Syncfusion.XForms.DataForm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Scheduler.Models
{
    public class NewJob : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private string _description { get; set; }
        private float _quote { get; set; }
        private DateTime _scheduledOn { get; set; } = DateTime.Now.AddDays(1);
        private JobType _type { get; set; }
        private DateTime _scheduleTime { get; set; }
        private bool _reminder { get; set; } = true;

        private Dictionary<string, List<string>> propErrors = new Dictionary<string, List<string>>();
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors
        {
            get
            {
                var propErrorsCount = propErrors.Values.FirstOrDefault(r => r.Count > 0);
                if (propErrorsCount != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
       
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description cannot be empty.")]
        [StringLength(50, ErrorMessage = "Description should not exceed 200 characters")]
        public string Description 
        {
            get
            {
                return _description;
            }
            set
            {
                if (value != _description)
                {
                    _description = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Quote cannot be empty.")]
        public float Quote 
        {
            get
            {
                return _quote;
            }
            set
            {
                if (value != _quote)
                {
                    _quote = value;
                    this.RaisePropertyChanged("Quote");
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Schedule Date cannot be empty.")]
        [DateRange(MinYear = 2020, MaxYear = 2030, ErrorMessage = "Schedule date is invalid")]
        public DateTime ScheduledOn
        {
            get
            {
                return _scheduledOn;
            }
            set
            {
                if (value != _scheduledOn)
                {
                    _scheduledOn = value;
                    this.RaisePropertyChanged("ScheduledOn");
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Job type Date cannot be empty.")]
        public JobType Type
        {
            get
            {
                return _type;
            }
            set 
            {
                if (value != _type)
                {
                    _type = value;
                    this.RaisePropertyChanged("Type");
                }
            } 
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Schedule time type Date cannot be empty.")]
        [DataType(DataType.Time)]
        public DateTime ScheduleTime
        {
            get
            {
                return _scheduleTime;
            }
            set
            {
                if (value != _scheduleTime)
                {
                    ScheduledOn = ScheduledOn.Date + value.TimeOfDay;
                    RaisePropertyChanged("ScheduleTime");
                }
            }
        }
        public bool Reminder 
        {
            get { return _reminder; }
            set { _reminder = value; }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (propertyName == null)
            {
                return null;
            }
            List<string> errors = new List<string>();
            if (propertyName == "Description")
            {
                return string.Empty;
            }
            else
            {
                errors.Clear();
                propErrors.Remove(propertyName);
            }

            if (propertyName == "ScheduledOn")
            {
                if (ScheduledOn.Date <= DateTime.Today.Date)
                {
                    errors.Add("Schedule date needs to be in future.");
                    propErrors.Add(propertyName, errors);
                    return errors;
                }
                else
                {
                    errors.Clear();
                    propErrors.Remove(propertyName);
                }
            }
            else
            {
                errors.Clear();
                propErrors.Remove(propertyName);
            }


            if (propertyName == "Quote")
            {
                var quoteLenght = this.Quote.ToString().Length;
                if (quoteLenght > 8)
                {
                    errors.Add("Quote cannot be more than 5 digits long.");
                    propErrors.Add(propertyName, errors);
                }
                else
                {
                    errors.Clear();
                    propErrors.Remove(propertyName);
                }
            }
            else
            {
                errors.Clear();
                propErrors.Remove(propertyName);
            }


            if (propErrors.TryGetValue(propertyName, out errors))
            {
                return errors;
            }

            return null;
        }

        private void RaisePropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}

