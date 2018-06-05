using Common;
using MaterialDesignThemes.Wpf;
using System;

namespace MvvmLight1.Model
{
    public class DataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to connect to the actual data service

            var item = new DataItem(new DemoItem[] 
            {
                 new DemoItem(SR.GetString("Home"), new View.Home(), PackIconKind.Home),
                 new DemoItem(SR.GetString("Palette"), new View.PaletteSelector(), PackIconKind.Palette),
                 new DemoItem(SR.GetString("ConvertFile"), new View.ConvertFile(), PackIconKind.Key),
                 new DemoItem(SR.GetString("Vision"), new View.Vision(), PackIconKind.Video),
            });
            callback(item, null);
        }
    }
}