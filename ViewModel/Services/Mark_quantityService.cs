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
    public class Mark_quantityService : IMark_quantityService
    {
        IUnitOfWork dataBase;
        public Mark_quantityService(string name)
        {
            dataBase = new EntityUnitOfWork(name);
        }
        public void AddIncidentToMark_quantity(int Mark_quantityId, IncidentViewModel incidentVM)
        {
            var Mark_quantity = dataBase.Mark_quantitys.Get(Mark_quantityId);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<IncidentViewModel, Incident>());
            var mapper = config.CreateMapper();
            var incident = mapper.Map<Incident>(incidentVM);
            Mark_quantity.Incidents.Add(incident);
            dataBase.Save();
        }
        public void CreateMark_quantity(Mark_quantityViewModel Mark_quantity)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mark_quantityViewModel, Mark_quantity>());
            var mapper = config.CreateMapper();
            Mark_quantity Mark_quantity_new = mapper.Map<Mark_quantity>(Mark_quantity);
            dataBase.Mark_quantitys.Create(Mark_quantity_new);
            dataBase.Save();
        }
        public void DeleteMark_quantity(int Mark_quantityId)
        {
            dataBase.Mark_quantitys.Delete(Mark_quantityId);
            dataBase.Save();
        }
        public Mark_quantityViewModel Get(int id)
        {
            throw new NotImplementedException();
        }
        public ObservableCollection<Mark_quantityViewModel> GetAll()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Mark_quantity, Mark_quantityViewModel>();
                cfg.CreateMap<Incident, IncidentViewModel>();
            });
            var mapper = config.CreateMapper();
            var Mark_quantitys = mapper.Map<ObservableCollection<Mark_quantityViewModel>>(dataBase.Mark_quantitys.GetAll());
            return Mark_quantitys;
        }
        public void RemoveIncidentFromMark_quantity(int Mark_quantityId, int incidentId)
        {
            Mark_quantity Mark_quantity = dataBase.Mark_quantitys.Get(Mark_quantityId);
            Mark_quantity.Incidents.Remove(dataBase.Incidents.Get(incidentId));
            dataBase.Mark_quantitys.Update(Mark_quantity);
            dataBase.Incidents.Delete(incidentId);
            dataBase.Save();
        }
        public void UpdateMark_quantity(Mark_quantityViewModel Mark_quantityVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mark_quantityViewModel, Mark_quantity>());
            var mapper = config.CreateMapper();
            Mark_quantity Mark_quantity_new = mapper.Map<Mark_quantity>(Mark_quantityVM);
            dataBase.Mark_quantitys.Update(mapper.Map<Mark_quantity>(Mark_quantity_new));
            dataBase.Save();
        }
    }
}
