using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Jab;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using Project1.Presets;
using Project1.Viewmodels;

namespace Project1;

[ServiceProvider]
[Singleton<MainViewmodel>]
[Singleton<SettingsViewmodel>]
[Singleton<IFileDialogService,Avaloniafiledialogservice>]
[Singleton<StartCopyService>]
[Singleton<AktualiselSettingsInUI>]
[Singleton<Settings>(Instance = nameof(JsonAppSettings))]
[Singleton<PresetIndex>]
[Singleton<GetSetPresetIndex>]
[Singleton<ExportViewmodel>]
[Singleton<MainWindow>]
[Singleton<cleanwerth>(Instance = nameof(length))]
[Singleton<IProgressBarService>]
sealed partial class ServiceProvider
{
    public Settings JsonAppSettings { get; }
    public cleanwerth length { get; set; }
    public ServiceProvider()
    {


        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GnuCopy", "Settings", "Settings.json");
        Task.Run(() =>
        {
            cleanwerth a = new cleanwerth()
            {
                Datalengt = 0
            };
            length = a;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GnuCopy"));
                Directory.CreateDirectory(Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GnuCopy", "Settings"));
            }
        });
        

        JsonAppSettings = File.Exists(path) ? JsonConvert.DeserializeObject<Settings>(File.ReadAllText(path)) ?? new() : new();
    }
}