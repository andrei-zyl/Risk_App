using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Data.Entities;

namespace Data.EFContext
{
    class SourceRiskInitializer : CreateDatabaseIfNotExists<SourceRiskContext>
    {
        protected override void Seed(SourceRiskContext context)
        {
            context.SourceRisks.AddRange(new SourceRisk[] {
                new SourceRisk { SourceRiskName = "Риск персонала" },
                new SourceRisk { SourceRiskName = "Риск ИТ" },
                new SourceRisk { SourceRiskName = "Методологический риск" },
                new SourceRisk { SourceRiskName = "Внешний риск" },


            });
            context.Incidents.AddRange(new Incident[]
            {
                
                        new Incident { 
                            DateOfIncident=new DateTime (2020, 1,4),
                            Description="Неверно составлен договор лизинга, указана процентная ставка, соответствующая утратившему силу тарифному плану",
                            DirectLoss=500,
                            PotentialLoss=500, 
                            Mark=3, 
                            Measures="Обновить справочник тарифов на корпоративном сайте, обратить внимание работников на вступившие в силу обновления",
                            ObjectRiskId=1,
                            SourceRiskId=1,
                            Status = "Урегулирован",
                            LossId=3,
                            UnitId=1},

                        new Incident { 
                            DateOfIncident=new DateTime (2020, 1,6),
                            Description="Излишне начислена премия Иванову С.П. за декабрь 2019 г.",
                            DirectLoss=0,
                            PotentialLoss=700,
                            Mark=2,
                            Measures="Произведен перерасчет, Иванов С.П. уведомлен",
                            Status = "Урегулирован",
                            ObjectRiskId=6,
                            SourceRiskId=1,
                            LossId=5,
                            UnitId=6},

                        new Incident {
                            DateOfIncident=new DateTime (2020, 1,15),
                            Description="В договоре международного лизинга не прописаны штрафные санкции с учетом резидентства лизингополучателя",
                            DirectLoss=0,
                            PotentialLoss=1500,
                            Mark=3,
                            Measures="Изменения в договор внесены, обратить внимание работников на тщательное изучения вопросов резидентства",
                            Status = "Урегулирован",
                            ObjectRiskId=2,
                            SourceRiskId=1,
                            LossId=3,
                            UnitId=2},

                        new Incident {
                            DateOfIncident=new DateTime (2020, 2,15),
                            Description="В договоре лизинга указана неверная сумма процентов",
                            DirectLoss=0,
                            PotentialLoss=2100,
                            Mark=3,
                            Measures="Изменения в договор внесены, с работником проведена консультация",
                            Status = "Урегулирован",
                            ObjectRiskId=1,
                            SourceRiskId=1,
                            LossId=3,
                            UnitId=1},

                    
               
                
                        new Incident { 
                            DateOfIncident=new DateTime (2020, 1,27),
                            Description="Вышел из строя ПК в Юридическом отделе",
                            DirectLoss=1500,
                            PotentialLoss=1500,
                            Mark=3,
                            Measures="ПК восстановлению не подлежит, причина поломки не выявлена",
                            Status = "Урегулирован",
                            ObjectRiskId=4,
                            SourceRiskId=2,
                            LossId=2,
                            UnitId=10},

                        new Incident { 
                            DateOfIncident=new DateTime (2020, 2,22),
                            Description="Вышел из строя сервер, была парализована работа 5 подразделений в течение 3 часов",
                            DirectLoss=400,
                            PotentialLoss=4000,
                            Mark=4,
                            Measures="Отремонтирован северный мост. Отделу ИТ поручено разработать дополнительные меры безопасности",
                            Status = "Урегулирован",
                            ObjectRiskId=4,
                            SourceRiskId=2,
                            LossId=4,
                            UnitId=4},

                        new Incident {
                            DateOfIncident=new DateTime (2020, 1,27),
                            Description="Вышла из строя 1С",
                            DirectLoss=300,
                            PotentialLoss=1500,
                            Mark=3,
                            Measures="Осуществлена доработка модуля программы",
                            Status = "Урегулирован",
                            ObjectRiskId=4,
                            SourceRiskId=2,
                            LossId=4,
                            UnitId=6},

                        new Incident {
                            DateOfIncident=new DateTime (2020, 3,4),
                            Description="Вышел из строя принтер",
                            DirectLoss=0,
                            PotentialLoss=300,
                            Mark=1,
                            Measures="Осуществлен осмотр и ремонт на месте",
                            Status = "Урегулирован",
                            ObjectRiskId=4,
                            SourceRiskId=2,
                            LossId=4,
                            UnitId=8},
                    
                        new Incident {
                            DateOfIncident=new DateTime (2020, 4,7),
                            Description="При разработке тарифа 'Супер-лизинг' не учтены повышенные кредитные риски",
                            DirectLoss=0,
                            PotentialLoss=4000,
                            Mark=4,
                            Measures="Проведено заседание Дирекции, условия тарифа откорректированы",
                            Status = "Урегулирован",
                            ObjectRiskId=1,
                            SourceRiskId=3,
                            LossId=3,
                            UnitId=1},                        
                 

                
                        new Incident {
                            DateOfIncident=new DateTime (2020, 5,7),
                            Description="Заключен договор лизинга по поддельным документам. Злоумышленник не найден",
                            DirectLoss=600,
                            PotentialLoss=10000,
                            Mark=5,
                            Measures="Автомобиль удалось вернуть, уплачены расходы по возврату. Начальнику Отдела безопасности объявлен выговор",
                            Status = "Урегулирован",
                            ObjectRiskId=5,
                            SourceRiskId=4,
                            LossId=5,
                            UnitId=5},
                   
            });
            context.ObjectRisks.AddRange(new ObjectRisk[] 
            { 
                new ObjectRisk { ObjectRiskName = "Внутренний лизинг" },
                new ObjectRisk { ObjectRiskName = "Международный лизинг" },
                new ObjectRisk { ObjectRiskName = "Хозяйственное обеспечение" },
                new ObjectRisk { ObjectRiskName = "Информационное обеспечение" },
                new ObjectRisk { ObjectRiskName = "Обеспечение безопасности"},
                new ObjectRisk { ObjectRiskName = "Бухгалтерский учет"},
                new ObjectRisk { ObjectRiskName = "Казначейские операции"},
                new ObjectRisk { ObjectRiskName = "Риск-менеджмент"},
                new ObjectRisk { ObjectRiskName = "Возврат имущества"},
                new ObjectRisk { ObjectRiskName = "Юридическое сопровождение"},
                new ObjectRisk { ObjectRiskName = "Работа с персоналом"},});

            context.Losses.AddRange(new Loss[] { 
                new Loss { LossName = "Штрафы и взыскания" }, 
                new Loss { LossName = "Порча имущества" }, 
                new Loss { LossName = "Недополученные платежи" },
                new Loss { LossName = "Поломка оборудования и ПО" },
                new Loss { LossName = "Излишние выплаты" },
            });
            

            context.Units.AddRange(new Unit[] 
            { 
                new Unit { UnitName = "Управление внутреннего лизинга" },
                new Unit { UnitName = "Управление международного лизинга" },
                new Unit { UnitName = "Хозяйственный отдел"},
                new Unit { UnitName = "Отдел ИТ" },
                new Unit { UnitName = "Отдел безопасности" },
                new Unit { UnitName = "Бухгалтерское управление"},
                new Unit { UnitName = "Финансовое управление"},
                new Unit { UnitName = "Управление рисков"},
                new Unit { UnitName = "Отдел возврата имущества"},
                new Unit { UnitName = "Юридический отдел"},
                new Unit { UnitName = "Отдел персонала"} });

            context.Mark_frequencys.AddRange(new Mark_frequency[] 
            { 
                new Mark_frequency { Mark_frequencyName = "Инцидент произошел впервые", Mark_frequencyRange="0 - 1.0", Mark_frequencyValue=1},
                new Mark_frequency { Mark_frequencyName = "Инцидент происходил в течение года", Mark_frequencyRange = "1.0 - 2.0", Mark_frequencyValue=2 },
                new Mark_frequency { Mark_frequencyName = "Инцидент происходил в течение года от 2 до 4 раз", Mark_frequencyRange = "2.0 - 3.0", Mark_frequencyValue=3 },
                new Mark_frequency { Mark_frequencyName = "Инцидент происходил в течение года от 5 до 7 раз", Mark_frequencyRange = "3.0 - 4.0", Mark_frequencyValue=4 },
                new Mark_frequency { Mark_frequencyName = "Инцидент происходил в течение года от 8 раз и чаще", Mark_frequencyRange = "4.0 - 5.0", Mark_frequencyValue=5 } });

            context.Mark_losss.AddRange(new Mark_loss[] { 
                new Mark_loss { Mark_lossName = "Прямые потери не зафиксированы", Mark_lossRange = "0 - 1.0", Mark_lossValue=1 }, 
                new Mark_loss { Mark_lossName = "Потери зафиксированы от 1 до 1 000 руб.", Mark_lossRange= "1.0 - 2.0", Mark_lossValue=2 }, 
                new Mark_loss { Mark_lossName = "Потери зафиксированы от 1 000 до 20 000 руб.", Mark_lossRange = "2.0 - 3.0", Mark_lossValue=3 }, 
                new Mark_loss { Mark_lossName = "Потери зафиксированы от 20 000 руб. до 30 000 руб.", Mark_lossRange = "3.0 - 4.0", Mark_lossValue=4 }, 
                new Mark_loss { Mark_lossName = "Потери зафиксированы от 30 000 руб.", Mark_lossRange = "4.0 - 5.0", Mark_lossValue=5 } });

            context.Mark_quantitys.AddRange(new Mark_quantity[] { 
                new Mark_quantity { Mark_quantityName = "Инцидент затронул 1 подразделение", Mark_quantityRange= "0 - 1.0", Mark_quantityValue=1 }, 
                new Mark_quantity { Mark_quantityName = "Инцидент затронул 2 подразделения", Mark_quantityRange= "1.0 - 2.0", Mark_quantityValue=2 },
                new Mark_quantity { Mark_quantityName = "Инцидент затронул от 3 до 4 подразделений", Mark_quantityRange= "2.0 - 3.0", Mark_quantityValue=3 }, 
                new Mark_quantity { Mark_quantityName = "Инцидент затронул от 5 до 6 подразделений", Mark_quantityRange= "3.0 - 4.0", Mark_quantityValue=4 },
                new Mark_quantity { Mark_quantityName = "Инцидент затронул более 6 подразделений", Mark_quantityRange= "4.0 - 5.0", Mark_quantityValue=5 } });

            context.Mark_times.AddRange(new Mark_time[] { 
                new Mark_time { Mark_timeName = "Инцидент длился до 5 минут", Mark_timeRange= "0 - 1.0", Mark_timeValue=1 }, 
                new Mark_time { Mark_timeName = "Инцидент длился от 5 до 30 минут", Mark_timeRange = "1.0 - 2.0", Mark_timeValue=2 }, 
                new Mark_time { Mark_timeName = "Инцидент длился от 30 минут до 1 часа", Mark_timeRange = "2.0 - 3.0", Mark_timeValue=3 },
                new Mark_time { Mark_timeName = "Инцидент длился от 1 часа до 2 часов", Mark_timeRange = "3.0 - 4.0", Mark_timeValue=4 }, 
                new Mark_time { Mark_timeName = "Инцидент длился свыше 2 часов", Mark_timeRange = "4.0 - 5.0", Mark_timeValue=5 } });

            context.SaveChanges();
        }
    }
}