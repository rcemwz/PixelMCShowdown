package co.pixelmc.listeners;

import co.pixelmc.enums.BattleOutcome;
import com.google.common.collect.ImmutableMap;
import com.pixelmonmod.pixelmon.api.battles.BattleResults;
import com.pixelmonmod.pixelmon.api.events.battles.BattleEndEvent;
import com.pixelmonmod.pixelmon.battles.controller.participants.BattleParticipant;
import net.minecraft.entity.LivingEntity;
import net.minecraft.entity.player.ServerPlayerEntity;
import okhttp3.*;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.UUID;

public class PixelmonBattleListener {

    private static class PostBattleStat{
        private List<PostPlayer> participants;

        public PostBattleStat(List<PostPlayer> players){
            this.participants = players;
        }

        public List<PostPlayer> getParticipants() {
            return participants;
        }

        public void setParticipants(List<PostPlayer> participants) {
            this.participants = participants;
        }
    }

    private static class PostPlayer {
        private UUID uuid;
        private int battleOutcome;

        PostPlayer(UUID uuid, int battleOutcome) {
            this.uuid = uuid;
            this.battleOutcome = battleOutcome;
        }

        public void setBattleOutcome(int battleOutcome) {
            this.battleOutcome = battleOutcome;
        }

        public void setUuid(UUID uuid) {
            this.uuid = uuid;
        }

        public int getBattleOutcome() {
            return battleOutcome;
        }

        public UUID getUuid() {
            return uuid;
        }
    }

    public void onBattleCompleted(BattleEndEvent battleEndEvent){
        if (battleEndEvent.abnormal)
            return;

        List<PostPlayer> postPlayers = new ArrayList<>();

        ImmutableMap<BattleParticipant, BattleResults> results = battleEndEvent.results;
        for (Map.Entry<BattleParticipant, BattleResults> x : results.entrySet()){
            LivingEntity battlePartEntity = x.getKey().getEntity();
            if (!(battlePartEntity instanceof ServerPlayerEntity))
                continue;

            ServerPlayerEntity player = (ServerPlayerEntity) battlePartEntity;
            BattleResults battleResult = x.getValue();

            postPlayers.add(new PostPlayer(player.getUUID(), battleResult == BattleResults.VICTORY ? 1 : 0));
        }

        postToApi(postPlayers);
    }

    public void postToApi(List<PostPlayer> players){
        OkHttpClient okHttpClient = new OkHttpClient();
        Request request = new Request.Builder()
                .url("http://localhost:5777")
                .method("POST", RequestBody.create("JSON HERE".getBytes()))
                .build();

        try (Response response = okHttpClient.newCall(request).execute()) {
            return response.body().string();
        }
    }
}
