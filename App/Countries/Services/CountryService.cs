using AutoMapper;
using JWT53.MyEnum;
using LinqKit;
using System.Linq.Expressions;
using LapisApi.App.Countries.Dto;
using LapisApi.App.Countries.Dto.Request.Commands;
using LapisApi.App.Countries.Errors;
using LapisApi.App.Countries.Model;
using LapisApi.Data.Interfaces;
using LapisApi.Data.Models;
using LapisApi.Helpers;
using LapisApi.Helpers.Responses;
using LapisApi.Interfaces.Countries;
using LapisApi.MyEnum.CitySort;
namespace LapisApi.Services.Countries;

public class CountryService : ICountryService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public async Task<Result<CountryResponse>> AddCountryAsync(CountryCreateCommand dto)
  {
    var Country = _mapper.Map<Country>(dto);

    await _unitOfWork.Countries.AddAsync(Country);
    await _unitOfWork.SaveChangesAsync();

    var data = _mapper.Map<CountryResponse>(Country);
    return Result<CountryResponse>.Success(data);
  }

  public async Task<Result<IEnumerable<CountryResponse>>> GetAllCountriesAsync(CountryGetAllQuery query)
  {
    Expression<Func<Country, bool>> predicate = c =>
      (string.IsNullOrEmpty(query.Search) ||
       c.NameAr.Contains(query.Search) ||
       c.NameEn.ToLower().Contains(query.Search.ToLower())
      );

    if (query.IsActive != null)
    {
      predicate = predicate.And(c => c.IsActive == query.IsActive);
    }

    if (query.IsAutomaticAcceptance != null)
    {
      predicate = predicate.And(c => c.IsAutomaticAcceptance == query.IsAutomaticAcceptance);
    }

    var sortFunc = SortHelper.BuildSort<Country, CountrySortField>(query.Sort);

    var pagedResult =
      await _unitOfWork.Countries.GetPagedAsync(
        predicate: predicate,
        pageNumber: query.PageNumber,
        pageSize: query.PageSize,
        sort: sortFunc
      );

    var data = _mapper.Map<IEnumerable<CountryResponse>>(pagedResult.Data);

    var paging = new AppPaging
    {
      PageNumber = query.PageNumber,
      PageSize = query.PageSize,
      TotalRecords = pagedResult.TotalRecords
    };

    return Result<IEnumerable<CountryResponse>>.Success(data, paging);
  }

  public async Task<Result<IEnumerable<CountryAutoCompleteResponse>>> GetAutoComplete(
    CountryGetAutoCompleteQuery query
  )
  {
    Expression<Func<Country, bool>> predicate =
      c =>
      (
        string.IsNullOrEmpty(query.Search)
        ||
        c.NameAr.Contains(query.Search)
        ||
        c.NameEn.ToLower().Contains(query.Search.ToLower())
      );

    predicate = predicate.And(c => c.IsActive);

    var pagedResult = await _unitOfWork.Countries.GetPagedAsync(
      predicate: predicate,
      pageNumber: query.PageNumber,
      pageSize: query.PageSize
    );

    var data = _mapper.Map<IEnumerable<CountryAutoCompleteResponse>>(pagedResult.Data);

    var paging = new AppPaging
    {
      PageNumber = query.PageNumber,
      PageSize = query.PageSize,
      TotalRecords = pagedResult.TotalRecords
    };

    return Result<IEnumerable<CountryAutoCompleteResponse>>.Success(data, paging);
  }

  public async Task<Result<CountryResponse>> GetCountryByIdAsync(int id)
  {
    var Country = await _unitOfWork.Countries.GetByIdAsync(id);
    if (Country == null)
    {
      return Result<CountryResponse>.Failure(CountryErrors.NotFound);
    }

    var data = _mapper.Map<CountryResponse>(Country);
    return Result<CountryResponse>.Success(data);
  }

  public async Task<Result<CountryResponse>> UpdateCountryAsync(int id, UpdateCountryCommand countryCommand)
  {
    var Country = await _unitOfWork.Countries.GetByIdAsync(id);
    if (Country == null)
    {
      return Result<CountryResponse>.Failure(CountryErrors.NotFound);
    }

    _mapper.Map(countryCommand, Country);
    await _unitOfWork.Countries.UpdateAsync(Country);
    await _unitOfWork.SaveChangesAsync();

    return Result<CountryResponse>.Success(_mapper.Map<CountryResponse>(Country));
  }

  public async Task<Result<object>> DeleteCountryAsync(int id)
  {
    var Country = await _unitOfWork.Countries.GetByIdAsync(id);
    if (Country == null)
    {
      return Result<object>.Failure(CountryErrors.NotFound);
    }

    await _unitOfWork.Countries.RemoveAsync(Country);
    await _unitOfWork.SaveChangesAsync();

    return Result<object>.Success(null);
  }
}