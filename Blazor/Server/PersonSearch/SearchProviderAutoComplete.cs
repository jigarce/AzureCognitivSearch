using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;

namespace BlazorAzureSearch.Server;

public class SearchProviderAutoComplete
{
    private readonly SearchClient _searchClient;
    private readonly string? _index;

    public SearchProviderAutoComplete(IConfiguration configuration)
    {
        _index = configuration["PersonCitiesIndexName"];

        Uri serviceEndpoint = new Uri(configuration["PersonCitiesSearchUri"]);
        AzureKeyCredential credential = new AzureKeyCredential(configuration["PersonCitiesSearchApiKey"]);
        _searchClient = new SearchClient(serviceEndpoint, _index, credential);

    }

    public async Task<SuggestResults<PersonCity>> Suggest(
        bool highlights, bool fuzzy, string term)
    {
        SuggestOptions sp = new SuggestOptions()
        {
            UseFuzzyMatching = fuzzy,
            Size = 5,
        };
        sp.Select.Add("Id");
        sp.Select.Add("Name");
        sp.Select.Add("FamilyName");
        sp.Select.Add("Info");
        sp.Select.Add("CityCountry");
        sp.Select.Add("Web");

        if (highlights)
        {
            sp.HighlightPreTag = "<b>";
            sp.HighlightPostTag = "</b>";
        }

        var suggestResults = await _searchClient.SuggestAsync<PersonCity>(term, "personSg", sp).ConfigureAwait(false);
        return suggestResults.Value;
    }
}
