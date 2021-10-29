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
        private readonly OrderBusinessController orderBusinessController;
        private readonly DailyPayoutBusinessController dailyPayoutBusinessController;
        private readonly ImageMetaBusinessController imageMetaBusinessController;
        private readonly TransferBusinessController transferBusinessController;
        private readonly BufferTransferBusinessController bufferTransferBusinessController;
        private readonly ShareholderBusinessController shareholderBusinessController;
        private readonly VotingBusinessController votingBusinessController;

        private readonly Mapper mapper;
        private readonly Dictionary<string, Dictionary<string, string>> valueSets;
        /// <summary>
        /// Constructor business project controller
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="valueSetRepository"></param>
        /// <param name="orderBusinessController"></param>
        /// <param name="dailyPayoutBusinessController"></param>
        /// <param name="imageMetaBusinessController"></param>
        /// <param name="transferBusinessController"></param>
        /// <param name="bufferTransferBusinessController"></param>
        /// <param name="shareholderBusinessController"></param>
        public ProjectBusinessController(
            ProjectRepository repository,
            ValueSetRepository valueSetRepository,
            OrderBusinessController orderBusinessController,
            DailyPayoutBusinessController dailyPayoutBusinessController,
            ImageMetaBusinessController imageMetaBusinessController,
            TransferBusinessController transferBusinessController,
            BufferTransferBusinessController bufferTransferBusinessController,
            ShareholderBusinessController shareholderBusinessController,
            VotingBusinessController votingBusinessController
            )
        {
            this.repository = repository;
            this.valueSetRepository = valueSetRepository;
            this.orderBusinessController = orderBusinessController;
            this.dailyPayoutBusinessController = dailyPayoutBusinessController;
            this.imageMetaBusinessController = imageMetaBusinessController;
            this.transferBusinessController = transferBusinessController;
            this.bufferTransferBusinessController = bufferTransferBusinessController;
            this.shareholderBusinessController = shareholderBusinessController;
            this.votingBusinessController = votingBusinessController;

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
            if (!string.IsNullOrEmpty(ret.Project?.Id))
            {
                ret.Images = imageMetaBusinessController.ListAllForProject(ret.Project.Id).ToArray();
                ret.BufferTxs = bufferTransferBusinessController.ListAllForProject(ret.Project.Id).ToArray();
            }
            if (ret.Project.ASA.HasValue)
            {
                ret.Shareholders = shareholderBusinessController.ListAllForAsset(ret.Project.ASA.Value).ToArray();
                ret.Transfers = transferBusinessController.ListAllForAsset(ret.Project.ASA.Value).ToArray();
                ret.Bids = orderBusinessController.ListAllBidsForProject(ret.Project.ASA.Value).ToArray();
                ret.Offers = orderBusinessController.ListAllOffersForProject(ret.Project.ASA.Value).ToArray();
                ret.Bids = orderBusinessController.ListAllBidsForProject(ret.Project.ASA.Value).ToArray();
            }
            if (!string.IsNullOrEmpty(ret.Project.IssuerAccount))
            {
                var qList = new List<Model.Comm.Voting.VotingBase>();
                foreach (var item in votingBusinessController.ListAllForProject(ret.Project.IssuerAccount))
                {
                    qList.Add(new Model.Comm.Voting.VotingBase()
                    {
                        Question = item,
                        Result = votingBusinessController.Get(item.TxId)
                    });
                }

            }
            return ret;
        }
    }
}
