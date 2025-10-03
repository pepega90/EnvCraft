using EnvCraft.src;
using EnvCraft.src.Models;
using EnvCraft.src.UI;
using EnvCraft.src.Views;
using Terminal.Gui;

namespace EnvCraft
{
    internal class Program
    {
        static string fileName = ".env";
        static List<Item> listItem = new();

        static void Main(string[] args)
        {
            Application.Init();
            var top = Application.Top;

            // Main Window
            var win = new Window()
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
            };
            top.Add(win);

            // Left Panel
            var envListView = new EnvListView(listItem);

            // Right Panel
            var envFormView = new EnvFormView(listItem, () => fileName, envListView);

            win.Add(envListView, envFormView);

            // Menu
            var menu = MenuBarView.Create(listItem, envListView, envFormView, (newPath) =>
            {
                fileName = newPath;
            });
            top.Add(menu);

            // Initial Load
            if (File.Exists(fileName))
            {
                Util.LoadEnvFile(fileName, listItem);
            }

            envListView.Refresh();

            Application.Run();
        }
    }
}
