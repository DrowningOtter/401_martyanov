using System.Windows.Input;

namespace OptimalSquaresUI;

public static class MyCommands
{
    public static RoutedCommand CalcStartCommand = new RoutedCommand("Check if can start calculations", typeof(MyCommands));
    public static RoutedCommand CalcStopCommand = new RoutedCommand("Stop calculations", typeof(MyCommands));
}