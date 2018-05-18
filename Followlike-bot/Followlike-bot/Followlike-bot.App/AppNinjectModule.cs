using Followlike_bot.App.ViewModel;
using Followlike_bot.BotEngine;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Followlike_bot.App
{
    public class AppNinjectModule : NinjectModule
    {
        public static IKernel GetKernel()
        {
            return new StandardKernel(new AppNinjectModule(), new BotEngineNinjectModule());
        }

        public override void Load()
        {
            Bind<MainViewModel>().ToSelf();
        }
    }
}
