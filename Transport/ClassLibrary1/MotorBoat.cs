using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKRLib
{
    public sealed class MotorBoat : Transport
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="model">Модель</param>
        /// <param name="power">Мощность</param>
        public MotorBoat(string model, uint power) : base(model, power) { }

        public override string StartEngine()
        {
            return $"{Model}: Brrrbrr";
        }

        /// <summary>
        /// Переопределенный метод для строкового представления объекта
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "MotorBoat. " + base.ToString();
        }
    }
}
