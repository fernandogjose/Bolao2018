using System.Collections.Generic;
using Core.Domain.Enums;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Models;
using Core.Domain.Validations;
using Core.WebApi.Models;

namespace Core.Domain.Services {
    public class UserPointService {

        private readonly IUserPointRepository _userPointRepository;

        private readonly UserGameService _userGameService;

        public UserPointService (IUserPointRepository userPointRepository, UserGameService userGameService) {
            _userPointRepository = userPointRepository;
            _userGameService = userGameService;
        }

        public void DeleteByOficialGameId (int oficialGameId) {
            _userPointRepository.DeleteByOficialGameId (oficialGameId);
        }

        public List<UserPointClassification> List () {
            List<UserPointClassification> userPointClassificationsResponse = _userPointRepository.List ();
            return userPointClassificationsResponse;
        }

        public void Calculate (OficialGameSaveRequest oficialGame) {
            List<UserPoint> userPoints = new List<UserPoint> (0);

            //--- obter a lista de resultados do usuário do jogo que deseja calcular
            List<UserGameScore> userGameScores = _userGameService.ListByOficialGameId (oficialGame.OficialGameId);

            //--- loop para calcular
            foreach (var userGameScore in userGameScores) {

                //--- resultado de vitoria certo com placar certo 
                if ((oficialGame.ScoreTeamA == userGameScore.ScoreTeamA && oficialGame.ScoreTeamB == userGameScore.ScoreTeamB) &&
                    ((oficialGame.ScoreTeamA > oficialGame.ScoreTeamB && userGameScore.ScoreTeamA > userGameScore.ScoreTeamB) ||
                        (oficialGame.ScoreTeamA < oficialGame.ScoreTeamB && userGameScore.ScoreTeamA < userGameScore.ScoreTeamB))) {

                    userPoints.Add (new UserPoint {
                        UserId = userGameScore.UserId,
                            OficialGameId = userGameScore.OficialGameId,
                            PointType = PointTypeEnum.ResultadoDeVitoriaCertoComPlacarCerto
                    });
                    continue;
                }

                //--- resultado de vitoria certo com um placar certo 
                if ((oficialGame.ScoreTeamA == userGameScore.ScoreTeamA || oficialGame.ScoreTeamB == userGameScore.ScoreTeamB) &&
                    ((oficialGame.ScoreTeamA > oficialGame.ScoreTeamB && userGameScore.ScoreTeamA > userGameScore.ScoreTeamB) ||
                        (oficialGame.ScoreTeamA < oficialGame.ScoreTeamB && userGameScore.ScoreTeamA < userGameScore.ScoreTeamB))) {

                    userPoints.Add (new UserPoint {
                        UserId = userGameScore.UserId,
                            OficialGameId = userGameScore.OficialGameId,
                            PointType = PointTypeEnum.ResultadoDeVitoriaCertoComUmPlacarCerto
                    });
                    continue;
                }

                //--- resultado de vitoria certo com um placar errado 
                if ((oficialGame.ScoreTeamA != userGameScore.ScoreTeamA && oficialGame.ScoreTeamB != userGameScore.ScoreTeamB) &&
                    ((oficialGame.ScoreTeamA > oficialGame.ScoreTeamB && userGameScore.ScoreTeamA > userGameScore.ScoreTeamB) ||
                        (oficialGame.ScoreTeamA < oficialGame.ScoreTeamB && userGameScore.ScoreTeamA < userGameScore.ScoreTeamB))) {

                    userPoints.Add (new UserPoint {
                        UserId = userGameScore.UserId,
                            OficialGameId = userGameScore.OficialGameId,
                            PointType = PointTypeEnum.ResultadoDeVitoriaCertoComPlacarErrado
                    });
                    continue;
                }

                //--- Resultado de empate certo com placar certo
                if (oficialGame.ScoreTeamA == userGameScore.ScoreTeamA &&
                    oficialGame.ScoreTeamB == userGameScore.ScoreTeamB &&
                    oficialGame.ScoreTeamA == oficialGame.ScoreTeamB) {

                    userPoints.Add (new UserPoint {
                        UserId = userGameScore.UserId,
                            OficialGameId = userGameScore.OficialGameId,
                            PointType = PointTypeEnum.ResultadoDeEmpateCertoComPlacarCerto
                    });
                    continue;
                }

                //--- Resultado de empate certo com placar errado
                if (oficialGame.ScoreTeamA != userGameScore.ScoreTeamA &&
                    oficialGame.ScoreTeamB != userGameScore.ScoreTeamB &&
                    oficialGame.ScoreTeamA == oficialGame.ScoreTeamB &&
                    userGameScore.ScoreTeamA == userGameScore.ScoreTeamB) {

                    userPoints.Add (new UserPoint {
                        UserId = userGameScore.UserId,
                            OficialGameId = userGameScore.OficialGameId,
                            PointType = PointTypeEnum.ResultadoDeEmpateCertoComPlacarErrado
                    });
                    continue;
                }

                //--- caso o resultado esteja errado
                userPoints.Add (new UserPoint {
                    UserId = userGameScore.UserId,
                        OficialGameId = userGameScore.OficialGameId,
                        PointType = PointTypeEnum.ResultadoErrado
                });
            }

            _userPointRepository.Create (userPoints);
        }
    }
}