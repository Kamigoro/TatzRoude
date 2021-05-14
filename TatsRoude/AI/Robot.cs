using Microsoft.ML;
using System;
using System.Threading.Tasks;
using TatsRoude.Client.AI.Data;

namespace TatsRoude.Client.AI
{
    public class Robot : IRobot
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;
        private readonly PredictionEngine<SentimentData, PredictionData> _engine;

        public Robot()
        {
            //Creation of context
            _mlContext = new MLContext();

            //Data Loading
            var trainingData = _mlContext.Data.LoadFromTextFile<SentimentData>(@"AI\Data\Samples.tsv", hasHeader: true);

            //Specification of the algorithm used
            var pipeline = _mlContext.Transforms.Text.FeaturizeText(outputColumnName: "Features", inputColumnName: nameof(SentimentData.Text)).
                Append(_mlContext.BinaryClassification.Trainers.SdcaNonCalibrated());

            //Training of the model
            _model = pipeline.Fit(trainingData);

            //Creation of prediction engine
            _engine = _mlContext.Model.CreatePredictionEngine<SentimentData, PredictionData>(_model);

            Console.WriteLine("Model has been trained and is ready !!");
        }

        public Task<PredictionData> IsThisToxic(string message)
        {
            var data = new SentimentData() { Text = message };
            var prediction = _engine.Predict(data);
            return Task.FromResult(prediction);
        }
    }
}
