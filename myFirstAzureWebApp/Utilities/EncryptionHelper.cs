using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;
using System.Web.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace myFirstAzureWebApp.Utilities
{
    public class EncryptionHelper
    {
        //this is an optional property to hold the secret after it is retrieved
        public static string EncryptSecret { get; set; }

        //the method that will be provided to the KeyVaultClient
        public static async Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            ClientCredential clientCred = new ClientCredential(WebConfigurationManager.AppSettings["ClientId"],
                        WebConfigurationManager.AppSettings["ClientSecret"]);
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)
                throw new InvalidOperationException("Failed to obtain the JWT token");

            return result.AccessToken;
        }


        //static string GetAccountSASToken()
        //{
        //    // To create the account SAS, you need to use your shared key credentials. Modify for your account.
        //    string ConnectionString = "DefaultEndpointsProtocol=https;AccountName=wsachin;AccountKey=" + EncryptSecret;
        //    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);

        //                // Return the SAS token.
        //    return storageAccount.GetSharedAccessSignature(policy);
        //}

        public static string Getimage()
        {
            string ConnectionString = "DefaultEndpointsProtocol=https;AccountName=wsachin;AccountKey=" + EncryptSecret;

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");
            
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("myblob");
            String text="";
            using (var memoryStream = new MemoryStream())
            {
                blockBlob.DownloadToStream(memoryStream);
            

                
                text = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray(), 0, memoryStream.ToArray().Length);
            }



            return text;
        }
    }
}