using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ServerApi.Services
{
    public class ImageVerification
    {
        private const string subscriptionKey = "f8542b42d23d48c2bca19f3e869e4b2b";
        private static readonly List<VisualFeatureTypes> features = new List<VisualFeatureTypes>()
        {
            VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
            VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
            VisualFeatureTypes.Tags
        };

        public static Boolean VerifyImage(string url_imagen, string searchTerm)
        {
            string remoteImageUrl = url_imagen;

            ComputerVisionClient computerVision = new ComputerVisionClient(
                new ApiKeyServiceClientCredentials(subscriptionKey),
                new System.Net.Http.DelegatingHandler[] { });

            computerVision.Endpoint = "https://northeurope.api.cognitive.microsoft.com/";

            var t1 = AnalyzeRemoteAsync(computerVision, remoteImageUrl, searchTerm);
            Task.WhenAll(t1).Wait(5000);
            return t1.Result;
        }
        // Analyze a remote image
        private static async Task<Boolean> AnalyzeRemoteAsync(ComputerVisionClient computerVision, string imageUrl, string searchTerm)
        {
            if (!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
            {
                return false;
            }

            ImageAnalysis analysis = await computerVision.AnalyzeImageAsync(imageUrl, features);
            return DisplayResults(analysis, imageUrl, searchTerm);
        }

        private static async Task<Boolean> AnalyzeLocalAsync(ComputerVisionClient computerVision, string imagePath, string searchTerm)
        {
            if (!File.Exists(imagePath))
            {
                return false;
            }

            using (Stream imageStream = File.OpenRead(imagePath))
            {
                ImageAnalysis analysis = await computerVision.AnalyzeImageInStreamAsync(
                    imageStream, features);
                return DisplayResults(analysis, imagePath, searchTerm);
            }
        }

        private static Boolean DisplayResults(ImageAnalysis analysis, string imageUri, string searchTerm)
        {
            if (analysis.Description.Captions.Count > 0)
            {
                if (!analysis.Description.Captions[0].Text.Contains(searchTerm))
                {
                    return false;
                }
            }
            return true;
        }
    }

}