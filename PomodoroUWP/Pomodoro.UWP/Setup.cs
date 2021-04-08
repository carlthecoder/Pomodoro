using Pomodoro.UWP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.UWP
{
    public static class Setup
    {
        public static void Initialize()
        {
            RegisterTypes();
            RegisterSingletons();
        }

        private static void RegisterTypes()
        {
            Ioc.RegisterType<IPomodor, Pomodor>();
        }

        private static void RegisterSingletons()
        {
            Ioc.RegisterSingleton<ISettings, Settings>();
        }
    }
}
