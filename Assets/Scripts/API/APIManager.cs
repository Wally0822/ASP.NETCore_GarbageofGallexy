using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using APIModels;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public class APIManager : MonoSingleton<APIManager>
{
    public APIDataSO apidata = null;

    // Game Data
    private string _id;
    private string _authToken;

    // Player Data
    private long _player_uid;
    private int _exp;
    private int _hp;
    private int _score;
    private int _level;
    private int _status;

    // Ranking Data
    private string _rankId;
    private int _rankScore;
    private int _ranking;

    private void Awake()
    {
        if(apidata == null)
        {
            apidata = Resources.Load<APIDataSO>("APIData");
        }
    }

    public async UniTask CreateAccpuntAPI(User user)
    {
        await CallAPI<Dictionary<string, object>, User>(APIUrls.CreateAccountApi, user, null);
    }

    public async UniTask LoginAPI(User user)
    {
        _id = user.ID;

#if UNITY_EDITOR
        Debug.Log($"_id : {_id}");
#endif
        await CallAPI<Dictionary<string, object>, User>(APIUrls.LoginApi, user, HandleLoginResponse);
    }

    private void HandleLoginResponse(APIResponse<Dictionary<string, object>> apiResponse)
    {
        var responseBody = JsonConvert.DeserializeObject<Dictionary<string, object>>(apiResponse.responseBody);

        if (responseBody.TryGetValue("authToken", out var authTokenObj))
        {
            _authToken = authTokenObj as string;
        }

        if (_authToken != null)
        {
            apidata.SetResponseData("GameData", NewGameData());
        }
        else
        {
            Debug.LogError("Failed to retrieve authToken from API response.");
        }
    }

    public async UniTask GetGameDataAPI()
    {
         await CallAPI<Dictionary<string, object>, PlayerData>(APIUrls.GameDataApi, NewPlayerData(), null);
    }

    private void HandleGameDataResponse(APIResponse<Dictionary<string, object>> apiResponse)
    {
        // APIWebRequest.ParseResponseBodyToModel<PlayerData>(apiResponse.responseBody, "playerData");
    }

    public async UniTask GetRanking()
    {
        await CallAPI<Dictionary<string, object>, RankingData>(APIUrls.RankingApi, NewRankingData(), null);
    }

    private void HandleRankingDataResponse(APIResponse<Dictionary<string, object>> apiResponse)
    {
        // APIWebRequest.ParseResponseBodyToModel<RankingData[]>(apiResponse.responseBody, "rankingData");
    }

    private GameData NewGameData()
    {
        GameData gameData = new GameData
        {
            ID = _id,
            AuthToken = _authToken
        };
        return gameData;
    }

    private PlayerData NewPlayerData()
    {
        PlayerData playerData = new PlayerData
        {
            player_uid = _player_uid,
            exp = _exp,
            hp = _hp,
            score = _score,
            level = _level,
            status = _status
        };
        return playerData;
    }

    private RankingData NewRankingData()
    {
        RankingData rankingData = new RankingData
        {
            rankId = _rankId,
            rankScore = _rankScore,
            ranking = _ranking
        };
        return rankingData;
    }

    private async UniTask CallAPI<T, TRequest>(string apiUrl, TRequest requestBody, Action<APIResponse<T>> handler)
    {
        try
        {
            var apiResponse = await APIWebRequest.PostAsync<T>(apiUrl, requestBody);

            if(apiResponse == null)
            {
                Debug.LogError("API Respon is null");
            }

            handler?.Invoke(apiResponse);
        }
        catch (UnityWebRequestException e)
        {
            Debug.LogError($"API request failed : {e.Message}");
        }
    }
}