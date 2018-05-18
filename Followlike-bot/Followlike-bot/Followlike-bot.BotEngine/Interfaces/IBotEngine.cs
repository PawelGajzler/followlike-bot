using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Followlike_bot.BotEngine.Interfaces
{
    public interface IBotEngine
    {
        Task<bool> Start();

        bool Stop();
    }
}
