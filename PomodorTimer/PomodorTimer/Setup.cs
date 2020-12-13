using System;
using System.Collections.Generic;
using System.Text;

namespace PomodorTimer
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
        }

        private static void RegisterSingletons()
        {
            Ioc.RegisterSingleton<Settings, Settings>();
        }
    }
}
