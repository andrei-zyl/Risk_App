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
    public class Mark_frequencyService : IMark_frequencyService
    {
        IUnitOfWork dataBase;
        public Mark_frequencyService(string name)
        {
            dataBase = new EntityUnitOfWork(name);
        }
        public void AddIncidentToMark_frequency(int Mark_frequencyId, IncidentViewModel incidentVM)
        {
            var Mark_frequency = dataBase.Mark_frequencys.Get(Mark_frequencyId);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<IncidentViewModel, Incident>());
            var mapper = config.CreateMapper();
            var incident = mapper.Map<Incident>(incidentVM);
            Mark_frequency.Incidents.Add(incident);
            dataBase.Save();
        }
        public void CreateMark_frequency(Mark_frequencyViewModel Mark_frequency)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mark_frequencyViewModel, Mark_frequency>());
            var mapper = config.CreateMapper();
            Mark_frequency Mark_frequency_new = mapper.Map<Mark_frequency>(Mark_frequency);
            dataBase.Mark_frequencys.Create(Mark_frequency_new);
            dataBase.Save();
        }
        public void DeleteMark_frequency(int Mark_frequencyId)
        {
            dataBase.Mark_frequencys.Delete(Mark_frequencyId);
            dataBase.Save();
        }
        public Mark_frequencyViewModel Get(int id)
        {
            throw new NotImplementedException();
        }
        public ObservableCollection<Mark_frequencyViewModel> GetAll()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Mark_frequency, Mark_frequencyViewModel>();
                cfg.CreateMap<Incident, IncidentViewModel>();
            });
            var mapper = config.CreateMapper();
            var Mark_frequencys = mapper.Map<ObservableCollection<Mark_frequencyViewModel>>(dataBase.Mark_frequencys.GetAll());
            return Mark_frequencys;
        }
        public void RemoveIncidentFromMark_frequency(int Mark_frequencyId, int incidentId)
        {
            Mark_frequency Mark_frequency = dataBase.Mark_frequencys.Get(Mark_frequencyId);
            Mark_frequency.Incidents.Remove(dataBase.Incidents.Get(incidentId));
            dataBase.Mark_frequencys.Update(Mark_frequency);
            dataBase.Incidents.Delete(incidentId);
            dataBase.Save();
        }
        public void UpdateMark_frequency(Mark_frequencyViewModel Mark_frequencyVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mark_frequencyViewModel, Mark_frequency>());
            var mapper = config.CreateMapper();
            Mark_frequency Mark_frequency_new = mapper.Map<Mark_frequency>(Mark_frequencyVM);
            dataBase.Mark_frequencys.Update(mapper.Map<Mark_frequency>(Mark_frequency_new));
            dataBase.Save();
        }
    }
}
