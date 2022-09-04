package co.pixelmc.models;

import java.util.UUID;

public class WebApiPostPlayer {
    private UUID uuid;
    private String playerName;

    public String getPlayerName() {
        return playerName;
    }

    public UUID getUuid() {
        return uuid;
    }

    public void setPlayerName(String playerName) {
        this.playerName = playerName;
    }

    public void setUuid(UUID uuid) {
        this.uuid = uuid;
    }
}
