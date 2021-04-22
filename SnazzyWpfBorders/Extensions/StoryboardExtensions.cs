// -------------------------------------------------------------------------------------------------
// SnazzyWpfBorders - © Copyright 2021 - Jam-Es.com
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System.Windows.Media.Animation;

namespace SnazzyWpfBorders.Extensions
{
    public static class StoryboardExtensions
    {
        public static void AddDelayToChildren(this Storyboard sb, int delay)
        {
            foreach (Timeline t in sb.Children)
            {
                t.BeginTime += new System.TimeSpan(0, 0, 0, 0, delay);
            }
        }
    }
}
