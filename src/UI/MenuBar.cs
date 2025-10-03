using EnvCraft.src.Models;
using EnvCraft.src.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace EnvCraft.src.UI
{
    /// <summary>
    /// Menu bar untuk tampilan menu bar
    /// </summary>
    public static class MenuBarView
    {
        public static MenuBar Create(List<Item> listItem, EnvListView envListView, EnvFormView envFormView, Action<string> onFileOpened)
        {
            return new MenuBar(new MenuBarItem[]
            {
                new MenuBarItem("_File", new MenuItem[]
                {
                    new MenuItem("_Open", "Open a file", () =>
                    {
                        var d = new OpenDialog("Open File", "Choose a file to open")
                        {
                            CanChooseDirectories = false,
                            AllowsMultipleSelection = false
                        };

                        Application.Run(d);

                        if (!d.Canceled && d.FilePaths.Count > 0)
                        {
                            string path = d.FilePaths[0];
                            Util.LoadEnvFile(path, listItem);
                            envListView.Refresh();
                            onFileOpened(path);
                        }
                    }),
                    new MenuItem("_Exit", "Exit the app", () => Application.RequestStop())
                }),
                new MenuBarItem("_Help", new MenuItem[]
                {
                    new MenuItem("_About", "", () =>
                    {
                        MessageBox.Query("About", "EnvCraft v1.0\ncreated by Aji Mustofa (@pepega90)", "OK");
                    })
                })
            });
        }
    }
}
