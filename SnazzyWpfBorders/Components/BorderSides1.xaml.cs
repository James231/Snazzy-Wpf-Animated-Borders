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
    /// Interaction logic for BorderSides1.xaml
    /// </summary>
    public partial class BorderSides1 : UserControl
    {
        private static readonly DependencyProperty _EdgeDuration = DependencyProperty.Register("EdgeDuration", typeof(int), typeof(BorderSides1), new PropertyMetadata(200, (DependencyObject d, DependencyPropertyChangedEventArgs eventArgs) => { ((BorderSides1)d).EdgeDuration = (int)eventArgs.NewValue; }));
        private static readonly DependencyProperty _FadeOutDuration = DependencyProperty.Register("FadeOutDuration", typeof(int), typeof(BorderSides1), new PropertyMetadata(200, (DependencyObject d, DependencyPropertyChangedEventArgs eventArgs) => { ((BorderSides1)d).FadeOutDuration = (int)eventArgs.NewValue; }));
        private static readonly DependencyProperty _FadeOutDelay = DependencyProperty.Register("FadeOutDelay", typeof(int), typeof(BorderSides1), new PropertyMetadata(300, (DependencyObject d, DependencyPropertyChangedEventArgs eventArgs) => { ((BorderSides1)d).FadeOutDelay = (int)eventArgs.NewValue; }));
        private static readonly DependencyProperty _BColor = DependencyProperty.Register("BColor", typeof(Brush), typeof(BorderSides1), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(174, 234, 0)), (DependencyObject d, DependencyPropertyChangedEventArgs eventArgs) => { ((BorderSides1)d).BColor = (Brush)eventArgs.NewValue; }));

        private Storyboard topEdgeSb;
        private Storyboard leftEdgeSb;
        private Storyboard bottomEdgeSb;
        private Storyboard rightEdgeSb;

        public BorderSides1()
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
            topEdgeSb = CreateSb("TopEdgeCol1", "TopEdgeCol2", "TopEdge", true, 0);
            rightEdgeSb = CreateSb("RightEdgeCol1", "RightEdgeCol2", "RightEdge", false, 0);
            bottomEdgeSb = CreateSb("BottomEdgeCol1", "BottomEdgeCol2", "BottomEdge", true, 0);
            leftEdgeSb = CreateSb("LeftEdgeCol1", "LeftEdgeCol2", "LeftEdge", false, 0);
        }

        private Storyboard CreateSb(string colNameOne, string colNameTwo, string barName, bool isCol, int beginTime)
        {
            GridLengthAnimation colOneAnim = CreateGridLengthAnim(colNameOne, 0, 1, isCol, beginTime, EdgeDuration);
            GridLengthAnimation colTwoAnim = CreateGridLengthAnim(colNameTwo, 1, 0, isCol, beginTime, EdgeDuration);
            DoubleAnimation fadeOutAnim = CreateFadeOutAnim(barName, EdgeDuration + FadeOutDelay, FadeOutDuration, false);

            int resetTime = (EdgeDuration + FadeOutDelay) + FadeOutDuration;
            GridLengthAnimation resetColOneAnim = CreateGridLengthAnim(colNameOne, 1, 0, isCol, resetTime, 0);
            GridLengthAnimation resetColTwoAnim = CreateGridLengthAnim(colNameTwo, 0, 1, isCol, resetTime, 0);
            DoubleAnimation resetFadeOutAnim = CreateFadeOutAnim(barName, resetTime, 0, true);

            Storyboard sb = new Storyboard();
            sb.Children.Add(colOneAnim);
            sb.Children.Add(colTwoAnim);
            sb.Children.Add(fadeOutAnim);
            sb.Children.Add(resetColOneAnim);
            sb.Children.Add(resetColTwoAnim);
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
