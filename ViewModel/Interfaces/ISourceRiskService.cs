using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ViewModel.Models;

namespace ViewModel.Interfaces
{
   public interface ISourceRiskService
    {
        ObservableCollection<SourceRiskViewModel> GetAll();
        SourceRiskViewModel Get(int id);
        void AddIncidentToSourceRisk(int sourceriskId, IncidentViewModel incident);
        void RemoveIncidentFromSourceRisk(int sourceriskId, int incidentId);
        void CreateSourceRisk(SourceRiskViewModel sourcerisk);
        void DeleteSourceRisk(int sourceriskId);
        void UpdateSourceRisk(SourceRiskViewModel sourcerisk);
    }
}
