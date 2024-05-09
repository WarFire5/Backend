using Backend.Core.Enums;

namespace Backend.Core.Data;

public static class EnumProvider
{
    public static List<CoinType> GetCoinTypesForPc()
    {
        List<CoinType> _pcCoins = [CoinType.Bitcoin, CoinType.Ethereum, CoinType.Litecoin, CoinType.BitcoinCash, CoinType.Monero];
        return _pcCoins;
    }
    public static List<CoinType> GetCoinTypesForLaptop()
    {
        List<CoinType> _laptopCoins = [CoinType.Dash, CoinType.Zcash, CoinType.VertCoin, CoinType.BitShares, CoinType.Factom];
        return _laptopCoins;
    }
    public static List<CoinType> GetCoinTypesForVideoCard()
    {
        List<CoinType> _videoCardCoins = [CoinType.NEM, CoinType.Dogecoin, CoinType.MaidSafeCoin, CoinType.DigiByte, CoinType.Nautiluscoin];
        return _videoCardCoins;
    }
    public static List<CoinType> GetCoinTypesForAsic()
    {
        List<CoinType> _asicCoins = [CoinType.Clams, CoinType.Siacoin, CoinType.Decred, CoinType.VeriCoin, CoinType.Einsteinium];
        return _asicCoins;
    }
}
