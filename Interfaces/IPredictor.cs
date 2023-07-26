using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Analysis;

namespace RecommendationSystem.Interfaces
{
    internal interface IPredictor
    {
        void Predict(DataFrame dataFrame);
    }
}
