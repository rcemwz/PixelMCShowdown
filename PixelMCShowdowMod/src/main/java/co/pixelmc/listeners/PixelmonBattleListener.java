package co.pixelmc.listeners;

import com.google.common.collect.ImmutableMap;
import com.pixelmonmod.pixelmon.api.battles.BattleResults;
import com.pixelmonmod.pixelmon.api.events.battles.BattleEndEvent;
import com.pixelmonmod.pixelmon.battles.controller.participants.BattleParticipant;
import net.minecraft.entity.LivingEntity;
import net.minecraft.entity.player.ServerPlayerEntity;
import okhttp3.OkHttpClient;

import java.net.http.HttpClient;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;
import java.util.Map;

public class PixelmonBattleListener {

    public void onBattleCompleted(BattleEndEvent battleEndEvent){
        if (battleEndEvent.abnormal)
            return;

        List<String> winners = new java.util.ArrayList<>();
        List<String> participants = new java.util.ArrayList<>();

        ImmutableMap<BattleParticipant, BattleResults> results = battleEndEvent.results;
        for (Map.Entry<BattleParticipant, BattleResults> x : results.entrySet()){
            LivingEntity battlePartEntity = x.getKey().getEntity();
            if (!(battlePartEntity instanceof ServerPlayerEntity))
                continue;

            ServerPlayerEntity player = (ServerPlayerEntity) battlePartEntity;
            BattleResults battleResult = x.getValue();

            participants.add(player.getUUID().toString());
            if (battleResult == BattleResults.VICTORY)
                winners.add(player.getUUID().toString());
        }
    }

    public void postToApi(List<String> p, List<String> winners){
        OkHttpClient okHttpClient = new OkHttpClient();

    }
}
