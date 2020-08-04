using System;
using System.ComponentModel;
using Syncfusion.XForms.DataForm;

namespace Scheduler.Behaviours
{
    public class NewJobPageBehavior : BehaviourBase<SfDataForm>
    {
        private SfDataForm dataForm;

        protected override void OnAttachedTo(SfDataForm bindable)
        {
            this.dataForm = bindable;
            this.dataForm.RegisterEditor("Reminder", "Switch");
            this.dataForm.AutoGeneratingDataFormItem += this.OnAutoGeneratingDataFormItem;
            this.dataForm.BindingContextChanged += this.OnBindingContextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(SfDataForm bindable)
        {
            base.OnDetachingFrom(bindable);
            dataForm.AutoGeneratingDataFormItem -= OnAutoGeneratingDataFormItem;
            (dataForm.DataObject as INotifyPropertyChanged).PropertyChanged -= OnPropertyChanged;
            dataForm.BindingContextChanged -= OnBindingContextChanged;
            dataForm = null;
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            (dataForm.DataObject as INotifyPropertyChanged).PropertyChanged += OnPropertyChanged;
        }

        private void OnAutoGeneratingDataFormItem(object sender, AutoGeneratingDataFormItemEventArgs e)
        {
            if (e.DataFormItem != null)
            {
                if (e.DataFormItem.Name.Equals("HasErrors"))
                {
                    e.Cancel = true;
                }
                else if (e.DataFormItem.Name == "Reminder")
                {
                    e.DataFormItem.LayoutOptions = LayoutType.Default;
                }
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Description"))
            {
                var value = (string)sender.GetType().GetProperty("Description").GetValue(sender);
                if (string.IsNullOrEmpty(value))
                {
                    dataForm.Validate("Description");
                }
            }
            else if (e.PropertyName.Equals("Quote"))
            {
                var value = (float)sender.GetType().GetProperty("Quote").GetValue(sender);
                if (value <= 0 || value > 8)
                {
                    dataForm.Validate("Quote");
                }
            }
            else if (e.PropertyName.Equals("ScheduledOn"))
            {
                var value = (DateTime)sender.GetType().GetProperty("ScheduledOn").GetValue(sender);
                if (value < DateTime.Today)
                {
                    dataForm.Validate("ScheduledOn");
                }
            }
        }
    }
}
