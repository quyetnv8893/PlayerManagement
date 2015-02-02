using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayerManagement.Models.PlayerMatch
{
    public interface IPlayerMatchRepository
    {
        IEnumerable<PlayerMatch> GetPlayerMatches();
        IEnumerable<PlayerMatch> GetPlayerMatchesByPlayerId(String playerId);
        IEnumerable<PlayerMatch> GetPlayerMatchesByMatchId(String matchId);
        PlayerMatch GetPlayerMatchByPlayerIdAndMatchId(String playerId,String matchId);
        void InsertPlayerMatch(PlayerMatch playerMatch);
        void EditPlayerMatch(PlayerMatch playerMatch);
        void DeletePlayerMatch(PlayerMatch playerMatch);
    }
}