using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static System.Net.Mime.MediaTypeNames;
using Xunit;

namespace CongestionTaxCalculator.Test
{
    [TestClass]
    public class TaxCalculator
    {
        [TestMethod]
        public void calculate_tax_on_some_times()
        {
            var configurableDateTimeOffsetProvider = Application.Services.GetService<IConfigurableDateTimeOffsetProvider>()!;
        }
    }
}
