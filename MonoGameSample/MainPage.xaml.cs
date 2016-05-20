using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MonoGameSample.Resources;
using MonoGame.Framework.WindowsPhone;

namespace MonoGameSample
{
    public partial class MainPage : PhoneApplicationPage
    {
        private Game1 game;

        public MainPage()
        {
            InitializeComponent();
            game = XamlGame<Game1>.Create("", this);
        }
    }
}