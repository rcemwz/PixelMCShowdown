package co.pixelmc.services;

import co.pixelmc.models.WebApiPostBattleStat;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;

import java.io.IOException;
import java.util.List;
import java.util.Map;

public class BattleStatsService {
    private final String webApiUrl;

    public BattleStatsService(String webApiUrl){
        this.webApiUrl = webApiUrl;
    }

    public void addBattleOutcome(List<WebApiPostBattleStat> battleStats) throws IOException {
        Gson gson = new GsonBuilder().setPrettyPrinting().create();
        String postBody = gson.toJson(battleStats);

        OkHttpClient okHttpClient = new OkHttpClient();
        Request request = new Request.Builder()
                .url(this.webApiUrl + "/api/battlestats")
                .method("POST", RequestBody.create(postBody.getBytes()))
                .build();

        try (Response response = okHttpClient.newCall(request).execute()) {
            //TODO returns confirmed posts so probs good to return them here at some point too
        }
    }
}
