// -------------------------------------------------------------------------------------------------
// SnazzyWpfBorders - © Copyright 2021 - Jam-Es.com
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System.Windows;
using System.Windows.Media.Animation;
using SnazzyWpfBorders.Extensions;

namespace SnazzyWpfBorders
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool warningFlashEnabled = false;
        private bool topDashEnabled = false;
        private bool dashEnabled = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoopButtonClick(object sender, RoutedEventArgs e)
        {
            BorderLoop.Start();
        }

        private void Sides1ButtonClick(object sender, RoutedEventArgs e)
        {
            BorderSides1.Start();
        }

        private void Sides2ButtonClick(object sender, RoutedEventArgs e)
        {
            BorderSides2.Start();
        }

        private void FlashButtonClick(object sender, RoutedEventArgs e)
        {
            BorderFade.Start();
        }

        private void WarningFlashToggleClick(object sender, RoutedEventArgs e)
        {
            if (warningFlashEnabled)
            {
                // Stop the flash animation
                BorderWarningFlash.Storyboard.Stop(BorderWarningFlash);
            }
            else
            {
                // Add gap between flashes
                BorderWarningFlash.Storyboard.AddDelayToChildren(80);

                // Set it to repeat forever
                BorderWarningFlash.Storyboard.RepeatBehavior = RepeatBehavior.Forever;

                // Start flashing
                BorderWarningFlash.Start();
            }

            warningFlashEnabled = !warningFlashEnabled;
        }

        private void TopDashToggleClick(object sender, RoutedEventArgs e)
        {
            if (topDashEnabled)
            {
                BorderTopDash.Stop();
            }
            else
            {
                BorderTopDash.Start();
            }

            topDashEnabled = !topDashEnabled;
        }

        private void DashToggleClick(object sender, RoutedEventArgs e)
        {
            if (dashEnabled)
            {
                BorderDash.Stop();
            }
            else
            {
                BorderDash.Start();
            }

            dashEnabled = !dashEnabled;
        }
    }
}
