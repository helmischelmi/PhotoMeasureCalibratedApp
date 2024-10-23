
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using PhotoMeasureCalibrated.Models;

namespace PhotoMeasureCalibrated.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public MainModel Model { get; }


    [ObservableProperty]
    private string imageFolderPath;



    public MainViewModel(MainModel model)
    {
        Model = model;
    }

    [RelayCommand]
    public async Task SelectFolder()
    {
        var dialog = new OpenFolderDialog();

        dialog.Title = "Select Folder with image";
        dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Favorites);

        if (dialog.ShowDialog() ==true && !string.IsNullOrWhiteSpace(dialog.FolderName))
        {
            ImageFolderPath = dialog.FolderName;
        }
    }


}