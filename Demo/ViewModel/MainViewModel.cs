
using Common;
using MaterialDesignThemes.Wpf;
using MvvmLight1.Model;

namespace MvvmLight1.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            DemoItems = new DemoItem[]
            {
                 new DemoItem("Home", new View.Home(), PackIconKind.Home),
                 new DemoItem("Palette", new View.PaletteSelector(), PackIconKind.Palette),
                 new DemoItem("ConvertFile", new View.ConvertFile(), PackIconKind.Key),
                 new DemoItem("Vision", new View.Vision(), PackIconKind.Video),
            };
            ChangeUI();
            SR.LanguageChanged += (s, e) => ChangeUI();
        }

        #region Singleton
        //private static readonly object SyncObject = new object();
        //private static MainViewModel instance;
        //private MainViewModel()
        //{
        //    DemoItems = new DemoItem[]
        //    {
        //         new DemoItem("Home", new View.Home(), PackIconKind.Home),
        //         new DemoItem("Palette", new View.PaletteSelector(), PackIconKind.Palette),
        //         new DemoItem("ConvertFile", new View.ConvertFile(), PackIconKind.Key),
        //         new DemoItem("Vision", new View.Vision(), PackIconKind.Video),
        //    };
        //    ChangeUI();
        //    SR.LanguageChanged += (s, e) => ChangeUI();
        //}



        //public static MainViewModel Instance
        //{
        //    get
        //    {
        //        lock (SyncObject)
        //        {
        //            return instance ?? (instance = new MainViewModel());
        //        }
        //    }
        //}

        #endregion

        private DemoItem[] demoItems;
        public DemoItem[] DemoItems
        {
            get
            {
                return demoItems;
            }

            set
            {
                SetProperty(ref demoItems, value);
            }
        }

        private void ChangeUI()
        {
            foreach (var item in DemoItems)
            {
                item.DisplayName = SR.GetString(item.Name);
            }
        }
    }
}