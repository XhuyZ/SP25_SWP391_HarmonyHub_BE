using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Responses;
using Domain.Entities;
using Repository.Interfaces;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementations;

public class TransactionService : ITransactionService
{
    private readonly IMapper _mapper;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAppointmentRepository _appointmentRepository;

    public TransactionService(IMapper mapper, ITransactionRepository transactionRepository)
    {
        _mapper = mapper;
        _transactionRepository = transactionRepository;
        _appointmentRepository = _appointmentRepository;
    }

    public async Task<IEnumerable<TransactionResponse>> GetAllTransactions()
    {
        try
        {
            var result = await _transactionRepository.GetAllTransactions();
            return _mapper.Map<IEnumerable<TransactionResponse>>(result);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<TransactionResponse> GetTransactionByTransactionId(string transactionId)
    {
        try
        {
            var result = await _transactionRepository.GetTransactionByTransactionId(transactionId);
            if (result == null)
                throw new ServiceException(MessageConstants.NOT_FOUND);

            return _mapper.Map<TransactionResponse>(result);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task CreateTransaction(Transaction transaction)
    {
        try
        {
            await _transactionRepository.AddAsync(transaction);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task UpdateTransactionStatus(string transactionId, int status)
    {
        try
        {
            var existingTransaction = await _transactionRepository.GetTransactionByTransactionId(transactionId);
            existingTransaction.Status = status;
            await _transactionRepository.UpdateAsync(existingTransaction);
            // if (status == (int)TransactionStatusEnum.Successful)
            // {
            //     var existingAppointment = await _appointmentRepository.GetByIdAsync((int)existingTransaction.AppointmentId);
            //     existingAppointment.Status = (int)AppointmentStatusEnum.Paid;
            //     await _appointmentRepository.UpdateAsync(existingAppointment);
            // }
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }
}