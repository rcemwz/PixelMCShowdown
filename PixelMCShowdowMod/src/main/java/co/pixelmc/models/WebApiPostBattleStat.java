package co.pixelmc.models;

import java.util.UUID;

public class WebApiPostBattleStat {
    private UUID uuid;
    private int battleOutcome;

    public UUID getUuid() {
        return uuid;
    }

    public int getBattleOutcome() {
        return battleOutcome;
    }

    public void setUuid(UUID uuid) {
        this.uuid = uuid;
    }

    public void setBattleOutcome(int battleOutcome) {
        this.battleOutcome = battleOutcome;
    }
}
