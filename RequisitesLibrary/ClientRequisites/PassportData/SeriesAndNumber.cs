﻿using Microsoft.EntityFrameworkCore;

namespace RequisitesLibrary.ClientRequisites.PassportData
{
    [Owned]
    /// <summary>
    /// Класс серии и номера паспорта
    /// </summary>
    public class SeriesAndNumber
    {
        /// <summary>
        /// Конструктор серии и номера паспорта
        /// </summary>
        /// <param name="series">серия</param>
        /// <param name="number">номер</param>
        public SeriesAndNumber(string series, string number)
        {
            this.Series = series;
            this.Number = number;
        }

        public string FullSeriesAndNumber => $"S: {Series} N: {Number}";

        public string Series { get; private set; }
        public string Number { get; private set; }
    }
}
