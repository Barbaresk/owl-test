using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace GameLogic
{
    /// <summary>
    /// Коробка с предметами
    /// </summary>
    public class Box : Item
    {
        private IProbabilityService _probabilityService;
        private List<ItemProbability> _itemProbabilities; 

        /// <summary>
        /// Конструктор коробки с предметами
        /// </summary>
        /// <param name="name">Название коробки</param>
        /// <param name="probabilityService">Сервис для генерации рандома</param>
        public Box(string name, IProbabilityService probabilityService) : base(name)
        {
            _probabilityService = probabilityService ?? throw new ArgumentNullException("probability service is null");
            _itemProbabilities = new List<ItemProbability>();
        }

        /// <summary>
        /// Метод добавления предмета в коробку
        /// </summary>
        /// <param name="weight">Статистический вес</param>
        /// <param name="item"></param>
        public void AddItem(uint weight, Item item) 
        {
            _itemProbabilities.Add(new ItemProbability(weight, item));
        }

        /// <summary>
        /// Метод выбора предмета с учетом их относительного статистического веса
        /// </summary>
        /// <returns>Найденный предмет</returns>
        public override Item Open()
        {
            if (_itemProbabilities.Count == 0) throw new Exception("empty box");
            uint rand = _probabilityService.GetProbability(_itemProbabilities.Aggregate(0u, (s, i) => s + i.Weight));
            uint sum = 0;
            foreach(var i in _itemProbabilities)
            {
                sum += i.Weight;
                if (sum >= rand)
                {
                    return i.Item.Open();
                }
            }
            throw new Exception("wrong probability weight");

        }
    }
}
