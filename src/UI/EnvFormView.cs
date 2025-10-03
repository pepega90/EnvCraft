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
    /// Form view, untuk input key dan value
    /// </summary>
    public class EnvFormView : FrameView
    {
        private readonly List<Item> listItem;
        private readonly Func<string> getFilePath;
        private readonly EnvListView envListView;

        private TextField keyField;
        private TextField valueField;
        private ComboBox dropDownEnvList;

        public EnvFormView(List<Item> items, Func<string> envFile, EnvListView listView) : base("Env Form")
        {
            listItem = items;
            getFilePath = envFile;
            envListView = listView;

            X = Pos.Right(listView);
            Y = 0;
            Width = Dim.Fill();
            Height = Dim.Fill();

            InitForm();
        }

        private void InitForm()
        {
            keyField = new TextField("") { X = 1, Y = 1, Width = 30 };
            valueField = new TextField("") { X = 1, Y = Pos.Bottom(keyField) + 1, Width = 30 };
            dropDownEnvList = new ComboBox() { X = 1, Y = Pos.Bottom(valueField) + 1, Width = 30, Height = 5 };

            var tambahBtn = new Button("Tambah/Update")
            {
                X = Pos.Right(keyField) + 1,
                Y = keyField.Y
            };
            tambahBtn.Clicked += AddOrUpdate;

            var hapusBtn = new Button("Hapus")
            {
                X = Pos.Right(dropDownEnvList) + 1,
                Y = dropDownEnvList.Y
            };
            hapusBtn.Clicked += DeleteItem;

            var saveBtn = new Button("Save .env")
            {
                X = dropDownEnvList.X,
                Y = dropDownEnvList.Y + 2
            };
            saveBtn.Clicked += SaveFile;

            Add(
                new Label("Key:") { X = 1, Y = 0 },
                keyField,
                new Label("Value:") { X = 1, Y = Pos.Bottom(keyField) },
                valueField,
                new Label("Env to Delete:") { X = 1, Y = Pos.Bottom(valueField) },
                dropDownEnvList,
                tambahBtn,
                hapusBtn,
                saveBtn
            );

            RefreshDropDown();
        }

        private void AddOrUpdate()
        {
            var k = keyField.Text.ToString();
            var v = valueField.Text.ToString();

            if (string.IsNullOrWhiteSpace(k) || string.IsNullOrWhiteSpace(v))
                return;

            int idx = listItem.FindIndex(x => x.Key == k);
            if (idx >= 0)
                listItem[idx].Value = v;
            else
                listItem.Add(new Item { Key = k, Value = v });

            keyField.Text = "";
            valueField.Text = "";

            RefreshDropDown();
            envListView.Refresh();
        }

        private void DeleteItem()
        {
            int idx = dropDownEnvList.SelectedItem;
            if (idx <= 0) return;

            listItem.RemoveAt(idx - 1);

            RefreshDropDown();
            envListView.Refresh();
        }

        private void SaveFile()
        {
            string filePath = getFilePath();
            Util.SaveEnvFile(filePath, listItem);
            MessageBox.Query("Saved", "✅ .env file saved!", "Ok");
        }

        private void RefreshDropDown()
        {
            var items = new List<string> { "-- Select Key --" };
            items.AddRange(listItem.Select(v => v.Key));
            dropDownEnvList.SetSource(items);
            dropDownEnvList.SelectedItem = 0;
        }
    }
}
