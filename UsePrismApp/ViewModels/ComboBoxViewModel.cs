using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsePrismApp.ViewModels
{
    public sealed class ComboBoxViewModel
    {
        public ComboBoxViewModel(int value, string displayValue)
        {
            Value = value;

            DisplayValue = displayValue;
        }

        public int Value { get; }

        public string DisplayValue { get; }
    }
}
