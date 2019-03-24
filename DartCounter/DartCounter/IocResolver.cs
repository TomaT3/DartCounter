using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using DartCounter.Model;
using DartCounter.Services;
using DartCounter.ViewModel;
using Xamarin.Forms;

namespace DartCounter
{
    public sealed class IocResolver
    {
        public static void Initialize()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.Register<INavigation>(context => App.Current.MainPage.Navigation).SingleInstance();

            // ViewModels
            builder.RegisterType<SettingsViewModel>().AsSelf();
            builder.RegisterType<GameViewModel>().AsSelf();

            // Services
            builder.RegisterType<SettingsService>().As<ISettingsService>().SingleInstance();
            builder.RegisterType<Game>().As<IGame>().SingleInstance();

            IContainer container = builder.Build();

            AutofacServiceLocator asl = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => asl);
        }
    }
}

