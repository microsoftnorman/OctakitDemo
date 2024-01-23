using System.Diagnostics;
using Octokit;
using System.Text.Json;
using ProductHeaderValue = Octokit.ProductHeaderValue;

namespace GitHubInformationGrabber
{
    internal class GithubMetaScraper
    {
        private GitHubClient github;

        public bool IsRateLimited
        {
            get; private set;
        }
       public GithubMetaScraper(string token = "") {             
           github = new GitHubClient(new ProductHeaderValue("MetaDataGrabber"))
           {
               Credentials = new Credentials(token)
           };
            IsRateLimited = false;
        }
      
        private async Task WriteListToJsonFileAsync<T>(IReadOnlyList<T> list, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, System.IO.FileMode.OpenOrCreate))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    string jsonString = JsonSerializer.Serialize(list);
                    await writer.WriteAsync(jsonString);
                }
            }
        }

        public async Task<List<string>> GetMetaData(string organization, int ratelimit = 2000)
        {
            var timestamp = DateTime.Now.ToString("yyyy-MM-ddHHmmss");
            var repofile = $"{timestamp}-repos.json";
            var userFile = $"{timestamp}-members.json";
            var issueFile = $"{timestamp}-issues.json";
            var commitFile = $"{timestamp}-commits.json";
            var pullRequestFile = $"{timestamp}-pullrequests.json";


            var files = new List<string> { repofile, userFile, issueFile, pullRequestFile, commitFile };

            var members = await github.Organization.Member.GetAll(organization);
            await WriteListToJsonFileAsync(members, userFile);

            IReadOnlyList<Repository> allRepos = await github.Repository.GetAllForOrg(organization);
            await WriteListToJsonFileAsync(allRepos, repofile);

            foreach (var repo in allRepos)
            {
                await RateLimitMonitor(ratelimit);

                try
                {
                    var commits = await github.Repository.Commit.GetAll(organization, repo.Name);
                    await WriteListToJsonFileAsync<GitHubCommit>(commits, commitFile);

                    var issues = await github.Issue.GetAllForRepository(organization, repo.Name);
                    await WriteListToJsonFileAsync(issues, issueFile);

                    var pullRequest = await github.PullRequest.GetAllForRepository(organization, repo.Name);
                    await WriteListToJsonFileAsync(pullRequest, pullRequestFile);
                }
                catch (Octokit.ApiException ex)
                {
                    //do nothing repo is probably empty
                }

            }

            return files;
        }

        private async Task RateLimitMonitor(int ratelimit)
        {
            var rateLimitRemaing = github.GetLastApiInfo().RateLimit.Remaining;
            while (rateLimitRemaing <= ratelimit)
            {
                IsRateLimited = rateLimitRemaing <= ratelimit;
                await Task.Delay(TimeSpan.FromMinutes(1));
                rateLimitRemaing = github.GetLastApiInfo().RateLimit.Remaining;
            }
        }
    }
}
