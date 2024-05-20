using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Model.Nile.Interface;
using Model.Nile.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Model.Nile.Scheduler
{
    public class MicrosoftCalender(MicrosoftKeysOptions microsoftKeysOptions) : BaseCalender ,IMicrosoftCalender
    {
        private readonly MicrosoftKeysOptions _microsoftKeysOptions = microsoftKeysOptions;

        public string GetCelender()
        {
            throw new NotImplementedException();
        }

        private GraphServiceClient GetGraphServiceClient()
        {

            string[] scopes = { _microsoftKeysOptions.Scope };

            ClientSecretCredential clientSecretCredential = new ClientSecretCredential(_microsoftKeysOptions.TenantId, _microsoftKeysOptions.ClientId, _microsoftKeysOptions.ClientSecret);

            return new GraphServiceClient(clientSecretCredential, scopes);
        }

        public async Task<string> GetUserProfileDetails(string email)
        {
            
           var graphServiceClient = GetGraphServiceClient();            
           User me =   await graphServiceClient.Users[email].GetAsync(r => r.QueryParameters.Select = ["displayName", "officeLocation"]);          
           return JsonSerializer.Serialize(me);
        }


        public async Task<string> GetAllUsers()
        {
            var graphServiceClient = GetGraphServiceClient();            
            UserCollectionResponse userCollection = await graphServiceClient.Users.GetAsync();

            return JsonSerializer.Serialize(userCollection);
        }
        public  async Task<string> GetCelender(string email)
        {
            const int bufferDays = 7;
            var graphServiceClient = GetGraphServiceClient();
            var result = await graphServiceClient.Users[email].CalendarView.GetAsync((requestConfiguration) =>
            {
                requestConfiguration.QueryParameters.StartDateTime = DateTime.Now.AddDays(-bufferDays).ToString();
                requestConfiguration.QueryParameters.EndDateTime = DateTime.Now.AddDays(bufferDays).ToString();
            });

            return JsonSerializer.Serialize(result);
        }

        
    }
}
