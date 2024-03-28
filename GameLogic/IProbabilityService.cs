using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    /// <summary>
    /// Интерфейс некоторого сервиса, отвечающего за генерацию случайных значений
    /// </summary>
    public interface IProbabilityService
    {
        /// <summary>
        /// Получаем случайное значение диапазона [1, max]
        /// </summary>
        /// <param name="max">Максимальное значение, которое может быть сгенерировано</param>
        /// <returns>Число из диапазона [1, max]</returns>
        uint GetProbability(uint max);
    }
}
