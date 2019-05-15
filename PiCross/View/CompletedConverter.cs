using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiCross;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Cells;

namespace View
{
    public class CompletedConverter : IValueConverter
    {

        public string completed { get; set; }
        public string notcompleted { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool checkCompleted = Boolean.Parse(value.ToString());
            return checkCompleted ? completed : notcompleted;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}