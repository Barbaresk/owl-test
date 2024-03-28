using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    /// <summary>
    /// Описание вероятности выпадения предмета 
    /// </summary>
    public class ItemProbability
    {
        /// <summary>
        /// Статистический вес
        /// </summary>
        public uint Weight { get; private set; }
        
        /// <summary>
        /// Предмет
        /// </summary>
        public Item Item { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="weight">Статистический вес</param>
        /// <param name="item">Предмет</param>
        public ItemProbability(uint weight, Item item)
        {
            if (weight == 0) throw new ArgumentException("weight should be greater than zero");
            if (item == null) throw new ArgumentNullException("item is null");
            Weight = weight;
            Item = item;
        }
    }
}
