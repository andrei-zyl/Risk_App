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
    public class LossService : ILossService
    {
        IUnitOfWork dataBase;
        public LossService(string name)
        {
            dataBase = new EntityUnitOfWork(name);
        }
        public void AddIncidentToLoss(int LossId, IncidentViewModel incidentVM)
        {
            var Loss = dataBase.Losses.Get(LossId);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<IncidentViewModel, Incident>());
            var mapper = config.CreateMapper();
            var incident = mapper.Map<Incident>(incidentVM);
            Loss.Incidents.Add(incident);
            dataBase.Save();
        }
        public void CreateLoss(LossViewModel Loss)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<LossViewModel, Loss>());
            var mapper = config.CreateMapper();
            Loss Loss_new = mapper.Map<Loss>(Loss);
            dataBase.Losses.Create(Loss_new);
            dataBase.Save();
        }
        public void DeleteLoss(int LossId)
        {
            dataBase.Losses.Delete(LossId);
            dataBase.Save();
        }
        public LossViewModel Get(int id)
        {
            throw new NotImplementedException();
        }
        public ObservableCollection<LossViewModel> GetAll()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Loss, LossViewModel>();
                cfg.CreateMap<Incident, IncidentViewModel>();
            });
            var mapper = config.CreateMapper();
            var Losses = mapper.Map<ObservableCollection<LossViewModel>>(dataBase.Losses.GetAll());
            return Losses;
        }
        public void RemoveIncidentFromLoss(int LossId, int incidentId)
        {
            Loss Loss = dataBase.Losses.Get(LossId);
            Loss.Incidents.Remove(dataBase.Incidents.Get(incidentId));
            dataBase.Losses.Update(Loss);
            dataBase.Incidents.Delete(incidentId);
            dataBase.Save();
        }
        public void UpdateLoss(LossViewModel LossVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<LossViewModel, Loss>());
            var mapper = config.CreateMapper();
            Loss Loss_new = mapper.Map<Loss>(LossVM);
            dataBase.Losses.Update(mapper.Map<Loss>(Loss_new));
            dataBase.Save();
        }
    }
}
