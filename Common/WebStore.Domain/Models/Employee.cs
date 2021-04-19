using System;

namespace WebStore.Domain.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public int Age
        {
            get
            {
                return CalcAge(Birthday ?? DateTime.Now);
            }
        }

        public DateTime? Birthday { get; set; }

        public DateTime HireDate { get; set; }

        public decimal Salary { get; set; }

        public string EMail { get; set; }

        /// <summary>
        /// метод расчета возраста сотрудника в годах
        /// </summary>
        /// <param name="dOb"></param>
        /// <returns></returns>
        private int CalcAge(DateTime dOb)
        {
            int age = 0;
            age = DateTime.Now.Year - dOb.Year;
            if (DateTime.Now.DayOfYear < dOb.DayOfYear)
                age--;

            return age;
        }

        public override string ToString() => $"{LastName} {FirstName} {Patronymic} {Age}";
    }
}
