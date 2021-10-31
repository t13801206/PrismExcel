using System;
using System.Windows;

namespace PrismExcel.Core
{
    [Obsolete("列インデックスのために使おうとして断念した")]
    public class SampleAttachedProperties
    {
        public static int GetColmunIndex(DependencyObject obj)
        {
            return (int)obj.GetValue(ColmunIndexProperty);
        }

        public static void SetColmunIndex(DependencyObject obj, int value)
        {
            obj.SetValue(ColmunIndexProperty, value);
        }

        public static readonly DependencyProperty ColmunIndexProperty =
            DependencyProperty.RegisterAttached("ColmunIndex", typeof(int), typeof(SampleAttachedProperties), new PropertyMetadata(0));
    }
}
