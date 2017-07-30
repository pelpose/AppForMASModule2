using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using App4.Models;

namespace App4
{
public class AzureManger
{

    private static AzureManger instance;
    private MobileServiceClient client;
    private IMobileServiceTable<Product> emotion;

    private AzureManger()
    {
        this.client = new MobileServiceClient("https://pelpose1.azurewebsites.net");
        this.emotion = this.client.GetTable<Product>();
    }

    public MobileServiceClient AzureClient
    {
        get { return client; }
    }

    public static AzureManger AzureManagerInstance
    {
        get
        {
            if (instance == null)
            {
                instance = new AzureManger();
            }

            return instance;
        }
    }

    public async Task<List<Product>> GetHotDogInformation()
    {
        return await this.emotion.ToListAsync();
    }
}
}
