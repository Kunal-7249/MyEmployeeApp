using MyEmployeeApp.Domain.Entities;
using MyEmployeeApp.Domain.RepositoryInterfaces;
using Nest;

public class EmployeeSearchRepository : IEmployeeSearchRepository
{
    private readonly IElasticClient _elasticClient;

    public EmployeeSearchRepository(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    public async Task<IEnumerable<Employee>> SearchAsync(string query)
    {
        var response = await _elasticClient.SearchAsync<Employee>(s => s
            .Index("employees")
            .Query(q => q.QueryString(qs => qs.Query(query))));

        return response.Documents;
    }
}
