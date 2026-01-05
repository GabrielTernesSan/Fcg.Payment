using Fcg.Payment.Application.Responses;
using Fcg.Payment.Common.Responses;
using Fcg.Payment.Domain.Entities;
using Fcg.Payment.Domain.Enums;
using Fcg.Payment.Domain.Repositories;
using Fcg.Payment.Proxy.Auth.Client.Interfaces;
using Fcg.Payment.Proxy.Games.Client.Interfaces;
using Fcg.Payment.Proxy.User.Client.Interfaces;

namespace Fcg.Payment.Application.Services
{
    public class PaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IClientAuth _clientAuth;
        private readonly IClientUser _clientUser;
        private readonly IClientGames _clientGame;

        public PaymentService(
            IPaymentRepository paymentRepository,
            IClientAuth clientAuth,
            IClientUser clientUser,
            IClientGames clientGame)
        {
            _paymentRepository = paymentRepository;
            _clientAuth = clientAuth;
            _clientUser = clientUser;
            _clientGame = clientGame;
        }

        public async Task<Response<CheckoutResponse>> CheckoutAsync(Guid userId, decimal amount, CancellationToken cancellationToken = default)
        {
            var response = new Response<CheckoutResponse>();

            var authCheck = await _clientAuth.GetAuthUserAsync(userId);

            if (authCheck.Result == null || authCheck.Erros.Any())
            {
                response.AddError("Usuário inexistente no serviço de autenticação.");
                return response;
            }

            var payment = new PaymentTransaction(userId, amount);

            await _paymentRepository.AddAsync(payment, cancellationToken);

            response.Result = new CheckoutResponse
            {
                PaymentId = payment.Id,
                Status = payment.Status.ToString(),
                CreatedAt = payment.CreatedAt
            };

            return response;
        }

        public async Task<Response<bool>> ApproveAsync(Guid paymentId, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            var payment = await _paymentRepository.GetByIdAsync(paymentId, cancellationToken);

            if (payment is null || payment.Status != PaymentStatus.Pending)
            {
                response.AddError("Pagamento não encontrado ou não está pendente.");
                return response;
            }

            payment.Approve();
            await _paymentRepository.UpdateAsync(payment, cancellationToken);

            var integrationResult = await _clientUser.AddBalanceAsync(payment.UserId, payment.Amount);

            if (!integrationResult)
            {
                response.AddError("Pagamento aprovado localmente, mas falhou ao sincronizar saldo com Fcg.User.");
                return response;
            }

            response.Result = true;
            return response;
        }

        public async Task<Response<bool>> RejectAsync(Guid paymentId, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            var payment = await _paymentRepository.GetByIdAsync(paymentId, cancellationToken);

            if (payment is null || payment.Status != PaymentStatus.Pending)
            {
                response.AddError("Pagamento não encontrado ou já processado.");
                return response;
            }

            payment.Reject();
            await _paymentRepository.UpdateAsync(payment, cancellationToken);

            response.Result = true;
            return response;
        }

        public async Task<Response<PaymentResponse>> GetByIdAsync(Guid paymentId, CancellationToken cancellationToken = default)
        {
            var response = new Response<PaymentResponse>();

            var payment = await _paymentRepository.GetByIdAsync(paymentId, cancellationToken);

            if (payment is null)
            {
                response.AddError("Pagamento não encontrado.");
                return response;
            }

            response.Result = PaymentResponse.FromEntity(payment);
            return response;
        }

        public async Task<Response<CheckoutResponse>> PurchaseGameAsync(Guid userId, Guid gameId, CancellationToken cancellationToken = default)
        {
            var response = new Response<CheckoutResponse>();

            var userCheck = await _clientUser.GetUserAsync(userId);

            if (userCheck.HasErrors)
            {
                response.AddError("Usuário não encontrado no serviço Fcg.User.");
                return response;
            }

            var gameCheck = await _clientGame.GetGameAsync(gameId);

            if (gameCheck.HasErrors)
            {
                response.AddError("Game não encontrado no serviço Fcg.Games.");
                return response;
            }

            if (userCheck.Result.Wallet < gameCheck.Result.Price)
            {
                response.AddError("Saldo insuficiente para realizar a compra do jogo.");
                return response;
            }

            var payment = new PaymentTransaction(userId, gameCheck.Result.Price, gameId);

            await _paymentRepository.AddAsync(payment, cancellationToken);

            response.Result = new CheckoutResponse
            {
                PaymentId = payment.Id,
                Status = payment.Status.ToString(),
                CreatedAt = payment.CreatedAt
            };

            return response;
        }
    }
}