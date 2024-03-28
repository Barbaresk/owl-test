using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GameLogic
{
    /// <summary>
    /// Трежекласс
    /// </summary>
    public class Treasure
    {
        private Dictionary<string, Box> _boxes;
        /// <summary>
        /// Создание трежекласса на основе списков вложенностей и статистических весов
        /// </summary>
        /// <param name="probabilityService">Сервис генерации вероятностей</param>
        /// <param name="sources">Список элементов трежекласса</param>
        public Treasure(IProbabilityService probabilityService, IEnumerable<BoxInfo> sources)
        {
            if (probabilityService == null) throw new ArgumentNullException("probability service is null");
            if (sources == null) throw new ArgumentNullException("probabilities is null");
            if (sources.Count() == 0) throw new ArgumentException("probabilities list is empty");
            _boxes = new Dictionary<string, Box>();
            //заполнение коробками
            foreach(var s in sources)
            {
                if (_boxes.ContainsKey(s.Name)) throw new Exception("duplicate boxes");
                _boxes.Add(s.Name, new Box(s.Name, probabilityService));
            }
            //заполнение коробок
            foreach (var bi in sources)
            {
                var b = _boxes[bi.Name];
                foreach (var p in bi.Probabilities)
                {
                    if (_boxes.ContainsKey(p.Name))
                        b.AddItem(p.Weight, _boxes[p.Name]);
                    else
                        b.AddItem(p.Weight, new Item(p.Name));
                }
            }
        }

        /// <summary>
        /// Получение случайного предмета из трежи
        /// </summary>
        /// <param name="boxName"></param>
        /// <returns>Случайный предмет</returns>
        public Item GetItemFromBox(string boxName)
        {
            if (!_boxes.ContainsKey(boxName)) throw new Exception("box not found");
            return _boxes[boxName].Open();
        }

        /// <summary>
        /// Проверка существования коробки
        /// </summary>
        /// <param name="boxName">Название коробки</param>
        /// <returns>Признак существует или нет коробка с таким наименованием</returns>
        public bool HasBox(string boxName) => _boxes.ContainsKey(boxName);

        /// <summary>
        /// Список существующих коробок
        /// </summary>
        public IEnumerable<string> BoxNames => _boxes.Select(b => b.Key).ToList();
    }
}
