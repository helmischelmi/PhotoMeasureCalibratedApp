using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using PhotoMeasureCalibrated.Models;

namespace PhotoMeasureCalibrated.ViewModels;

internal class MainViewModel: ObservableObject
{
    public MainModel Model { get; }

    public MainViewModel(MainModel model)
    {
        Model = model;
    }


}