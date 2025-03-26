namespace PublicTransportApp.Models.Stops
{
    public class Transfer
    {
        public string TransferStopId { get; set; }
        public int TransferSure { get; set; }
        public float TransferUcret { get; set; }   // it was double    
    }
}
