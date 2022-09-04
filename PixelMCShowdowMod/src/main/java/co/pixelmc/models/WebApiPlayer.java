package co.pixelmc.models;

import java.util.Date;
import java.util.UUID;

public class WebApiPlayer {
    private int id;
    private UUID uuid;
    private String playerName;
    private int eloRating;
    private Date createdDateTime;


    public UUID getUuid() {
        return uuid;
    }

    public Date getCreatedDateTime() {
        return createdDateTime;
    }

    public int getEloRating() {
        return eloRating;
    }

    public int getId() {
        return id;
    }

    public String getPlayerName() {
        return playerName;
    }

    public void setUuid(UUID uuid) {
        this.uuid = uuid;
    }

    public void setCreatedDateTime(Date createdDateTime) {
        this.createdDateTime = createdDateTime;
    }

    public void setEloRating(int eloRating) {
        this.eloRating = eloRating;
    }

    public void setId(int id) {
        this.id = id;
    }

    public void setPlayerName(String playerName) {
        this.playerName = playerName;
    }
}
