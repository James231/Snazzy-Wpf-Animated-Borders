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
    /// Interaction logic for BorderTopDash.xaml
    /// </summary>
    public partial class BorderTopDash : UserControl
    {
        private static readonly DependencyProperty _Duration = DependencyProperty.Register("Duration", typeof(int), typeof(BorderTopDash), new PropertyMetadata(200, (DependencyObject d, DependencyPropertyChangedEventArgs eventArgs) => { ((BorderTopDash)d).Duration = (int)eventArgs.NewValue; }));
        private static readonly DependencyProperty _BColor = DependencyProperty.Register("BColor", typeof(Brush), typeof(BorderTopDash), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(174, 234, 0)), (DependencyObject d, DependencyPropertyChangedEventArgs eventArgs) => { ((BorderTopDash)d).BColor = (Brush)eventArgs.NewValue; }));

        private Storyboard storyboard;

        public BorderTopDash()
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
            GridLengthAnimation startMove = CreateGridLengthAnim("TopStart", 0, 1, true, 0, Duration);
            GridLengthAnimation endMove = CreateGridLengthAnim("TopEnd", 1, 0, true, 0, Duration);

            GridLengthAnimation startSubMove = CreateGridLengthAnim("TopSuperStart", 0, 1, true, Duration, Duration);
            GridLengthAnimation endPenMove = CreateGridLengthAnim("TopPenEnd", 1, 0, true, Duration, Duration);

            storyboard = new Storyboard();
            storyboard.Children.Add(startMove);
            storyboard.Children.Add(endMove);
            storyboard.Children.Add(startSubMove);
            storyboard.Children.Add(endPenMove);
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
