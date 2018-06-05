using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using MaterialDesignColors;
using Common;

namespace MvvmLight1.ViewModel
{
    public class PaletteSelectorViewModel:ViewModelBase
    {
        public PaletteSelectorViewModel()
        {
            Swatches = new SwatchesProvider().Swatches;            
        }

        public ICommand ToggleBaseCommand { get; } = new AnotherCommandImplementation(o => ApplyBase((bool)o));

        private static void ApplyBase(bool isDark)
        {
            new PaletteHelper().SetLightDark(isDark);
        }

        public IEnumerable<Swatch> Swatches { get; }

        public ICommand ApplyPrimaryCommand { get; } = new AnotherCommandImplementation(o => ApplyPrimary((Swatch)o));

        private static void ApplyPrimary(Swatch swatch)
        {
            new PaletteHelper().ReplacePrimaryColor(swatch);
        }

        public ICommand ApplyAccentCommand { get; } = new AnotherCommandImplementation(o => ApplyAccent((Swatch)o));
        
        private static void ApplyAccent(Swatch swatch)
        {
            new PaletteHelper().ReplaceAccentColor(swatch);
        }
        
        private Dictionary<string, string> languages;
        public Dictionary<string, string> Languages
        {
            get
            {
                return languages ?? (languages = SR.GetLanguages());
            }
        }

        public string SelectLanguage
        {
            get { return languages[SR.GetString("Language")]; }
            set
            {
                SR.ChangeLanguageFile(value);
            }
        }
    }
}
