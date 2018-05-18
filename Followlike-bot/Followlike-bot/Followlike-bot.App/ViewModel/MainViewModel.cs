using Followlike_bot.BotEngine.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Input;

namespace Followlike_bot.App.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private IBotEngine botEngine;
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IBotEngine botEngine)
        {
            this.botEngine = botEngine;
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        private bool isRunning = true;
        
        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                isRunning = value;
                RaisePropertyChanged();
            }
        }

        private ICommand startBotCommand;

        public ICommand StartBotCommand
        {
            get
            {
                return startBotCommand ?? (startBotCommand = new RelayCommand(async () =>
                 {
                     IsRunning = false;
                     bool taskResult = await botEngine.Start();
                     if (!taskResult)
                     {
                         MessageBox.Show("Some unexpected error");
                     }
                     IsRunning = true;
                 }));
            }
        }

        private ICommand stopBotCommand;

        public ICommand StopBotCommand
        {
            get
            {
                return stopBotCommand ?? (stopBotCommand = new RelayCommand(() =>
                {
                    botEngine.Stop();
                }));
            }
        }
    }
}