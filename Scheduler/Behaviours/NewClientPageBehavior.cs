using System;
using System.ComponentModel;
using Xamarin.Forms;
using Syncfusion.XForms.DataForm;

namespace Scheduler.Behaviours
{
    public class NewClientPageBehavior : BehaviourBase<SfDataForm>
    {
        private SfDataForm dataForm;

        protected override void OnAttachedTo(SfDataForm bindable)
        {
            this.dataForm = bindable;
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
                else if (e.DataFormItem.Name.Equals("Phone"))
                {
                    (e.DataFormItem as DataFormTextItem).KeyBoard = Keyboard.Numeric;
                }
                else if (e.DataFormItem.Name.Equals("Email"))
                {
                    (e.DataFormItem as DataFormTextItem).KeyBoard = Keyboard.Email;
                }
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("FullName"))
            {
                var value = (string)sender.GetType().GetProperty("FullName").GetValue(sender);
                if (string.IsNullOrEmpty(value))
                {
                    dataForm.Validate("FullName");
                }
            }
            else if (e.PropertyName.Equals("Phone"))
            {
                var value = (string)sender.GetType().GetProperty("Phone").GetValue(sender);
                if (string.IsNullOrEmpty(value))
                {
                    dataForm.Validate("Phone");
                }
            }
            else if (e.PropertyName.Equals("Email"))
            {
                var value = (string)sender.GetType().GetProperty("Email").GetValue(sender);
                if (string.IsNullOrEmpty(value))
                {
                    dataForm.Validate("Email");
                }
            }
            else if (e.PropertyName.Equals("Address"))
            {
                var value = (string)sender.GetType().GetProperty("Address").GetValue(sender);
                if (string.IsNullOrEmpty(value))
                {
                    dataForm.Validate("Address");
                }
            }
        }
    }
}
