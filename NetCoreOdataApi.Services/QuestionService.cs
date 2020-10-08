using NetCoreOdataApi.Core.Models.Quiz;
using NetCoreOdataApi.Core.Repositories;
using NetCoreOdataApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreOdataApi.Services
{
    public interface IQuestionService
    {
        Task<IQueryable<QuestionViewModel>> GetAllQuestionsAsync();
        IQueryable<QuestionViewModel> GetAllQuestions();
        Task<IQueryable<QuestionViewModel>> GetAllAnswersAsync();
        IQueryable<QuestionViewModel> GetAllAnswers();
        Task<Question> InsertAsync(QuestionViewModel model);
        Question Insert(QuestionViewModel model);
        Task<QuestionViewModel> UpdateAsync(Guid key, QuestionViewModel model);
    }
    public class QuestionService:Service<Question>, IQuestionService
    {
        public QuestionService(IRepositoryAsync<Question> repository) : base(repository)
        {

        }
        public Task<IQueryable<QuestionViewModel>> GetAllQuestionsAsync()
        {
            return Task.Run(() => GetAllQuestions());
        }
        public IQueryable<QuestionViewModel> GetAllQuestions()
        {
            return this.Queryable().Where(x => x.Delete == false).Select(x => new QuestionViewModel()
            {
                Id = x.Id,
                Content = x.Content,
                //Answer = x.Answer,
                ImageQuestion = x.ImageQuestion,
                Options = new string[] { x.Option1, x.Option2, x.Option3, x.Option4 },
                //CreateDate = x.CreateDate,
                //LastModifiedDate = x.LastModifiedDate,
                Delete = x.Delete
            }).OrderBy(x=> Guid.NewGuid());
        }
        public Task<IQueryable<QuestionViewModel>> GetAllAnswersAsync()
        {
            return Task.Run(() => GetAllAnswers());
        }
        public IQueryable<QuestionViewModel> GetAllAnswers()
        {
            return this.Queryable().Where(x => x.Delete == false).Select(x => new QuestionViewModel()
            {
                Id = x.Id,
                Content = x.Content,
                Answer = x.Answer,
                ImageQuestion = x.ImageQuestion,
                Options = new string[] { x.Option1, x.Option2, x.Option3, x.Option4 },
                Delete = x.Delete
            }).OrderBy(x => Guid.NewGuid());
        }


        public Question Insert(QuestionViewModel model)
        {
            Question newQues = new Question()
            {
                Content = model.Content,
                Answer = model.Answer,
                Option1 = model.Option1,
                Option2 = model.Option2,
                Option3 = model.Option3,
                Option4 = model.Option4,
                ImageQuestion = model.ImageQuestion,
                Delete = false,
                CreateDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
            };
            base.Insert(newQues);
            return newQues;

        }

        public Task<Question> InsertAsync(QuestionViewModel model)
        {
            return Task.Run(() => Insert(model));
        }

        public async Task<QuestionViewModel> UpdateAsync(Guid key, QuestionViewModel model)
        { 
            bool result = await Task.Run(() => Update(key, model));
            return model;
        }
        private bool Update(Guid key, QuestionViewModel model)
        {
            var data = Find(key);
            if (data != null)
            {
                data.Content = model.Content;
                data.Answer = model.Answer;
                data.LastModifiedDate = DateTime.Now;
                return true;
            }
            return false;
        }
    }
}
