namespace ServerApi.Models
{
    public class EbayCall
    {
        public string apiUrl { get; set; }
        public string apiCall { get; set; }
        public string clientId { get; set; }
        public string responseFormat { get; set; }
        public string operationName { get; set; }
        public string keywords { get; set; }
        public bool descriptionSearch { get; set; }
        public int paginationInputEntries { get; set; }
        public string getApiCall()
        {
            return apiUrl +
            "OPERATION-NAME=" + apiCall +
            "&SERVICE-VERSION=1.0.0" +
            "&SECURITY-APPNAME=" + clientId +
            "&RESPONSE-DATA-FORMAT=" + responseFormat +
            "&REST-PAYLOAD" +
            "&keywords=" + keywords +
            "&descriptionSearch=" + descriptionSearch +
            "&paginationInput.entriesPerPage=" + paginationInputEntries;
        }
    }
}