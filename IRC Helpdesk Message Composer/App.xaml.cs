using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using IRC.Helpdesk.Core;
using IRC.Helpdesk.ViewModels;
using ME.Wpf.Mvvm;

namespace IRC_Helpdesk_Message_Composer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Register dialog service singlton
            var dialogService = new Dialog(null);
            dialogService.Register<MainWindowViewModel, MainWindow>();
            dialogService.Register<MessageDialogViewModel, MessageDialog>();
            dialogService.Register<ConfigureDialogViewModel, ConfigureDialog>();
            dialogService.Register<AboutViewModel, AboutDialog>();
            DI.AddSinglton<IDialogService>(dialogService);

            //Register Categories Provider as singlton
            ICategoriesProvider catsProvider = new CategoriesProvider();
            DI.AddSinglton<ICategoriesProvider>(catsProvider);

            //Register message composer as singlton.
            DI.AddSinglton<IMessageComposer>(new MessageComposer());

            var assetsConfig = new AssetsSourceConfiguration(Environment.CurrentDirectory + "\\Configs\\ExcelConfig.json");
            DI.AddSinglton<IAssetSourceConfiguration>(assetsConfig);

            var appSettingsProvider = new JsonSettingsProvider(Environment.CurrentDirectory + "\\Configs\\AppSettings.json");
            DI.AddSinglton<ISettingsProvider>(appSettingsProvider);

            var assetsSource = new ExcelAssetReader();
            assetsSource.Configure(assetsConfig);

            DI.AddSinglton<IAssetSource>(assetsSource);

            DI.AddSinglton<IClipBoard>(new ClipboardService());

            //Register mail service as scoped
            DI.AddService<IMailService, OutlookMailService>();
            
            DI.AddTransient(typeof(MainWindowViewModel));

            DI.Construct();
            IRC.Helpdesk.ViewModels.DI.SetProvider(DI.Provider);

            DI.GetService<IDialogService>().ShowWindow<MainWindowViewModel>(DI.GetService<MainWindowViewModel>(), true);
        }
    }
}
