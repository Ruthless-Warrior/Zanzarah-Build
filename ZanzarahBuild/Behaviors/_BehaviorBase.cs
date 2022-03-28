using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace ZanzarahBuild.Behaviors
{
    public abstract class BehaviorBase<T>
        : Behavior<T> where T : DependencyObject { }
}
