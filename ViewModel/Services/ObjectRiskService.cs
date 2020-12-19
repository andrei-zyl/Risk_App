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
    public class ObjectRiskService : IObjectRiskService
    {
        IUnitOfWork dataBase;
        public ObjectRiskService(string name)
        {
            dataBase = new EntityUnitOfWork(name);
        }
        public void AddIncidentToObjectRisk(int ObjectriskId, IncidentViewModel incidentVM)
        {
            var Objectrisk = dataBase.ObjectRisks.Get(ObjectriskId);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<IncidentViewModel, Incident>());
            var mapper = config.CreateMapper();
            var incident = mapper.Map<Incident>(incidentVM);
            Objectrisk.Incidents.Add(incident);
            dataBase.Save();
        }
        public void CreateObjectRisk(ObjectRiskViewModel Objectrisk)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ObjectRiskViewModel, ObjectRisk>());
            var mapper = config.CreateMapper();
            ObjectRisk Objectrisk_new = mapper.Map<ObjectRisk>(Objectrisk);
            dataBase.ObjectRisks.Create(Objectrisk_new);
            dataBase.Save();
        }
        public void DeleteObjectRisk(int ObjectriskId)
        {
            dataBase.ObjectRisks.Delete(ObjectriskId);
            dataBase.Save();
        }
        public ObjectRiskViewModel Get(int id)
        {
            throw new NotImplementedException();
        }
        public ObservableCollection<ObjectRiskViewModel> GetAll()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ObjectRisk, ObjectRiskViewModel>();
                cfg.CreateMap<Incident, IncidentViewModel>();
            });
            var mapper = config.CreateMapper();
            var Objectrisks = mapper.Map<ObservableCollection<ObjectRiskViewModel>>(dataBase.ObjectRisks.GetAll());
            return Objectrisks;
        }
        public void RemoveIncidentFromObjectRisk(int ObjectriskId, int incidentId)
        {
            ObjectRisk Objectrisk = dataBase.ObjectRisks.Get(ObjectriskId);
            Objectrisk.Incidents.Remove(dataBase.Incidents.Get(incidentId));
            dataBase.ObjectRisks.Update(Objectrisk);
            dataBase.Incidents.Delete(incidentId);
            dataBase.Save();
        }
        public void UpdateObjectRisk(ObjectRiskViewModel ObjectriskVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ObjectRiskViewModel, ObjectRisk>());
            var mapper = config.CreateMapper();
            ObjectRisk Objectrisk_new = mapper.Map<ObjectRisk>(ObjectriskVM);
            dataBase.ObjectRisks.Update(mapper.Map<ObjectRisk>(Objectrisk_new));
            dataBase.Save();
        }
    }
}
