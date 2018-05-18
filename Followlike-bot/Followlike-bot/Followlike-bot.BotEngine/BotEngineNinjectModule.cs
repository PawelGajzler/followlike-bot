using Followlike_bot.BotEngine.Interfaces;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Followlike_bot.BotEngine
{
    public class BotEngineNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBotEngine>().To<BotEngine>();
        }
    }
}
