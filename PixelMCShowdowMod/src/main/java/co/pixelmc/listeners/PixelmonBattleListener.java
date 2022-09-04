package co.pixelmc.listeners;

import co.pixelmc.PixelMCShowdown;
import co.pixelmc.models.WebApiPostBattleStat;
import co.pixelmc.services.BattleStatsService;
import com.google.common.collect.ImmutableMap;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.pixelmonmod.pixelmon.api.battles.BattleResults;
import com.pixelmonmod.pixelmon.api.events.battles.BattleEndEvent;
import com.pixelmonmod.pixelmon.battles.controller.participants.BattleParticipant;
import net.minecraft.entity.LivingEntity;
import net.minecraft.entity.player.ServerPlayerEntity;
import okhttp3.*;
import org.apache.logging.log4j.Logger;

import java.io.IOException;
import java.util.*;

public class PixelmonBattleListener {

    private final BattleStatsService battleStatsService;
    private final Logger logger;

    public PixelmonBattleListener(BattleStatsService battleStatsService, Logger logger){
        this.battleStatsService = battleStatsService;
        this.logger = logger;
    }

    public void onBattleCompleted(BattleEndEvent battleEndEvent){
        if (battleEndEvent.abnormal)
            return;

        List<WebApiPostBattleStat> postBattleStats = new ArrayList<>();

        ImmutableMap<BattleParticipant, BattleResults> results = battleEndEvent.results;
        for (Map.Entry<BattleParticipant, BattleResults> x : results.entrySet()){
            LivingEntity battlePartEntity = x.getKey().getEntity();
            if (!(battlePartEntity instanceof ServerPlayerEntity))
                continue;

            ServerPlayerEntity player = (ServerPlayerEntity) battlePartEntity;
            BattleResults battleResult = x.getValue();

            WebApiPostBattleStat battleStat = new WebApiPostBattleStat();
            battleStat.setBattleOutcome(battleResult == BattleResults.VICTORY ? 1 : 0);
            battleStat.setUuid(player.getUUID());

            postBattleStats.add(battleStat);
        }

        try {
            battleStatsService.addBattleOutcome(postBattleStats);
            logger.info("logged: battle stats successfully");
        } catch (IOException e) {
            logger.error(e.getStackTrace());
        }
    }
}
