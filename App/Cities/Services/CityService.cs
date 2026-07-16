using AutoMapper;
using JWT53.MyEnum;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using LapisApi.App.Cities.Dto;
using LapisApi.App.Cities.Enums;
using LapisApi.App.Cities.Errors;
using LapisApi.App.Cities.Interfaces;
using LapisApi.Data.Interfaces;
using LapisApi.Data.Models;
using LapisApi.Dto.City;
using LapisApi.Helpers;
using LapisApi.Helpers.Responses;
using LapisApi.MyEnum.CitySort;

namespace LapisApi.Services.Cities;

public class CityService : ICityService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public CityService(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public async Task<Result<CityResponse>> AddAsync(CityCreateCommand command)
  {
    var city = _mapper.Map<City>(command);

    await _unitOfWork.Cities.AddAsync(city);
    await _unitOfWork.SaveChangesAsync();

    var data = _mapper.Map<CityResponse>(city);
    return Result<CityResponse>.Success(data);
  }



  public async Task<Result<IEnumerable<CityResponse>>> GetAllAsync(CityGetAllQuery query)
  {
    Expression<Func<City, bool>> predicate =
      c =>
      (
        string.IsNullOrEmpty(query.Search)
        ||
        c.NameAr.Contains(query.Search)
        ||
        c.NameEn.ToLower().Contains(query.Search.ToLower())
      );

    if (query.IsActive != null)
    {
      predicate = predicate.And(c => c.IsActive == query.IsActive);
    }

    if (query.CountryId != null)
    {
      predicate = predicate.And(c => c.CountryId == query.CountryId);
    }

    var sortFunc = SortHelper.BuildSort<City, CitySortFieldEnum>(query.Sort);

    var pagedResult = await _unitOfWork.Cities.GetPagedAsync(
      predicate: predicate,
      pageNumber: query.PageNumber,
      pageSize: query.PageSize,
      sort: sortFunc,
      queryBuilder: o => o.Include(o => o.Country)
    );

    var data = _mapper.Map<IEnumerable<CityResponse>>(pagedResult.Data);

    var paging = new AppPaging
    {
      PageNumber = query.PageNumber,
      PageSize = query.PageSize,
      TotalRecords = pagedResult.TotalRecords
    };

    return Result<IEnumerable<CityResponse>>.Success(data, paging);
  }
  
  public async Task<Result<IEnumerable<CityAutoCompleteResponse>>> GetAutoComplete(
    CityGetAutoCompleteQuery query
  )
  {
    Expression<Func<City, bool>> predicate =
      c =>
      (
        string.IsNullOrEmpty(query.Search)
        ||
        c.NameAr.Contains(query.Search)
        ||
        c.NameEn.ToLower().Contains(query.Search.ToLower())
      );

    predicate = predicate.And(c => c.IsActive);
    
    if (query.CountryId != null)
    {
      predicate = predicate.And(c => c.CountryId == query.CountryId);
    }

    var pagedResult = await _unitOfWork.Cities.GetPagedAsync(
      predicate: predicate,
      pageNumber: query.PageNumber,
      pageSize: query.PageSize
    );

    var data = _mapper.Map<IEnumerable<CityAutoCompleteResponse>>(pagedResult.Data);

    var paging = new AppPaging
    {
      PageNumber = query.PageNumber,
      PageSize = query.PageSize,
      TotalRecords = pagedResult.TotalRecords
    };

    return Result<IEnumerable<CityAutoCompleteResponse>>.Success(data, paging);
  }


  public async Task<Result<CityResponse>> GetByIdAsync(int id)
  {
    var city = await _unitOfWork.Cities.GetByIdAsync(id);
    if (city == null)
    {
      return Result<CityResponse>.Failure(CityErrors.NotFound);
    }

    var data = _mapper.Map<CityResponse>(city);
    return Result<CityResponse>.Success(data);
  }

  public async Task<Result<CityResponse>> UpdateAsync(int id, CityUpdateCommand command)
  {
    var city = await _unitOfWork.Cities.GetByIdAsync(id);
    if (city == null)
    {
      return Result<CityResponse>.Failure(CityErrors.NotFound);
    }

    _mapper.Map(command, city);
    await _unitOfWork.Cities.UpdateAsync(city);
    await _unitOfWork.SaveChangesAsync();

    return Result<CityResponse>.Success(_mapper.Map<CityResponse>(city));
  }

  public async Task<Result<object>> DeleteAsync(int id)
  {
    var city = await _unitOfWork.Cities.GetByIdAsync(id);
    if (city == null)
    {
      return Result<object>.Failure(CityErrors.NotFound);
    }

    await _unitOfWork.Cities.RemoveAsync(city);
    await _unitOfWork.SaveChangesAsync();

    return Result<object>.Success(null);
  }
}