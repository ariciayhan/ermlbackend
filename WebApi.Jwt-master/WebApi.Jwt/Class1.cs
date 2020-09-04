namespace Iaea.SG.EQUIS.Common.BusinessEntities.ViewModel.AssetManagement.Receive
{
    public class MeasurementInputModel
    {
        public string EquipmentNumber { get; set; }
        public string PersonnelNumber { get; set; }
        public string LocationCode { get; set; }
        public string Measurement { get; set; }
        public string MeasurementDevice { get; set; }
    }

    public class MeasurementSaveReponseViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string[] ValidationMessages { get; set; }
    }
}