using System;
using System.Collections.ObjectModel;
using ViewModel.Interfaces;
using Data.Entities;
using ViewModel.Models;
using Data.Interfaces;
using Data.Repositories;
using AutoMapper;

namespace ViewModel.Services
{
    public class Mark_lossService : IMark_lossService
    {
        IUnitOfWork dataBase;
        public Mark_lossService(string name)
        {
            dataBase = new EntityUnitOfWork(name);
        }
        public void AddIncidentToMark_loss(int Mark_lossId, IncidentViewModel incidentVM)
        {
            var Mark_loss = dataBase.Mark_losss.Get(Mark_lossId);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<IncidentViewModel, Incident>());
            var mapper = config.CreateMapper();
            var incident = mapper.Map<Incident>(incidentVM);
            Mark_loss.Incidents.Add(incident);
            dataBase.Save();
        }
        public void CreateMark_loss(Mark_lossViewModel Mark_loss)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mark_lossViewModel, Mark_loss>());
            var mapper = config.CreateMapper();
            Mark_loss Mark_loss_new = mapper.Map<Mark_loss>(Mark_loss);
            dataBase.Mark_losss.Create(Mark_loss_new);
            dataBase.Save();
        }
        public void DeleteMark_loss(int Mark_lossId)
        {
            dataBase.Mark_losss.Delete(Mark_lossId);
            dataBase.Save();
        }
        public Mark_lossViewModel Get(int id)
        {
            throw new NotImplementedException();
        }
        public ObservableCollection<Mark_lossViewModel> GetAll()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Mark_loss, Mark_lossViewModel>();
                cfg.CreateMap<Incident, IncidentViewModel>();
            });
            var mapper = config.CreateMapper();
            var Mark_losss = mapper.Map<ObservableCollection<Mark_lossViewModel>>(dataBase.Mark_losss.GetAll());
            return Mark_losss;
        }
        public void RemoveIncidentFromMark_loss(int Mark_lossId, int incidentId)
        {
            Mark_loss Mark_loss = dataBase.Mark_losss.Get(Mark_lossId);
            Mark_loss.Incidents.Remove(dataBase.Incidents.Get(incidentId));
            dataBase.Mark_losss.Update(Mark_loss);
            dataBase.Incidents.Delete(incidentId);
            dataBase.Save();
        }
        public void UpdateMark_loss(Mark_lossViewModel Mark_lossVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mark_lossViewModel, Mark_loss>());
            var mapper = config.CreateMapper();
            Mark_loss Mark_loss_new = mapper.Map<Mark_loss>(Mark_lossVM);
            dataBase.Mark_losss.Update(mapper.Map<Mark_loss>(Mark_loss_new));
            dataBase.Save();
        }
    }
}
