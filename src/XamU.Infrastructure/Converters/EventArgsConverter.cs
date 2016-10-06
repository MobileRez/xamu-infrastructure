//
// EventArgsConverter.cs
//
// Author:
//       Mark Smith <mark.smith@xamarin.com>
//
// Copyright (c) 2016 Xamarin, Microsoft.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;

namespace XamarinUniversity.Converters
{
    /// <summary>
    /// A custom EventArgsConverter used to convert an EventArg to string.
    /// </summary>
    public class EventArgsConverter : IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Implement this method to convert <paramref name="value" /> to <paramref name="targetType" /> 
        /// by using <paramref name="parameter" /> and <paramref name="culture" />.
        /// </summary>
        /// <param name="value">To be added.</param>
        /// <param name="targetType">To be added.</param>
        /// <param name="parameter">Optional prefix</param>
        /// <param name="culture">Culture</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(PropertyName))
                throw new ArgumentNullException(nameof(PropertyName), $"{nameof(PropertyName)} must be set");

            if (parameter == null)
                return null;

            var theType = parameter.GetType().GetTypeInfo();
            var pi = theType.GetDeclaredProperty(PropertyName);
            if (pi == null)
                throw new ArgumentException($"{nameof(PropertyName)} not found on {value.GetType()}");

            return pi.GetValue(parameter);
        }

        /// <summary>
        /// Used to convert a value from target > source; not used for this converter.
        /// </summary>
        /// <param name="value">To be added.</param>
        /// <param name="targetType">To be added.</param>
        /// <param name="parameter">To be added.</param>
        /// <param name="culture">To be added.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        
        /// <summary>
        /// Returns the converter
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
