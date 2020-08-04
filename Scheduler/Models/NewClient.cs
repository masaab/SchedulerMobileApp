using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Scheduler.Models
{
    public class NewClient : INotifyPropertyChanged, INotifyDataErrorInfo
    {

        private string _fullName;
        private string _phone;
        private string _email;
        private string _address;
        private Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private Dictionary<string, List<string>> propErrors = new Dictionary<string, List<string>>();

        public NewClient()
        {

        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "FullName cannot be empty.")]
        [StringLength(50, ErrorMessage = "FullName should not exceed 50 characters")]

        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                if (value != _fullName)
                {
                    _fullName = value;
                    this.RaisePropertyChanged("FullName");
                }

            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone cannot be empty.")]
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                if (value != Phone)
                {
                    _phone = value;
                    this.RaisePropertyChanged("Phone");
                }

            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email cannot be empty.")]
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (value != Email)
                {
                    _email = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Address cannot be empty.")]
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                if (value != _address)
                {
                    _address = value;
                    this.RaisePropertyChanged("Address");
                }
            }
        }

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

        public IEnumerable GetErrors(string propertyName)
        {
            if (propertyName == null)
            {
                return null;
            }
            List<string> errors = new List<string>();
            if (propertyName == "Phone")
            {
                if (string.IsNullOrEmpty(Phone))
                {
                    return string.Empty;
                }
                var phoneLength = this.Phone.ToString().Length;
                if (phoneLength < 10)
                {
                    errors.Add("Length should be between 9 and 10.");
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

            if (propertyName == "Email" && (!string.IsNullOrEmpty(Email)))
            {
                if (string.IsNullOrEmpty(Email))
                {
                    return string.Empty;
                }
                if (!emailRegex.IsMatch(Email))
                {
                    errors.Add("Please enter a valid email.");
                    propErrors.Add(propertyName, errors);
                }
                else
                {
                    errors.Clear();
                    propErrors.Remove(propertyName);
                }
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
