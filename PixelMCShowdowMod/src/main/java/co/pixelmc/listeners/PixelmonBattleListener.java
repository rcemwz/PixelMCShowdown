package co.pixelmc.listeners;

import co.pixelmc.PixelMCShowdown;
import com.google.common.collect.ImmutableMap;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.pixelmonmod.pixelmon.api.battles.BattleResults;
import com.pixelmonmod.pixelmon.api.events.battles.BattleEndEvent;
import com.pixelmonmod.pixelmon.battles.controller.participants.BattleParticipant;
import net.minecraft.entity.LivingEntity;
import net.minecraft.entity.player.ServerPlayerEntity;
import okhttp3.*;

import java.io.IOException;
import java.util.*;

public class PixelmonBattleListener {
    public void onBattleCompleted(BattleEndEvent battleEndEvent){
        if (battleEndEvent.abnormal)
            return;

        List<Map<String, Object>> postPlayers = new ArrayList<>();

        ImmutableMap<BattleParticipant, BattleResults> results = battleEndEvent.results;
        for (Map.Entry<BattleParticipant, BattleResults> x : results.entrySet()){
            LivingEntity battlePartEntity = x.getKey().getEntity();
            if (!(battlePartEntity instanceof ServerPlayerEntity))
                continue;

            ServerPlayerEntity player = (ServerPlayerEntity) battlePartEntity;
            BattleResults battleResult = x.getValue();

            Map<String, Object> postPlayer = new HashMap<>();
            postPlayer.put("uuid", player.getUUID().toString());
            postPlayer.put("battleOutcome", battleResult == BattleResults.VICTORY ? 1 : 0);

            postPlayers.add(postPlayer);
        }

        Response response;
        try {
            response = postToApi(postPlayers);
        } catch (IOException e) {
            PixelMCShowdown.getLogger().error(e.getStackTrace());
            return;
        }

        PixelMCShowdown.getLogger().info(response.message());
    }

    public Response postToApi(List<Map<String, Object>> players) throws IOException {
        Gson gson = new GsonBuilder().setPrettyPrinting().create();
        String postBody = gson.toJson(players);

        OkHttpClient okHttpClient = new OkHttpClient();
        Request request = new Request.Builder()
                .url("http://localhost:89")
                .method("POST", RequestBody.create(postBody.getBytes()))
                .build();

        try (Response response = okHttpClient.newCall(request).execute()) {
            return response;
        }
    }
}
