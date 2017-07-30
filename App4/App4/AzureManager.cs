using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using App4.Models;

namespace App4
{
    public class AzureManager
    {

        private static AzureManager instance;
        private MobileServiceClient client;
        private IMobileServiceTable<Product> EmotionTable;

        public AzureManager()
        {
            this.client = new MobileServiceClient("http://pelpose1.azurewebsites.net");
            this.EmotionTable = this.client.GetTable<Product>();
        }

        public MobileServiceClient AzureClient
        {
            get { return client; }
        }

        public static AzureManager AzureManagerInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AzureManager();
                }

                return instance;
            }
        }

        public async Task<List<Product>> GetEmotion()
        {

            List<Product> NewEmotion = new List<Product>();

            NewEmotion = await EmotionTable.ToListAsync();

            return NewEmotion;

        }

        public async Task<Product> InsertEmotion(Product Emotion)
        {

            await EmotionTable.InsertAsync(Emotion);

            return Emotion;

        }

    }
}