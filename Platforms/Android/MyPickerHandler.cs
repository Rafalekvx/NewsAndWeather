using Android.Graphics.Drawables;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace NewsAndWeather;

public class MyPickerHandler : PickerHandler
{
    protected override void ConnectHandler(MauiPicker platformView)
    {
        base.ConnectHandler(platformView);
        GradientDrawable gd = new GradientDrawable();
        gd.SetColor(global::Android.Graphics.Color.Transparent);
        platformView.SetBackground(gd);
        
    }
}