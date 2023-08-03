using System.Collections.Generic;

public class APIUrls
{
    private static readonly string url = "https://bb46-101-235-202-157.ngrok-free.app/";
    public static readonly string VersionApi = url + "Version";
    public static readonly string MasterDataApi = url + "MasterData";
    public static readonly string CreateAccountApi = url + "CreateAccount";
    public static readonly string LoginApi = url + "Login";
    public static readonly string GameDataApi = url + "GameData";
    public static readonly string RankingApi = url + "Ranking";
    public static readonly string StageApi = url + "Stage";

    private static HashSet<string> validUrls = new HashSet<string>
    {
        VersionApi,
        MasterDataApi,
        CreateAccountApi,
        LoginApi,
        GameDataApi,
        RankingApi,
        StageApi
    };

    public static bool IsValidUrl(string url) => validUrls.Contains(url);
}