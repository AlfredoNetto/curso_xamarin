using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace PodePedir.Model
{
    public class ByteToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                ImageSource objImageSource;
                if (value != null)
                {
                    byte[] byteImageData = (byte[])value;
                    objImageSource = ImageSource.FromStream(() => new MemoryStream(byteImageData));
                }
                else
                {
                    objImageSource = null;
                }
                return objImageSource;
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
