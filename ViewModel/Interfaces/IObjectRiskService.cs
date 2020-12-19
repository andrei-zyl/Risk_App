using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Models;

namespace ViewModel.Interfaces
{
    public interface IObjectRiskService
    {
        ObservableCollection<ObjectRiskViewModel> GetAll();
        ObjectRiskViewModel Get(int id);
        void AddIncidentToObjectRisk(int ObjectriskId, IncidentViewModel incident);
        void RemoveIncidentFromObjectRisk(int ObjectriskId, int incidentId);
        void CreateObjectRisk(ObjectRiskViewModel Objectrisk);
        void DeleteObjectRisk(int ObjectriskId);
        void UpdateObjectRisk(ObjectRiskViewModel Objectrisk);
    }
}
