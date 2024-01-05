using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using NewsAndWeather.ViewModels.PopUps;
using SelectionChangedEventArgs = Syncfusion.Maui.Inputs.SelectionChangedEventArgs;

namespace NewsAndWeather.PopUps;

public partial class SettingsPopUp : Popup
{
    private SettingsPopUpViewModel _viewmodel;
    public SettingsPopUp()
    {
        InitializeComponent();
        var inputView = LocationPicker.Children[1] as Entry;
        inputView.HorizontalTextAlignment = TextAlignment.Center;
        //LocationPicker.SelectedIndex = Preferences.Default.Get("Location", 0);
        _viewmodel = new SettingsPopUpViewModel();
        this.BindingContext = _viewmodel;
        SelectedLabel.Text = Preferences.Default.Get("Location", "nothing").ToString();
    }

    private void LocationPicker_OnSelectedIndexChanged(object sender, SelectionChangedEventArgs e)
    {
        _viewmodel.ChangeLocation();
    }

    private void CancelButton_OnClicked(object sender, EventArgs e)
    {
        this.Close();
    }
}