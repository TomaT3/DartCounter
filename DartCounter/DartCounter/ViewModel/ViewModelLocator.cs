using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartCounter.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
        }

        public SettingsViewModel SettingsViewModel
        {
            get { return ServiceLocator.Current.GetInstance<SettingsViewModel>(); }
        }

        public GameViewModel GameViewModel
        {
            get { return ServiceLocator.Current.GetInstance<GameViewModel>(); }
        }
    }
}
