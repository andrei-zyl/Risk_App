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
    public class SourceRiskService : ISourceRiskService
    {
        IUnitOfWork dataBase;
        public SourceRiskService (string name)
        {
            dataBase = new EntityUnitOfWork(name);
        }
        public void AddIncidentToSourceRisk(int sourceriskId, IncidentViewModel incidentVM)
        {
            var sourcerisk = dataBase.SourceRisks.Get(sourceriskId);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<IncidentViewModel, Incident>());
            var mapper = config.CreateMapper();
            var incident = mapper.Map<Incident>(incidentVM);
            sourcerisk.Incidents.Add(incident);
            dataBase.Save();
        }
        public void CreateSourceRisk(SourceRiskViewModel sourcerisk)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<SourceRiskViewModel, SourceRisk>());
            var mapper = config.CreateMapper();
            SourceRisk sourcerisk_new = mapper.Map<SourceRisk>(sourcerisk);
            dataBase.SourceRisks.Create(sourcerisk_new);
            dataBase.Save();
        }
        public void DeleteSourceRisk(int sourceriskId)
        {
            dataBase.SourceRisks.Delete(sourceriskId);
            dataBase.Save();
        }
        public SourceRiskViewModel Get(int id)
        {
            throw new NotImplementedException();
        }
        public ObservableCollection<SourceRiskViewModel> GetAll()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<SourceRisk, SourceRiskViewModel>();
                cfg.CreateMap<Incident, IncidentViewModel>();
            });
            var mapper = config.CreateMapper();
            var sourcerisks = mapper.Map<ObservableCollection<SourceRiskViewModel>>(dataBase.SourceRisks.GetAll());
            return sourcerisks;
        }
        public void RemoveIncidentFromSourceRisk(int sourceriskId, int incidentId)
        {
            SourceRisk sourcerisk = dataBase.SourceRisks.Get(sourceriskId);
            sourcerisk.Incidents.Remove(dataBase.Incidents.Get(incidentId));
            dataBase.SourceRisks.Update(sourcerisk);
            dataBase.Incidents.Delete(incidentId);
            dataBase.Save();
        }
        public void UpdateSourceRisk(SourceRiskViewModel sourceriskVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<SourceRiskViewModel, SourceRisk>());
            var mapper = config.CreateMapper();
            SourceRisk sourcerisk_new = mapper.Map<SourceRisk>(sourceriskVM);
            dataBase.SourceRisks.Update(mapper.Map<SourceRisk>(sourcerisk_new));
            dataBase.Save();
        }
    }
}
