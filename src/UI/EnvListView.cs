using EnvCraft.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace EnvCraft.src.Views
{
    /// <summary>
    /// List env view, untuk menampilkan env list view di kiri layar
    /// </summary>
    public class EnvListView : FrameView
    {
        public ListView ListEnv { get; set; }
        private readonly List<Item> listItems;

        public EnvListView(List<Item> items) : base("Env Data")
        {
            listItems = items;
            X = 0;
            Y = 0;
            Width = Dim.Percent(50);
            Height = Dim.Fill();

            ListEnv = new ListView()
            {
                Width = Dim.Fill(),
                Height = Dim.Fill(),
            };

            Add(ListEnv);
        }

        public void Refresh()
        {
            var keyValue = listItems
                .Select((item, idx) => $"{idx + 1}.{item.Key}={item.Value}")
                .ToList();

            ListEnv.SetSource(keyValue);
        }
    }
}
