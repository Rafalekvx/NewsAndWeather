using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsAndWeather.ViewModels;

namespace NewsAndWeather.Views;

public partial class NewsPage : ContentPage
{
    private NewsPageViewModel _viewmodel;
    public NewsPage()
    {
        InitializeComponent();
        _viewmodel = new NewsPageViewModel();
        this.BindingContext = _viewmodel;
        LoadAfterConstruction();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    private async void LoadAfterConstruction()
    {
        _viewmodel.IsBusy = true;
        _viewmodel.GetList();
        _viewmodel.GetCategoriesList();
        //only set the binding of the CollectionView after loading completed
        PostsListView.SetBinding(ItemsView.ItemsSourceProperty, "Posts");
        CategoriesList.SetBinding(ItemsView.ItemsSourceProperty, "CategoriesList");

        _viewmodel.IsBusy = false;
    }
    
    
    private void Button_OnClicked(object sender, EventArgs e)
    {
        base.OnAppearing();
        _viewmodel.OnItemSelected(_viewmodel.LastestPost);
    }
}