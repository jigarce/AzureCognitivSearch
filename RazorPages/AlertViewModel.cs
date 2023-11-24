﻿namespace AspNetCoreAzureSearch;

public class AlertViewModel
{
    public string AlertType { get; set; }
    public string AlertTitle { get; set; }
    public string AlertMessage { get; set; }

    public AlertViewModel(string type, string title, string message)
    {
        AlertType = type;
        AlertTitle = title;
        AlertMessage = message;
    }
}
