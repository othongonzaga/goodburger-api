using goodburger_api.Application.DTOs;
using goodburger_api.Domain.ValueObjects;

namespace goodburger_api.Application.Interfaces;

public interface IOrderPricingCalculator
{
    OrderPricing Calculate(OrderSelection selection);
}