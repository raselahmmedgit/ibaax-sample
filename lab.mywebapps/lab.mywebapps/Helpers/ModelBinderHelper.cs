using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab.mywebapps.Helpers
{
    public class DateTimeModelBinder : DefaultModelBinder
    {
        ////way of 1
        //private string _customFormat;
        ////way of 1
        //public DateTimeModelBinder(string customFormat)
        //{
        //    _customFormat = customFormat;
        //}
        ////way of 1
        //public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        //{
        //    var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        //    return DateTime.ParseExact(value.AttemptedValue, _customFormat, CultureInfo.InvariantCulture);
        //}

        //public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        //{
        //    var displayFormat = bindingContext.ModelMetadata.DisplayFormatString;
        //    var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        //    return DateTime.ParseExact(value.AttemptedValue, displayFormat, CultureInfo.InvariantCulture);
        //}

        //public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        //{
        //    var displayFormat = bindingContext.ModelMetadata.DisplayFormatString;
        //    var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        //    if (!string.IsNullOrEmpty(displayFormat) && value != null)
        //    {
        //        DateTime date;
        //        displayFormat = displayFormat.Replace("{0:", string.Empty).Replace("}", string.Empty);
        //        // use the format specified in the DisplayFormat attribute to parse the date
        //        if (DateTime.TryParseExact(value.AttemptedValue, displayFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        //        {
        //            return date;
        //        }
        //        else
        //        {
        //            bindingContext.ModelState.AddModelError(
        //                bindingContext.ModelName,
        //                string.Format("{0} is an invalid date format", value.AttemptedValue)
        //            );
        //        }
        //    }

        //    return base.BindModel(controllerContext, bindingContext);
        //}
    }
}