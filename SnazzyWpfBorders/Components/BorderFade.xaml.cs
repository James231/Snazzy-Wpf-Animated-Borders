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
    /// Interaction logic for BorderFade.xaml
    /// </summary>
    public partial class BorderFade : UserControl
    {
        private static readonly DependencyProperty _FadeInDuration = DependencyProperty.Register("_FadeInDuration", typeof(int), typeof(BorderFade), new PropertyMetadata(200, (DependencyObject d, DependencyPropertyChangedEventArgs eventArgs) => { ((BorderFade)d).FadeInDuration = (int)eventArgs.NewValue; }));
        private static readonly DependencyProperty _FadeOutDuration = DependencyProperty.Register("FadeOutDuration", typeof(int), typeof(BorderFade), new PropertyMetadata(200, (DependencyObject d, DependencyPropertyChangedEventArgs eventArgs) => { ((BorderFade)d).FadeOutDuration = (int)eventArgs.NewValue; }));
        private static readonly DependencyProperty _FadeOutDelay = DependencyProperty.Register("FadeOutDelay", typeof(int), typeof(BorderFade), new PropertyMetadata(100, (DependencyObject d, DependencyPropertyChangedEventArgs eventArgs) => { ((BorderFade)d).FadeOutDelay = (int)eventArgs.NewValue; }));
        private static readonly DependencyProperty _BColor = DependencyProperty.Register("BColor", typeof(Brush), typeof(BorderFade), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(174, 234, 0)), (DependencyObject d, DependencyPropertyChangedEventArgs eventArgs) => { ((BorderFade)d).BColor = (Brush)eventArgs.NewValue; }));

        public BorderFade()
        {
            InitializeComponent();

            Loaded += (object s, RoutedEventArgs e) => InitSbs();
        }

        public Storyboard Storyboard { get; set; }

        public int FadeInDuration
        {
            get
            {
                return (int)GetValue(_FadeInDuration);
            }

            set
            {
                SetValue(_FadeInDuration, value);
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
            Storyboard topEdgeSb = CreateSb("TopEdge", true, 0);
            Storyboard rightEdgeSb = CreateSb("RightEdge", false, 0);
            Storyboard bottomEdgeSb = CreateSb("BottomEdge", true, 0);
            Storyboard leftEdgeSb = CreateSb("LeftEdge", false, 0);

            Storyboard = new Storyboard();
            Storyboard.Children.Add(topEdgeSb);
            Storyboard.Children.Add(rightEdgeSb);
            Storyboard.Children.Add(bottomEdgeSb);
            Storyboard.Children.Add(leftEdgeSb);
        }

        private Storyboard CreateSb(string barName, bool isCol, int beginTime)
        {
            DoubleAnimation fadeInAnim = CreateFadeOutAnim(barName, 0, FadeInDuration, true);
            DoubleAnimation fadeOutAnim = CreateFadeOutAnim(barName, FadeInDuration + FadeOutDelay, FadeOutDuration, false);

            int resetTime = FadeInDuration + FadeOutDelay + FadeOutDuration;

            Storyboard sb = new Storyboard();
            sb.Children.Add(fadeInAnim);
            sb.Children.Add(fadeOutAnim);
            return sb;
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
            Storyboard.Begin(this, true);
        }
    }
}
