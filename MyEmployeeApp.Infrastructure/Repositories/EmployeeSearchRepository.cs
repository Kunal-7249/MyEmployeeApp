using MyEmployeeApp.Domain.DTOs;
using MyEmployeeApp.Domain.Entities;
using MyEmployeeApp.Domain.RepositoryInterfaces;
using MyEmployeeApp.Infrastructure.Elastic.QueryBuilders;
using Nest;

public class EmployeeSearchRepository : IEmployeeSearchRepository
{
    private readonly IElasticClient _elasticClient;

    public EmployeeSearchRepository(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    public async Task<IEnumerable<Employee>> SearchAsync(EmployeeSearchDto dto)
    {
        var query = EmployeeSearchQueryBuilder.Build(dto, new QueryContainerDescriptor<Employee>());

        var response = await _elasticClient.SearchAsync<Employee>(s => s
            .Index("employees")
            .Query(_ => query)
        );

        return response.Documents;
    }
}
