using System;
using System.Windows;
using System.Windows.Controls;

namespace SPR.Client.Views.Buttons;

public class NavigationButton : ListViewItem
{
    static NavigationButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationButton), new FrameworkPropertyMetadata(typeof(NavigationButton)));
    }

    public Uri Navlink
    {
        get { return (Uri)GetValue(NavlinkProperty); }
        set { SetValue(NavlinkProperty, value); }
    }
    public static readonly DependencyProperty NavlinkProperty =  DependencyProperty.Register("Navlink", typeof(Uri), typeof(NavigationButton), new PropertyMetadata(null));

}