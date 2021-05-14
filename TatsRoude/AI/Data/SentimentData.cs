using Microsoft.ML.Data;

namespace TatsRoude.Client.AI.Data
{
    public class SentimentData
    {
        [LoadColumn(0)]
        public bool Label { get; set; }
        [LoadColumn(2)]
        public string Text { get; set; }
    }
}
