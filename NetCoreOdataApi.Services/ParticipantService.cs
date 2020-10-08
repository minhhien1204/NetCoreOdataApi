using NetCoreOdataApi.Core.Models.Quiz;
using NetCoreOdataApi.Core.Repositories;
using NetCoreOdataApi.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreOdataApi.Services
{
    public interface IParticipantService
    {
        Task<IQueryable<ParticipantViewModel>> GetAllParticipantsAsync();
        IQueryable<ParticipantViewModel> GetAllParticipants();
        Task<Participant> InsertAsync(ParticipantViewModel model);
        Participant Insert(ParticipantViewModel model);
        Task<ParticipantViewModel> UpdateAsync(string key,ParticipantViewModel model);
    }
    public class ParticipantService : Service<Participant>, IParticipantService
    {
        public ParticipantService(IRepositoryAsync<Participant> repository) : base(repository)
        {

        }
        public Task<IQueryable<ParticipantViewModel>> GetAllParticipantsAsync()
        {
            return Task.Run(() => GetAllParticipants());
        }
        public IQueryable<ParticipantViewModel> GetAllParticipants()
        {
            return _repository.Queryable().Where(x => x.Delete == false)
               .Select(x => new ParticipantViewModel()
               {
                   Id = x.Id,
                   CreateDate = x.CreateDate,
                   Delete = x.Delete,
                   LastModifiedDate = x.LastModifiedDate,
                   Name = x.Name,
                   Email = x.Email,
                   Score = x.Score,
                   TimeSpent = x.TimeSpent
               });
        }
        public Task<Participant> InsertAsync(ParticipantViewModel model)
        {
            return Task.Run(() => Insert(model));
        }
        public Participant Insert(ParticipantViewModel model)
        {
            Participant newParticipant = new Participant()
            {
                Name = model.Name,
                Email = model.Email,
                
                Delete = false,
                CreateDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,

            };
            base.Insert(newParticipant);
            return newParticipant;
        }

        public async Task<ParticipantViewModel> UpdateAsync(string key, ParticipantViewModel model)
        {
            bool result = await Task.Run(() => Update(key,model));
            return model;
        }

        private bool Update(string key, ParticipantViewModel model)
        {
            var data = Find(model.Id);
            if(data != null)
            {
                data.Score = model.Score;
                data.TimeSpent = model.TimeSpent;
                data.LastModifiedDate = DateTime.Now;
                return true;
            }
            return false;
        }
    }
}
