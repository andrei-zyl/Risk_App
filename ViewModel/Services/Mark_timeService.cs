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
    public class Mark_timeService : IMark_timeService
    {
        IUnitOfWork dataBase;
        public Mark_timeService(string name)
        {
            dataBase = new EntityUnitOfWork(name);
        }
        public void AddIncidentToMark_time(int Mark_timeId, IncidentViewModel incidentVM)
        {
            var Mark_time = dataBase.Mark_times.Get(Mark_timeId);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<IncidentViewModel, Incident>());
            var mapper = config.CreateMapper();
            var incident = mapper.Map<Incident>(incidentVM);
            Mark_time.Incidents.Add(incident);
            dataBase.Save();
        }
        public void CreateMark_time(Mark_timeViewModel Mark_time)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mark_timeViewModel, Mark_time>());
            var mapper = config.CreateMapper();
            Mark_time Mark_time_new = mapper.Map<Mark_time>(Mark_time);
            dataBase.Mark_times.Create(Mark_time_new);
            dataBase.Save();
        }
        public void DeleteMark_time(int Mark_timeId)
        {
            dataBase.Mark_times.Delete(Mark_timeId);
            dataBase.Save();
        }
        public Mark_timeViewModel Get(int id)
        {
            throw new NotImplementedException();
        }
        public ObservableCollection<Mark_timeViewModel> GetAll()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Mark_time, Mark_timeViewModel>();
                cfg.CreateMap<Incident, IncidentViewModel>();
            });
            var mapper = config.CreateMapper();
            var Mark_times = mapper.Map<ObservableCollection<Mark_timeViewModel>>(dataBase.Mark_times.GetAll());
            return Mark_times;
        }
        public void RemoveIncidentFromMark_time(int Mark_timeId, int incidentId)
        {
            Mark_time Mark_time = dataBase.Mark_times.Get(Mark_timeId);
            Mark_time.Incidents.Remove(dataBase.Incidents.Get(incidentId));
            dataBase.Mark_times.Update(Mark_time);
            dataBase.Incidents.Delete(incidentId);
            dataBase.Save();
        }
        public void UpdateMark_time(Mark_timeViewModel Mark_timeVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mark_timeViewModel, Mark_time>());
            var mapper = config.CreateMapper();
            Mark_time Mark_time_new = mapper.Map<Mark_time>(Mark_timeVM);
            dataBase.Mark_times.Update(mapper.Map<Mark_time>(Mark_time_new));
            dataBase.Save();
        }
    }
}
