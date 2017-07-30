using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using Xamarin.Forms;

using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using System.Linq;
using App4.Models;
using System.Threading.Tasks;

namespace App4.Views
{
	public partial class AboutPage : ContentPage
	{
        String result;
        public Command AddItemsCommand { get; set; }

        private AzureManager azureTest;

        //Emotion Server setup
        const String KEY = "88f3cbcd7fa3429a8bada7a6e8f90cb3";
        EmotionServiceClient emotionServiceClient = new EmotionServiceClient(KEY);

		public AboutPage()
		{
			InitializeComponent();
            azureTest = new AzureManager();


        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {

            await SaveEmotion();
            return;
        }


        private async Task SaveEmotion()
        {
            String NewEmotion = result;
            if (!String.IsNullOrEmpty(NewEmotion))
            {
                Product product = new Product();

                try
                {
                    product.EmotionString = NewEmotion;
                    product.Date = DateTime.UtcNow;

                    product = await azureTest.InsertEmotion(product);
                    await DisplayAlert("Thank you!", "your Emotion successfully uploaded.", "OK");
                    return;

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    IsBusy = false;
                }
            }
            else
            {
                await DisplayAlert("Error", "Please Take photo first!", "OK");
                return;
            }
        }


            private async void Button_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Camera is not working", "There is No camera available.", "OK");
                return;
            }

            MediaFile emotionImage = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Medium,
                Directory = "Sample",
                Name = $"{DateTime.UtcNow}.jpg"
            });
            
            //checks the image is null or not
            if (emotionImage == null)
                return;

            //display the image
            photo.Source = ImageSource.FromStream(() =>
            {
                return emotionImage.GetStream();
            });

            //connects to server and get emotion as string.
            using (var photoStream = emotionImage.GetStream())
            {
                Emotion[] emotionResult = await emotionServiceClient.RecognizeAsync(photoStream);
                if (emotionResult.Any())
                {
                    // Emotions detected are happiness, sadness, surprise, anger, fear, contempt, disgust, or neutral.
                    emotion.Text = emotionResult.FirstOrDefault().Scores.ToRankedList().FirstOrDefault().Key;
                    result = emotion.Text;
                }
            }
            emotionImage.Dispose();
        }
    }
}
