using Scheduler.Managers.Abstraction;
using Scheduler.ViewModels;
using Scheduler.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Scheduler.Managers
{
    public class NavigationManager : INavigationManager
    {
        private readonly Dictionary<Type, Type> _mapping;

        protected Application CurrentApplication => Application.Current;
        public NavigationManager()
        {
            _mapping = new Dictionary<Type, Type>();
            CreatePageViewModelMapping();
        }
        public Task ClearBackStack()
        {
            throw new NotImplementedException();
        }

        public async Task InitializeAsync()
        {
            await NavigateToAsync<MainViewModel>();
        }

        public async  Task NavigateBackAsync()
        {
            if (CurrentApplication.MainPage is MainPage mainPage)
            {
                 await mainPage.Detail.Navigation.PopAsync();
            }
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public async Task PopToRootAsync()
        {
            if (CurrentApplication.MainPage is MainPage mainPage)
            {
                await mainPage.Detail.Navigation.PopToRootAsync();
            }
        }

        public Task RemoveLastFromBackStackAsync()
        {
            throw new NotImplementedException();
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            try
            {
                Page page = CreatePage(viewModelType, parameter);
                if (page is MainPage)
                {
                    CurrentApplication.MainPage = page;
                }
                else
                {
                    MainPage mainpage = CurrentApplication.MainPage as MainPage;
                    if (mainpage.Detail is NavigationPage navigationPage)
                    {
                        var currentPage = navigationPage.CurrentPage;
                        if (currentPage.GetType() != page.GetType())
                        {
                            await navigationPage.PushAsync(page);
                        }
                    }

                    else
                    {
                        navigationPage = new NavigationPage(page);
                        mainpage.Detail = navigationPage;
                    }
                }

                await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
            }
            catch(Exception Ex)
            {
                throw;
            }
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            return Activator.CreateInstance(pageType) as Page;
        }
   

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mapping.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }
            return _mapping[viewModelType];
        }

        private void CreatePageViewModelMapping()
        {
            _mapping.Add(typeof(MainViewModel), typeof(MainPage));
            _mapping.Add(typeof(MenuViewModel), typeof(MenuPage));

            _mapping.Add(typeof(ClientViewModel), typeof(ClientPage));
            _mapping.Add(typeof(ClientDetailViewModel), typeof(ClientDetailPage));
            _mapping.Add(typeof(NewClientViewModel), typeof(NewClientPage));

            _mapping.Add(typeof(JobViewModel), typeof(JobPage));
            _mapping.Add(typeof(JobDetailViewModel), typeof(JobDetailPage));
            _mapping.Add(typeof(NewJobViewModel), typeof(NewJobPage));

            _mapping.Add(typeof(CalenderViewModel), typeof(CalenderPage));
            _mapping.Add(typeof(WeekViewModel), typeof(WeekView));

        }
    }
}
