using CertificateRequests.Application.DTOs;
using CertificateRequests.Application.Exceptions;
using CertificateRequests.Application.Interfaces;
using CertificateRequests.Domain.Entities;
using CertificateRequests.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CertificateRequests.Application.Services;

public class RequestService(IApplicationDbContext dbContext) : IRequestService
{

    public async Task<Guid> CreateAsync(CreateRequestDto dto)
    {
        var employeeExists = await dbContext.Employees
            .AnyAsync(x => x.Id == dto.EmployeeId);

        if (!employeeExists)
        {
            throw new NotFoundException("Employee not found");
        }

        var duplicateExists = await dbContext.CertificateRequests
            .AnyAsync(x =>
                x.EmployeeId == dto.EmployeeId &&
                x.Type == dto.Type &&
                x.CreatedAt > DateTime.UtcNow.AddMinutes(-1) &&
                x.Status != RequestStatus.Rejected);

        if (duplicateExists)
        {
            throw new BusinessException(
                "Similar request has already been submitted");
        }

        var request = new CertificateRequest
        {
            Id = Guid.NewGuid(),
            EmployeeId = dto.EmployeeId,
            Type = dto.Type,
            CopiesCount = dto.CopiesCount,
            Reason = dto.Reason,
            Status = RequestStatus.Created,
            CreatedAt = DateTime.UtcNow
        };

        dbContext.CertificateRequests.Add(request);

        await dbContext.SaveChangesAsync();

        return request.Id;
    }

    public async Task<List<RequestDto>> GetEmployeeRequestsAsync(Guid employeeId)
    {
        return await dbContext.CertificateRequests
            .Where(x => x.EmployeeId == employeeId)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new RequestDto
            {
                Id = x.Id,
                Type = x.Type,
                CopiesCount = x.CopiesCount,
                Reason = x.Reason,
                Status = x.Status,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<List<RequestDto>> GetAllAsync()
    {
        return await dbContext.CertificateRequests
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new RequestDto
            {
                Id = x.Id,
                Type = x.Type,
                CopiesCount = x.CopiesCount,
                Reason = x.Reason,
                Status = x.Status,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<RequestDetailsDto> GetByIdAsync(Guid id)
    {
        var request = await dbContext.CertificateRequests
            .Include(x => x.Employee)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (request is null)
        {
            throw new NotFoundException("Request not found");
        }

        return new RequestDetailsDto
        {
            Id = request.Id,
            EmployeeName = request.Employee.FullName,
            Type = request.Type,
            CopiesCount = request.CopiesCount,
            Reason = request.Reason,
            Status = request.Status,
            CreatedAt = request.CreatedAt
        };
    }

    public async Task UpdateStatusAsync(Guid id, UpdateStatusDto dto)
    {
        var request = await dbContext.CertificateRequests
            .FirstOrDefaultAsync(x => x.Id == id);

        if (request is null)
        {
            throw new NotFoundException("Request not found");
        }

        ValidateStatusTransition(request.Status, dto.Status);

        var history = new RequestStatusHistory
        {
            Id = Guid.NewGuid(),
            RequestId = request.Id,
            OldStatus = request.Status,
            NewStatus = dto.Status,
            ChangedAt = DateTime.UtcNow
        };

        request.Status = dto.Status;

        dbContext.RequestStatusHistories.Add(history);

        await dbContext.SaveChangesAsync();
    }

    private static void ValidateStatusTransition(
        RequestStatus currentStatus,
        RequestStatus newStatus)
    {
        var allowedTransitions = new Dictionary<RequestStatus, RequestStatus[]>
        {
            [RequestStatus.Created] =
            [
                RequestStatus.InProgress,
                RequestStatus.Rejected
            ],

            [RequestStatus.InProgress] =
            [
                RequestStatus.Completed,
                RequestStatus.Rejected
            ]
        };

        if (!allowedTransitions.TryGetValue(
                currentStatus,
                out var allowedStatuses))
        {
            throw new BusinessException(
                $"Cannot change status from {currentStatus}");
        }

        if (!allowedStatuses.Contains(newStatus))
        {
            throw new BusinessException(
                $"Invalid status transition from {currentStatus} to {newStatus}");
        }
    }
}