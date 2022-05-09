using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.ML;
using SampleML.NET.DataModels;

namespace SampleML.NET.Pages
{
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly PredictionEnginePool<SentimentData, SentimentPrediction> _predictionEnginePool;

        public IndexModel(ILogger<IndexModel> logger, PredictionEnginePool<SentimentData, SentimentPrediction> predictionEnginePool)
        {
            _logger = logger;
            _predictionEnginePool = predictionEnginePool;
        }

        public void OnGet()
        {

        }

        [BindProperty]
        public SentimentData SentimentData { get; set; }

        public Task<ContentResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Task.FromResult(Content("Error"));
            }

            SentimentPrediction prediction = _predictionEnginePool.Predict(modelName: "SentimentAnalysisModel", example: SentimentData);

            string sentiment = Convert.ToBoolean(prediction.Prediction) ? "Positive" : "Negative";

            return Task.FromResult(Content(sentiment));
        }
    }
}