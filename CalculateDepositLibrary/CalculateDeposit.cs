using System;

namespace CalculateDepositLibrary
{
    public class CalculateDeposit
    {
        /// <summary>
        /// Метод расчета депозита без капитализации
        /// </summary>
        /// <param name="balance">баланс депозита</param>
        /// <param name="rate">ставка депозита</param>
        /// <param name="openDate">дата закрытия депозита</param>
        /// <param name="selectedDate">выбранная дата на календаре</param>
        /// <returns>баланс депозита на выбранную дату</returns>
        public static (decimal profit, decimal profitBalance) CalculateWithoutCapitalization(decimal balance, DateTime openDate, double rate, DateTime selectedDate)
        {
            // Если месяц и год на календаре совпадает с месяцем и годом открытия вклада
            if (DateTime.Now.Month == selectedDate.Month && DateTime.Now.Year == selectedDate.Year)
            {
                return (default, default);
            }

            int span = selectedDate.AddMonths(-openDate.Month).Month; // Нахождение интервала для итерации
            span = selectedDate.Day < openDate.Day ? span - 1 : span; // Если выбранный день меньше, чем день открытия, то прибыль за  месяц не учитывается

            decimal profitBalance = balance;

            for (int i = 0; i < span; i++)
            {
                profitBalance += balance * (decimal)rate / 12;
            }

            return (profitBalance - balance, profitBalance);
        }

        /// <summary>
        /// Метод расчета депозита c капитализацией
        /// </summary>
        /// <param name="balance">баланс депозита</param>
        /// <param name="rate">ставка депозита</param>
        /// <param name="openDate">дата открытия депозита</param>
        /// <param name="selectedDate">выбранная дата на календаре</param>
        /// <returns>баланс депозита на выбранную дату</returns>
        public static (decimal profit, decimal profitBalance) CalculateWithCapitalization(decimal balance, DateTime openDate, double rate, DateTime selectedDate)
        {
            // Если месяц и год на календаре совпадает с месяцем и годом открытия вклада
            if (DateTime.Now.Month == selectedDate.Month && DateTime.Now.Year == selectedDate.Year)
            {
                return (default, default);
            }

            int span = selectedDate.AddMonths(-openDate.Month).Month; // Нахождение интервала для итерации
            span = selectedDate.Day < openDate.Day ? span - 1 : span; // Если выбранный день меньше, чем день открытия, то прибыль за  месяц не учитывается

            decimal profitBalance = balance;

            for (int i = 0; i < span; i++)
            {
                profitBalance += profitBalance * (decimal)rate / 12;
            }

            return (profitBalance - balance, profitBalance);
        }
    }
}
