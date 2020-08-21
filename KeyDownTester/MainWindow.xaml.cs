using KeyDownTester.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KeyDownTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // need to setup the global hook. this can go in
            // App.xaml.cs's constructor if you want
            HotkeysManager.SetupSystemHook();

            // You can create a globalhotkey object and pass it like so
            HotkeysManager.AddHotkey(new GlobalHotkey(ModifierKeys.Control, Key.S, () => { AddToList("Ctrl+S Fired"); }));

            // or do it like this. both end up doing the same thing, but this is probably simpler.
            HotkeysManager.AddHotkey(ModifierKeys.Control, Key.A, () => { AddToList("Ctrl+A Fired"); });
            HotkeysManager.AddHotkey(ModifierKeys.Control, Key.D, () => { AddToList("Ctrl+D Fired"); });
            HotkeysManager.AddHotkey(ModifierKeys.Shift, Key.D, () => { AddToList("Shift+D Fired"); });

            Closing += MainWindow_Closing;
        }

        public void AddToList(string content)
        {
            hotkeysFired.Items.Add(content);
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Need to shutdown the hook. idk what happens if
            // you dont, but it might cause a memory leak.
            HotkeysManager.ShutdownSystemHook();
        }
    }
}
