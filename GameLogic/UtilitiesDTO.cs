using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    /// <summary>
    /// Стуктура DTO для передачи инфы о вероятности
    /// </summary>
    public struct ProbabilityInfo
    {
        public string Name;
        public uint Weight;
    }

    /// <summary>
    /// Структура DTO для передачи списка вложенных элементов
    /// </summary>
    public struct BoxInfo
    {
        public string Name;
        public IEnumerable<ProbabilityInfo> Probabilities;
    }
}
