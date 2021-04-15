using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Введите отчество")]
        public string Patronymic { get; set; }

        public DateTime? Birthday { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public DateTime HireDate { get; set; }

        [RegularExpression(@"\d+(\,\d{1,2})?", ErrorMessage = "Формат ввода #, либо #,## ")]
        public decimal Salary { get; set; }

        public string EMail { get; set; }

        public int Age
        {
            get
            {
                return CalcAge(Birthday ?? DateTime.Now);
            }
        }

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
    }
}
