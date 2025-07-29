using MyEmployeeApp.Domain.Entities;
using MyEmployeeApp.Domain.DTOs;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEmployeeApp.Infrastructure.Elastic.QueryBuilders
{
    public static class EmployeeSearchQueryBuilder
    {
        public static QueryContainer Build(EmployeeSearchDto dto, QueryContainerDescriptor<Employee> q)
        {
            QueryContainer query = null;

            if (!string.IsNullOrWhiteSpace(dto.Name))
                query &= q.Match(m => m.Field(f => f.Name).Query(dto.Name));

            if (dto.Age.HasValue)
            {
                var comparison = dto.AgeComparison ?? "=";
                var birthdate = DateTime.Now.AddYears(-dto.Age.Value);
                switch (comparison)
                {
                    case ">":
                        query &= q.DateRange(r => r.Field(f => f.BirthDate).LessThan(birthdate));
                        break;
                    case "<":
                        query &= q.DateRange(r => r.Field(f => f.BirthDate).GreaterThan(birthdate));
                        break;
                    default:
                        query &= q.DateRange(r => r
                            .Field(f => f.BirthDate)
                            .GreaterThanOrEquals(birthdate.AddYears(-1))
                            .LessThanOrEquals(birthdate));
                        break;
                }
            }

            if (dto.Salary.HasValue)
            {
                var comp = dto.SalaryComparison ?? "=";
                switch (comp)
                {
                    case ">":
                        query &= q.Range(r => r.Field(f => f.Salary).GreaterThan(Convert.ToDouble(dto.Salary.Value)));
                        break;
                    case "<":
                        query &= q.Range(r => r.Field(f => f.Salary).LessThan(Convert.ToDouble(dto.Salary.Value)));
                        break;
                    case ">=":
                        query &= q.Range(r => r.Field(f => f.Salary).GreaterThanOrEquals(Convert.ToDouble(dto.Salary.Value)));
                        break;
                    case "<=":
                        query &= q.Range(r => r.Field(f => f.Salary).LessThanOrEquals(Convert.ToDouble(dto.Salary.Value)));
                        break;
                    case "=":
                        query &= q.Term(t => t.Field(f => f.Salary).Value(Convert.ToDouble(dto.Salary.Value)));
                        break;
                }
            }

            return query ?? q.MatchAll();
        }
    }

}
