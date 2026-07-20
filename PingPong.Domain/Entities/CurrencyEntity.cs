using PingPong.Domain.Primitives;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Domain.Entities;

public sealed class CurrencyEntity : Entity<CurrencyId>
{
    private CurrencyEntity(CurrencyId id, string name, string code, string symbol, decimal rate, bool isDefault)
        : base(id)
    {
        Name = name;
        Code = code;
        Symbol = symbol;
        Rate = rate;
        IsDefault = isDefault;
    }

    private CurrencyEntity() { }

    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty; // e.g., USD, EUR
    public string Symbol { get; private set; } = string.Empty; // e.g., $, €
    public decimal Rate { get; private set; }
    public bool IsDefault { get; private set; }

    public static CurrencyEntity Create(string name, string code, string symbol, decimal rate, bool isDefault = false)
    {
        return new CurrencyEntity(CurrencyId.New(), name, code, symbol, rate, isDefault);
    }

    public void Update(string name, string code, string symbol, decimal rate, bool isDefault)
    {
        Name = name;
        Code = code;
        Symbol = symbol;
        Rate = rate;
        IsDefault = isDefault;
    }

    public void SetAsDefault()
    {
        IsDefault = true;
    }

    public void UnsetAsDefault()
    {
        IsDefault = false;
    }
}