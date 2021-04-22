// -------------------------------------------------------------------------------------------------
// SnazzyWpfBorders - © Copyright 2021 - Jam-Es.com
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SnazzyWpfBorders.Components
{
    /// <summary>
    /// Interaction logic for BorderDash.xaml
    /// </summary>
    public partial class BorderDash : UserControl
    {
        private static readonly DependencyProperty _Duration = DependencyProperty.Register("Duration", typeof(int), typeof(BorderDash), new PropertyMetadata(200, (DependencyObject d, DependencyPropertyChangedEventArgs eventArgs) => { ((BorderDash)d).Duration = (int)eventArgs.NewValue; }));
        private static readonly DependencyProperty _BColor = DependencyProperty.Register("BColor", typeof(Brush), typeof(BorderDash), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(174, 234, 0)), (DependencyObject d, DependencyPropertyChangedEventArgs eventArgs) => { ((BorderDash)d).BColor = (Brush)eventArgs.NewValue; }));

        private Storyboard storyboard;

        public BorderDash()
        {
            InitializeComponent();

            Loaded += (object s, RoutedEventArgs e) => InitSbs();
        }

        public int Duration
        {
            get
            {
                return (int)GetValue(_Duration);
            }

            set
            {
                SetValue(_Duration, value);
            }
        }

        public Brush BColor
        {
            get
            {
                return (Brush)GetValue(_BColor);
            }

            set
            {
                SetValue(_BColor, value);
            }
        }

        public void InitSbs()
        {
            GridLengthAnimation startMoveT = CreateGridLengthAnim("TopStart", 0, 1, true, 0, Duration);
            GridLengthAnimation endMoveT = CreateGridLengthAnim("TopEnd", 1, 0, true, 0, Duration);
            GridLengthAnimation startMoveR = CreateGridLengthAnim("RightStart", 0, 1, false, 0, Duration);
            GridLengthAnimation endMoveR = CreateGridLengthAnim("RightEnd", 1, 0, false, 0, Duration);
            GridLengthAnimation startMoveB = CreateGridLengthAnim("BottomSuperStart", 1, 0, true, 0, Duration);
            GridLengthAnimation endMoveB = CreateGridLengthAnim("BottomPenEnd", 0, 1, true, 0, Duration);
            GridLengthAnimation startMoveL = CreateGridLengthAnim("LeftSuperStart", 1, 0, false, 0, Duration);
            GridLengthAnimation endMoveL = CreateGridLengthAnim("LeftPenEnd", 0, 1, false, 0, Duration);
            GridLengthAnimation startSubMoveT = CreateGridLengthAnim("TopSuperStart", 0, 1, true, Duration, Duration);
            GridLengthAnimation endPenMoveT = CreateGridLengthAnim("TopPenEnd", 1, 0, true, Duration, Duration);
            GridLengthAnimation startSubMoveR = CreateGridLengthAnim("RightSuperStart", 0, 1, false, Duration, Duration);
            GridLengthAnimation endPenMoveR = CreateGridLengthAnim("RightPenEnd", 1, 0, false, Duration, Duration);
            GridLengthAnimation startSubMoveB = CreateGridLengthAnim("BottomStart", 1, 0, true, Duration, Duration);
            GridLengthAnimation endPenMoveB = CreateGridLengthAnim("BottomEnd", 0, 1, true, Duration, Duration);
            GridLengthAnimation startSubMoveL = CreateGridLengthAnim("LeftStart", 1, 0, false, Duration, Duration);
            GridLengthAnimation endPenMoveL = CreateGridLengthAnim("LeftEnd", 0, 1, false, Duration, Duration);

            storyboard = new Storyboard();
            storyboard.Children.Add(startMoveT);
            storyboard.Children.Add(endMoveT);
            storyboard.Children.Add(startMoveR);
            storyboard.Children.Add(endMoveR);
            storyboard.Children.Add(startMoveB);
            storyboard.Children.Add(endMoveB);
            storyboard.Children.Add(startMoveL);
            storyboard.Children.Add(endMoveL);
            storyboard.Children.Add(startSubMoveT);
            storyboard.Children.Add(endPenMoveT);
            storyboard.Children.Add(startSubMoveR);
            storyboard.Children.Add(endPenMoveR);
            storyboard.Children.Add(startSubMoveB);
            storyboard.Children.Add(endPenMoveB);
            storyboard.Children.Add(startSubMoveL);
            storyboard.Children.Add(endPenMoveL);
            storyboard.RepeatBehavior = RepeatBehavior.Forever;
        }

        private GridLengthAnimation CreateGridLengthAnim(string name, double from, double to, bool isCol, int beginTime, int duration)
        {
            GridLengthAnimation anim = new GridLengthAnimation();
            anim.From = new GridLength(from, GridUnitType.Star);
            anim.To = new GridLength(to, GridUnitType.Star);
            anim.Duration = new Duration(new System.TimeSpan(0, 0, 0, 0, duration));
            anim.BeginTime = new System.TimeSpan(0, 0, 0, 0, beginTime);
            Storyboard.SetTargetName(anim, name);
            if (isCol)
            {
                Storyboard.SetTargetProperty(anim, new PropertyPath(ColumnDefinition.WidthProperty));
            }
            else
            {
                Storyboard.SetTargetProperty(anim, new PropertyPath(RowDefinition.HeightProperty));
            }

            return anim;
        }

        public void Start()
        {
            RootObj.Visibility = Visibility.Visible;
            storyboard.Begin(this, true);
        }

        public void Stop()
        {
            RootObj.Visibility = Visibility.Collapsed;
            storyboard.Stop(this);
        }
    }
}
