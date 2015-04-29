using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDateProject.DataModel
{
    internal struct MyDateStruct
    {
        public byte Day;
        public byte Month;
        public int Year;

        public MyDateStruct(byte day, byte month, int year)
        {
            Day = day;
            Month = month;
            Year = year;
        }
    }
}
