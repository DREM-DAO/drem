using AutoMapper;
using DREM_API.Model;
using DREM_API.Model.Comm;
using DREM_API.Model.DB;
using DREM_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.BusinessController
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectBusinessController
    {
        private readonly ProjectRepository repository;
        private readonly ValueSetRepository valueSetRepository;
        private readonly Mapper mapper;
        private readonly Dictionary<string, Dictionary<string, string>> valueSets;
        /// <summary>
        /// Constructor business project controller
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="valueSetRepository"></param>
        public ProjectBusinessController(ProjectRepository repository, ValueSetRepository valueSetRepository)
        {
            this.repository = repository;
            this.valueSetRepository = valueSetRepository;
            valueSets = valueSetRepository.ListAll("en-US");
            mapper = new Mapper(new MapperConfiguration(cnf =>
            {
                cnf.CreateMap<Model.DB.Project, Model.Comm.ProjectWithValueSets>();
            }));
        }

        internal Task<Model.DB.Project> CreateAsync(Model.Comm.ProjectBase project)
        {
            return repository.CreateAsync(project);
        }

        internal Task<Model.DB.Project> UpdateAsync(ProjectWithId project)
        {
            return repository.UpdateAsync(project);
        }

        internal Task<int> DeleteAsync(string id)
        {
            return repository.DeleteAsync(id);
        }

        internal IQueryable<Model.DB.Project> ListAll()
        {
            return repository.ListAll();
        }
        internal IEnumerable<Model.Comm.ProjectWithValueSets> ListAllPublic()
        {
            return repository.ListAllPublic().Select(Convert);
        }
        private Model.Comm.ProjectWithValueSets Convert(Model.DB.Project project)
        {
            var ret = mapper.Map<Model.Comm.ProjectWithValueSets>(project);

            if (valueSets.ContainsKey("region"))
            {
                if (string.IsNullOrEmpty(ret.Region) || !valueSets["region"].ContainsKey(ret.Region)) return null;
                ret.RegionName = valueSets["region"][ret.Region];
            }
            if (valueSets.ContainsKey("country"))
            {
                if (!string.IsNullOrEmpty(ret.Country) && valueSets["country"].ContainsKey(ret.Country))
                {
                    ret.CountryName = valueSets["country"][ret.Country];
                }
            }
            if (valueSets.ContainsKey("city"))
            {
                if (string.IsNullOrEmpty(ret.City) || !valueSets["city"].ContainsKey(ret.City)) return null;
                ret.CityName = valueSets["city"][ret.City];
            }
            if (valueSets.ContainsKey("currency"))
            {
                if (!valueSets["currency"].ContainsKey(ret.Currency)) return null;
                ret.CurrencyName = valueSets["currency"][ret.Currency];
            }
            if (valueSets.ContainsKey("investmentType"))
            {
                if (string.IsNullOrEmpty(ret.InvestmentType) || !valueSets["investmentType"].ContainsKey(ret.InvestmentType)) return null;
                ret.InvestmentTypeName = valueSets["investmentType"][ret.InvestmentType];
            }
            if (valueSets.ContainsKey("propertyType"))
            {
                if (string.IsNullOrEmpty(ret.PropertyType) || !valueSets["propertyType"].ContainsKey(ret.PropertyType)) return null;
                ret.PropertyTypeName = valueSets["propertyType"][ret.PropertyType];
            }
            return ret;
        }
        internal Model.Comm.ProjectDetail GetDetailByUrlId(string urlId)
        {
            var ret = new ProjectDetail();
            ret.Project = Convert(repository.GetProjectByUrlId(urlId));

            return ret;
        }
    }
}
