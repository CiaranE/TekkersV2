using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekkersV2.Models;
using Plugin.RestClient.AssessmentClient;
using Plugin.RestClient.TestClient;

namespace TekkersV2.Services
{
    public class AssessmentServices
    {

        public async Task<List<Assessment>> GetAssessmentsAsync()
        {
            AssessmentClient<Assessment> client = new AssessmentClient<Assessment>();

            var listofAssessments = await client.GetAsync();

            return listofAssessments;
        }

        public async Task PostAssessmentAsync(Assessment a)
        {
            AssessmentClient<Assessment> client = new AssessmentClient<Assessment>();
            var sendAssessment = await client.PostAsync(a);
        }

        public async Task PutAssessmentAsync(string Id, Assessment a)
        {
            AssessmentClient<Assessment> client = new AssessmentClient<Assessment>();
            var updateAssessment = await client.PutAsync(a.Id, a);
        }

        public async Task DeleteAssessmentAsync(string Id)
        {
            AssessmentClient<Assessment> client = new AssessmentClient<Assessment>();
            var deleteAssessment = await client.DeleteAsync(Id);
        }

        //Find a Assessment by their surname
        public async Task<List<Assessment>> GetAssessmentByNameAsync(string name)
        {
            AssessmentClient<Assessment> client = new AssessmentClient<Assessment>();

            var listofAssessments = await client.GetByNameAsync(name);

            return listofAssessments;
        }
    }
}
