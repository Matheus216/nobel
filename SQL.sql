------------------------------------------------------------ SQL 1

SELECT c.nombre CampaignName,
   c.idcampanya CampaignID,
   u.Nombre AgentName,
   t.idAgent AgentID, 
   t.login AgentLogin
   count(t.idTransaccion) over(partition by c.idcampanya) totalsPeerCampaing
FROM Transaction t
INNER JOIN Usuario u ON t.idAgent = u.idusuario
INNER JOIN Campanya c ON t.idCampanya = c.idcampanya; 
WHERE t.tinicio >= c.tinicial && t.tinicio < c.tfinal
   && t.tfinal <= c.tfinal && t.tfinal > c.tinicial;


---------------------------------------------------------- SQL2 


CREATE PROCEDURE GET_TRANSACTION_PEER_CAMPAIGN
   @CampaignID, @AgentID, @FinalId
AS 
   SELECT c.nombre CampaignName,
      c.idcampanya CampaignID,
      u.Nombre AgentName,
      t.idAgent AgentID, 
      t.login AgentLogin
      count(t.idTransaccion) over(partition by c.idcampanya) totalsPeerCampaing
   FROM Transaction t
   INNER JOIN Usuario u ON t.idAgent = u.idusuario
   INNER JOIN Campanya c ON t.idCampanya = c.idcampanya; 
   WHERE t.tinicio >= c.tinicial && t.tinicio < c.tfinal
      && t.tfinal <= c.tfinal && t.tfinal > c.tinicial
      && t.idFinal = @FinalId
      && t.campanyaId = @CampaignID
      && idAgente = @AgentID;
GO; 
