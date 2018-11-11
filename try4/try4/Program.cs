using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace try4
{
        class Program
        {
            // subscriptionKey = "0123456789abcdef0123456789ABCDEF"
            private const string subscriptionKey = "a21f6563c85b40fe8b8889d8ca3c9d17";

            

            // Specify the features to return
            private static readonly List<VisualFeatureTypes> features =
                new List<VisualFeatureTypes>()
            {
            VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
            VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
            VisualFeatureTypes.Tags
            };

            static void Main(string[] args)
            {
            if (Inicio("https://i.blogs.es/529382/osete/450_1000.jpg"))
            {
                Console.WriteLine("Todo bien");
            }
            else
            {
                Console.WriteLine("Error");
            }
            }


            public static Boolean Inicio(string url_imagen)
        {
            string remoteImageUrl = url_imagen;

            ComputerVisionClient computerVision = new ComputerVisionClient(
                new ApiKeyServiceClientCredentials(subscriptionKey),
                new System.Net.Http.DelegatingHandler[] { });

            computerVision.Endpoint = "https://northeurope.api.cognitive.microsoft.com/";

            Console.WriteLine("Images being analyzed ...");
            var t1 = AnalyzeRemoteAsync(computerVision, remoteImageUrl);

            Task.WhenAll(t1).Wait(5000);
            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
            return true;
        }
            // Analyze a remote image
            private static async Task AnalyzeRemoteAsync(
                ComputerVisionClient computerVision, string imageUrl)
            {
                if (!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
                {
                    Console.WriteLine(
                        "\nInvalid remoteImageUrl:\n{0} \n", imageUrl);
                    return;
                }

                ImageAnalysis analysis =
                    await computerVision.AnalyzeImageAsync(imageUrl, features);
                DisplayResults(analysis, imageUrl);
            }

            // Analyze a local image
            private static async Task AnalyzeLocalAsync(
                ComputerVisionClient computerVision, string imagePath)
            {
                if (!File.Exists(imagePath))
                {
                    Console.WriteLine(
                        "\nUnable to open or read localImagePath:\n{0} \n", imagePath);
                    return;
                }

                using (Stream imageStream = File.OpenRead(imagePath))
                {
                    ImageAnalysis analysis = await computerVision.AnalyzeImageInStreamAsync(
                        imageStream, features);
                    DisplayResults(analysis, imagePath);
                }
            }

            // Display the most relevant caption for the image
            private static void DisplayResults(ImageAnalysis analysis, string imageUri)
            {
                Console.WriteLine(imageUri);
            if (!analysis.Description.Captions[0].Text.Contains("microwave"))
            {
                Console.WriteLine("No es microonda.");
                return;
            }
                Console.WriteLine(analysis.Description.Captions[0].Text + "\n");
            }
        }
    }
