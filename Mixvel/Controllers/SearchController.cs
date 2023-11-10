using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mixvel.App.Searching;
using Mixvel.Searching;
using Newtonsoft.Json;

namespace Mixvel.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController : ControllerBase
{
    private readonly IService _service;
    private readonly IMapper _mapper;
    private readonly ILogger<SearchController> _logger;

    public SearchController(
        IService service,
        IMapper mapper,
        ILogger<SearchController> logger)
    {
        _service = service;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost(Name = "Search")]
    public async Task<ActionResult<SearchResponse>> Post(
        SearchRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.SearchAsync(
                _mapper.Map<Core.Searching.SearchRequest>(request),
                cancellationToken);

            return Ok(_mapper.Map<SearchResponse>(result));
        }
        catch (Exception e)
        {
            _logger.LogError(
                e,
                "Api unhandled exception {Request}",
                JsonConvert.SerializeObject(request));
            return BadRequest();
        }
    }
}