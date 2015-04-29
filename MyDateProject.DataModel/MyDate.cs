using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDateProject.DataModel
{
    /// <summary>
    /// Новое улучшенное представление даты
    /// </summary>
    public class MyDate
    {
        private MyDateStruct _dateStruct;

        #region Константы
        //todo: Доделать локализацию
        private const string LeapYearFebruaryException = "29 февраля для невисокосного года не существует";
        private const string OutOfRangeDescription = "Параметр '{0}' вышел за пределы диапазона";

        private const byte MaxDaysPerMonth = 31;
        private const byte MaxMonthsPerYear = 12;

        public const int MaxYearValue = Int32.MaxValue;
        public const int MinYearValue = Int32.MinValue;

        private static readonly IList<byte> MaxDaysByMonths = new List<byte>(MaxMonthsPerYear) { 31, 29, 31, 30, 31, 30, 31, 30, 30, 31, 30, 31 };
        #endregion

        #region Открытые свойства
        public byte Day
        {
            get
            {
                return _dateStruct.Day;
            }
        }

        public byte Month
        {
            get
            {
                return _dateStruct.Month;
            }
        }

        public int Year
        {
            get
            {
                return _dateStruct.Year;
            }
        }
        #endregion

        #region Internal методы
        /// <summary>
        /// Валидация значений перед присваиванием
        /// </summary>
        /// <param name="day">День</param>
        /// <param name="month">Месяц</param>
        /// <param name="year">Год</param>
        internal static void Validate(byte day, byte month, int year)
        {
            if (day == 0 || day > MaxDaysPerMonth)
                throw new ArgumentOutOfRangeException(String.Format(OutOfRangeDescription, "Day"));
            if (month == 0 || month > MaxMonthsPerYear)
                throw new ArgumentOutOfRangeException(String.Format(OutOfRangeDescription, "Month"));

            if (day > MaxDaysByMonths[month - 1])
                throw new ArgumentOutOfRangeException(String.Format(OutOfRangeDescription, "Day"));

            if (!IsYearLeap(year) && month == (byte)Months.February && day > MaxDaysByMonths[month - 1] - 1)
                throw new ArgumentOutOfRangeException(LeapYearFebruaryException);
        }

        /// <summary>
        /// Число високосных годов до текущего включительно
        /// </summary>
        /// <param name="year">Год</param>
        /// <returns></returns>
        internal static int GetLeapYearsCount(int year)
        {
            return (year / 4) - (year / 100) + (year / 400);
        }

        /// <summary>
        /// Количество суток в текущей дате
        /// </summary>
        /// <returns></returns>
        internal long GetDaysCount()
        {
            var isLeapYear = IsYearLeap(_dateStruct.Year);

            //годы
            var leapYears = GetLeapYearsCount(_dateStruct.Year);
            var notLeapYears = _dateStruct.Year - leapYears;

            long result = leapYears * 366 + notLeapYears * 365;
            //месяца
            for (byte i = 1; i < _dateStruct.Month; i++)
            {
                if (!isLeapYear && i == (byte)Months.February)
                    result += MaxDaysByMonths[i - 1] - 1;
                else
                    result += MaxDaysByMonths[i - 1];
            }
            //дни
            result += _dateStruct.Day;
            return result;
        }

        /// <summary>
        /// Перевод даты из числа дней
        /// </summary>
        /// <param name="days">Число дней</param>
        /// <returns></returns>
        internal static MyDateStruct GetDateByDays(long days)
        {
            var year = (int)(days / (365 + (double)1 / 4 - (double)1 / 100 + (double)1 / 400));
            var leapYears = GetLeapYearsCount(year);
            var notLeapYears = year - leapYears;
            var isLeapYear = IsYearLeap(year);

            days -= leapYears * 366 + notLeapYears * 365;
            byte month = 1;
            for (byte i = 1; i < MaxMonthsPerYear; i++)
            {
                byte daysPerMonth = 0;
                if (!isLeapYear && i == (byte)Months.February)
                    daysPerMonth = (byte)(MaxDaysByMonths[i - 1] - 1);
                else
                    daysPerMonth = MaxDaysByMonths[i - 1];

                if (days > daysPerMonth)
                    days -= daysPerMonth;
                else
                {
                    month = i;
                    break;
                }

            }

            var day = (byte)days;
            return new MyDateStruct(day, month, year);
        }
        #endregion

        #region Public методы
        /// <summary>
        /// Возвращает признак високонсости года. Алгоритм взят отсюда: http://www.net4lady.ru/kalendar-visokosnyx-let/
        /// </summary>
        /// <param name="year">Номер года</param>
        /// <returns></returns>
        public static bool IsYearLeap(int year)
        {
            var isLeap = false;
            var del400 = year % 400;
            var del100 = year % 100;
            var del4 = year % 4;

            if (del400 == 0 && del100 == 0 && del4 == 0)
                isLeap = true;
            else if (del100 != 0 && del4 == 0)
                isLeap = true;

            return isLeap;
        }



        /// <summary>
        /// Операция инкремента даты по дням
        /// </summary>
        /// <param name="days">Число дней</param>
        public void AddDays(int days)
        {
            var newDate = GetDateByDays(GetDaysCount() + days);
            _dateStruct = newDate;
        }

        #endregion

        public MyDate(byte day, byte month, int year)
        {
            Validate(day, month, year);
            _dateStruct = new MyDateStruct(day, month, year);
        }

    }
}
