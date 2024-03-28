using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    /// <summary>
    /// Один предмет
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Название предмета
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Конструктор предмета
        /// </summary>
        /// <param name="name">Название предмета</param>
        public Item(string name) { Name = name; }

        /// <summary>
        /// Метод для получения предмета
        /// </summary>
        /// <returns>Возвращаемый предмет</returns>
        public virtual Item Open() { return this; }
    }
}
