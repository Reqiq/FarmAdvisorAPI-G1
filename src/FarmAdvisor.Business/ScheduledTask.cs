using FarmAdvisor.DataAccess.MSSQL.Entities;
using FarmAdvisor.DataAccess.MSSQL.Functions.Crud;
namespace FarmAdvisor.Business
{
    
    public class ScheduledTask
    {
        public Crud _Crud { get; set; }
        public SensorsLogic sensorsLogic { get; set; }
        public ScheduledTask(Crud Crud, SensorsLogic sensorsLogic) {
            _Crud= Crud;
            this.sensorsLogic= sensorsLogic;
        }
        public async Task Run()
        {
            Field field1 = new Field();
            field1.FieldId = Guid.NewGuid();
            field1.Name = "field 1";
            field1.Alt = 4150;

            Sensor sensor1 = new Sensor();
            sensor1.SerialNumber = "ADR12823129";
            sensor1.LastCommunication = DateTime.Now.AddDays(-2);
            sensor1.BatteryStatus = 1;
            sensor1.OptimalGDD = 85;
            sensor1.CuttingDateTimeCalculated = DateTime.Now.AddDays(17);
            sensor1.LastForecastDate = DateTime.Now.AddDays(17);
            sensor1.Lat = -16.516667;
            sensor1.Long = -68.166667;
            sensor1.Field = field1;

            List<Sensor> sensorList = new List<Sensor>() { sensor1};
            //List<Sensor> sensorList = await _Crud.FindAll<Sensor>();
            await sensorsLogic.Run(sensorList);
        }
    }
}
