using Autofac;
using Scheduler.Managers;
using Scheduler.Managers.Abstraction;
using Scheduler.Repository;
using Scheduler.ViewModels;
using System;

namespace Scheduler.Bootstrap
{
    public class AppContainer
    {
        private static IContainer Container { get; set; }

    public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ClientViewModel>().SingleInstance();
            builder.RegisterType<NewClientViewModel>().SingleInstance();
            builder.RegisterType<ClientDetailViewModel>().SingleInstance();

            builder.RegisterType<NewJobViewModel>().SingleInstance();
            builder.RegisterType<JobViewModel>().SingleInstance();
            builder.RegisterType<JobDetailViewModel>().SingleInstance();

            builder.RegisterType<MainViewModel>().SingleInstance();
            builder.RegisterType<MenuViewModel>().SingleInstance();
            builder.RegisterType<CalenderViewModel>().SingleInstance();
            builder.RegisterType<WeekViewModel>().SingleInstance();

            builder.RegisterType<ClientManager>().As<IClientManager>().SingleInstance();
            builder.RegisterType<JobManager>().As<IJobManager>().SingleInstance();
            builder.RegisterType<NavigationManager>().As<INavigationManager>().SingleInstance();
            builder.RegisterType<CalendarManager>().As<ICalenderManager>().SingleInstance();

            builder.RegisterType<DataRepository>().As<IDataRepository>().SingleInstance();

            Container = builder.Build();
        }

        public static object Resolve(Type type)
        {
            try
            {
                return Container.Resolve(type);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static T Resolve<T>()
        {
            try
            {
                return Container.Resolve<T>();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
