using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Project1.Viewmodels;
using static Project1.MainWindow;

namespace Project1;

public class Copy
{
    private static string TempFolder = Path.Combine(MainViewmodel.Default.Copyfromtext, "OnionwareTemp");
    public static async Task Settings(bool zip)
    {
        IOC.Default.GetService<MainViewmodel>().ignorefolder.Add("OnionwareTemp");
        if(zip){ Directory.CreateDirectory(TempFolder);}
        if (IOC.Default.GetService<Settings>().Listingart ==true)
        {
            await White(zip);
            
        }else if (IOC.Default.GetService<Settings>().Listingart == false)
        {
            await Black(zip);
        }
        else
        {
            await Without(zip);
        }
    }

    private static async Task Without(bool zip)
    {
        List<string> firstfiles = Directory.EnumerateFiles(MainViewmodel.Default.Copyfromtext).ToList();
        foreach (var file in firstfiles)
        {
            try
            {
                File.Copy(file, zip == false ? Path.Combine(MainViewmodel.Default.Copytotext,GetName(file)):Path.Combine(TempFolder,GetName(file)), overwrite: IOC.Default.GetService<Settings>().Overrite);
                IOC.Default.GetService<IProgressBarService>().Progress();
            }
            catch (IOException e)
            {
                continue;
            }
        }

        List<string> folders = Directory.EnumerateDirectories(MainViewmodel.Default.Copyfromtext).ToList();
        foreach (var folder in folders)
        {
            Directory.CreateDirectory(Path.Combine(FolderPath(folder,zip)));
            List<string> files = Directory.EnumerateFiles(folder).ToList();
            foreach (var file in files)
            {
                try
                {
                    File.Copy(file, Path.Combine(FolderPath(folder,zip),GetName(file)), overwrite: IOC.Default.GetService<Settings>().Overrite);
                }
                catch (IOException e)
                {
                    continue;
                }

                IOC.Default.GetService<IProgressBarService>().Progress();
            }
        }
    }
    private static async Task Black(bool zip)
    {
        
        List<string> firstfiles = CleanupLoops.CLean(Directory.EnumerateFiles(MainViewmodel.Default.Copyfromtext,"*").ToArray(),IOC.Default.GetService<MainViewmodel>().ignorefiles.ToArray()).ToList();
        foreach (var file in firstfiles)
        {
            try
            {
                File.Copy(file,!zip ?Path.Combine(MainViewmodel.Default.Copytotext, GetName(file)):Path.Combine(TempFolder, GetName(file)),overwrite: IOC.Default.GetService<Settings>().Overrite);
            }
            catch (IOException e)
            {
                continue;
            }
        }

        List<string> folders = CleanupLoops.CLean(Directory.GetDirectories(MainViewmodel.Default.Copyfromtext,"*",SearchOption.AllDirectories),IOC.Default.GetService<MainViewmodel>().ignorefolder.ToArray()).ToList();
        
        foreach (var folder in folders)
        {
            var a = FolderPath(folder,zip);
            Directory.CreateDirectory(a);
            List<string> files = CleanupLoops.CLean(Directory.EnumerateFiles(folder).ToArray(),IOC.Default.GetService<MainViewmodel>().ignorefiles.ToArray()).ToList();
            foreach (var file in files)
            {
                try
                {
                    File.Copy(file, Path.Combine(a,GetName(file)), overwrite: IOC.Default.GetService<Settings>().Overrite);
                }
                catch (IOException e)
                {
                    continue;
                }

                IOC.Default.GetService<IProgressBarService>().Progress();
            }
        }
    }
    private static async Task White(bool zip)
    {
        List<string> firstfiles = CleanupLoops.CLeanWhite(Directory.EnumerateFiles(MainViewmodel.Default.Copyfromtext,"*").ToArray(),IOC.Default.GetService<MainViewmodel>().ignorefiles.ToArray()).ToList();
        foreach (var file in firstfiles)
        {
            try
            {
                File.Copy(file, zip == false ? Path.Combine(MainViewmodel.Default.Copytotext,GetName(file)):Path.Combine(TempFolder,GetName(file)), overwrite: IOC.Default.GetService<Settings>().Overrite);
                IOC.Default.GetService<IProgressBarService>().Progress();
            }
            catch (IOException e)
            {
                continue;
            }
        }

        List<string> folders = CleanupLoops.CLeanWhite(Directory.EnumerateDirectories(MainViewmodel.Default.Copyfromtext,"*").ToArray(),IOC.Default.GetService<MainViewmodel>().ignorefolder.ToArray()).ToList();
        foreach (var folder in folders)
        {
            Directory.CreateDirectory(Path.Combine(FolderPath(folder,zip)));
            List<string> files = CleanupLoops.CLeanWhite(Directory.EnumerateFiles(folder).ToArray(),IOC.Default.GetService<MainViewmodel>().ignorefiles.ToArray()).ToList();
            foreach (var file in files)
            {
                try
                {
                    File.Copy(file,Path.Combine(FolderPath(folder,zip),GetName(file)), overwrite: IOC.Default.GetService<Settings>().Overrite);
                }
                catch (IOException e)
                {
                    continue;
                }
            }
            IOC.Default.GetService<IProgressBarService>().Progress();
        }
    }


    public static string GetName(string path) => File.Exists(path) ? Path.GetFileName(path) : new DirectoryInfo(path).Name;

    public static string FolderPath(string folder, bool zip)
    {
        var a = folder.Replace(MainViewmodel.Default.Copyfromtext, zip ==false?MainViewmodel.Default.Copytotext:TempFolder);
        return a;
    }
    
}