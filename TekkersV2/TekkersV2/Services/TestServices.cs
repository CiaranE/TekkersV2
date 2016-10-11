using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekkersV2.Models;
using Plugin.RestClient.TestClient;

namespace TekkersV2.Services
{
    public class TestServices
    {

        public async Task<List<Test>> GetTestsAsync()
        {
            TestClient<Test> client = new TestClient<Test>();

            var listofTests = await client.GetAsync();

            return listofTests;
        }

        public async Task PostTestAsync(Test t)
        {
            TestClient<Test> client = new TestClient<Test>();
            var sendTest = await client.PostAsync(t);
        }

        public async Task PutTestAsync(string Id, Test t)
        {
            TestClient<Test> client = new TestClient<Test>();
            var updateTest = await client.PutAsync(t.Id, t);
        }

        public async Task DeleteTestAsync(string Id)
        {
            TestClient<Test> client = new TestClient<Test>();
            var deleteTest = await client.DeleteAsync(Id);
        }

        //Find a Test by their surname
        public async Task<List<Test>> GetTestByNameAsync(string name)
        {
            TestClient<Test> client = new TestClient<Test>();

            var listofTests = await client.GetByNameAsync(name);

            return listofTests;
        }

        internal async Task<List<Test>> GetAssessmentTestsAsync(string assessid)
        {
            TestClient<Test> client = new TestClient<Test>();
            var listOfAssessmentTests = await client.GetTestsByAssessmentAsync(assessid);
            return listOfAssessmentTests;
        }
    }
}
