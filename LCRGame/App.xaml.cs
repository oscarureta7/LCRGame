using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

using LCRGame.Services;
using LCRGame.Views;

using Prism.Unity;
using Prism.Mvvm;
using Prism.Ioc;

namespace LCRGame
{
    //NOTE: Decided to reference Prism library to quickly enable viewmodel location with dependency injection
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IDiceService, DiceService>();
            containerRegistry.RegisterSingleton<ISimulationService, SimulationService>();
        }

        protected override Window CreateShell()
        {
            Window mainWindow = new LCRGameView();
            return mainWindow;
        }
    }
}
