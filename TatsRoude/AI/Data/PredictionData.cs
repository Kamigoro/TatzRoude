using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace TatsRoude.Client.AI.Data
{
    public class PredictionData
    {
        // ColumnName attribute is used to change the column name from
        // its default value, which is the name of the field.
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }

        // No need to specify ColumnName attribute, because the field
        // name "Probability" is the column name we want.
        public float Probability { get; set; }

        public float Score { get; set; }
    }
}
