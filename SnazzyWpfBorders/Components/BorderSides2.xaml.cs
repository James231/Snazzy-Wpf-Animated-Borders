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
    /// Interaction logic for BorderSides2.xaml
    /// </summary>
    public partial class BorderSides2 : UserControl
    {
        private static readonly DependencyProperty _EdgeDuration = DependencyProperty.Register("EdgeDuration", typeof(int), typeof(BorderSides2), new PropertyMetadata(500, (DependencyObject d, DependencyPropertyChangedEventArgs eventArgs) => { ((BorderSides2)d).EdgeDuration = (int)eventArgs.NewValue; }));
        private static readonly DependencyProperty _FadeOutDuration = DependencyProperty.Register("FadeOutDuration", typeof(int), typeof(BorderSides2), new PropertyMetadata(200, (DependencyObject d, DependencyPropertyChangedEventArgs eventArgs) => { ((BorderSides2)d).FadeOutDuration = (int)eventArgs.NewValue; }));
        private static readonly DependencyProperty _FadeOutDelay = DependencyProperty.Register("FadeOutDelay", typeof(int), typeof(BorderSides2), new PropertyMetadata(400, (DependencyObject d, DependencyPropertyChangedEventArgs eventArgs) => { ((BorderSides2)d).FadeOutDelay = (int)eventArgs.NewValue; }));
        private static readonly DependencyProperty _BColor = DependencyProperty.Register("BColor", typeof(Brush), typeof(BorderSides2), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(174, 234, 0)), (DependencyObject d, DependencyPropertyChangedEventArgs eventArgs) => { ((BorderSides2)d).BColor = (Brush)eventArgs.NewValue; }));

        private Storyboard topEdgeSb;
        private Storyboard leftEdgeSb;
        private Storyboard bottomEdgeSb;
        private Storyboard rightEdgeSb;

        public BorderSides2()
        {
            InitializeComponent();

            Loaded += (object s, RoutedEventArgs e) => InitSbs();
        }

        public int EdgeDuration
        {
            get
            {
                return (int)GetValue(_EdgeDuration);
            }

            set
            {
                SetValue(_EdgeDuration, value);
            }
        }

        public int FadeOutDuration
        {
            get
            {
                return (int)GetValue(_FadeOutDuration);
            }

            set
            {
                SetValue(_FadeOutDuration, value);
            }
        }

        public int FadeOutDelay
        {
            get
            {
                return (int)GetValue(_FadeOutDelay);
            }

            set
            {
                SetValue(_FadeOutDelay, value);
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

        private void InitSbs()
        {
            topEdgeSb = CreateSb("TopShrink1", "TopShrink2", "TopExpand", "TopEdge", true, 0);
            rightEdgeSb = CreateSb("RightShrink1", "RightShrink2", "RightExpand", "RightEdge", false, 0);
            bottomEdgeSb = CreateSb("BottomShrink1", "BottomShrink2", "BottomExpand", "BottomEdge", true, 0);
            leftEdgeSb = CreateSb("LeftShrink1", "LeftShrink2", "LeftExpand", "LeftEdge", false, 0);
        }

        private Storyboard CreateSb(string shinkOneName, string shrinkTwoName, string expandName, string barName, bool isCol, int beginTime)
        {
            GridLengthAnimation expandAnim = CreateGridLengthAnim(expandName, 0, 1, isCol, beginTime, EdgeDuration);
            GridLengthAnimation shrinkAnimOne = CreateGridLengthAnim(shinkOneName, 1, 0, isCol, beginTime, EdgeDuration);
            GridLengthAnimation shrinkAnimTwo = CreateGridLengthAnim(shrinkTwoName, 1, 0, isCol, beginTime, EdgeDuration);
            DoubleAnimation fadeOutAnim = CreateFadeOutAnim(barName, EdgeDuration + FadeOutDelay, FadeOutDuration, false);

            int resetTime = (EdgeDuration + FadeOutDelay) + FadeOutDuration;
            GridLengthAnimation resetExpandAnim = CreateGridLengthAnim(expandName, 1, 0, isCol, resetTime, 0);
            GridLengthAnimation resetShrinkAnimOne = CreateGridLengthAnim(shinkOneName, 0, 1, isCol, resetTime, 0);
            GridLengthAnimation resetShrinkAnimTwo = CreateGridLengthAnim(shrinkTwoName, 0, 1, isCol, resetTime, 0);
            DoubleAnimation resetFadeOutAnim = CreateFadeOutAnim(barName, resetTime, 0, true);

            Storyboard sb = new Storyboard();
            sb.Children.Add(expandAnim);
            sb.Children.Add(shrinkAnimOne);
            sb.Children.Add(shrinkAnimTwo);
            sb.Children.Add(fadeOutAnim);
            sb.Children.Add(resetExpandAnim);
            sb.Children.Add(resetShrinkAnimOne);
            sb.Children.Add(resetShrinkAnimTwo);
            sb.Children.Add(resetFadeOutAnim);
            return sb;
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

        private DoubleAnimation CreateFadeOutAnim(string name, int beginTime, int duration, bool rev)
        {
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = rev ? 0 : 1;
            anim.To = rev ? 1 : 0;
            anim.Duration = new Duration(new System.TimeSpan(0, 0, 0, 0, duration));
            anim.BeginTime = new System.TimeSpan(0, 0, 0, 0, beginTime);
            Storyboard.SetTargetName(anim, name);
            Storyboard.SetTargetProperty(anim, new PropertyPath(UIElement.OpacityProperty));
            return anim;
        }

        public void Start()
        {
            topEdgeSb.Begin(this, true);
            leftEdgeSb.Begin(this, true);
            bottomEdgeSb.Begin(this, true);
            rightEdgeSb.Begin(this, true);
        }
    }
}
