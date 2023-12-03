using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using IrregularVerbs.Services;

namespace IrregularVerbs
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static App Instance { get; private set; } = null!;

        public IrregularVerbsStorage IrregularVerbsStorage { get; private set; }

        public App()
        {
            Instance = this;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IrregularVerbsStorage = new IrregularVerbsStorage();

        }
    }
}