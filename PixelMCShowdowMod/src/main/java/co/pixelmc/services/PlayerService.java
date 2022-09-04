package co.pixelmc.services;

import co.pixelmc.models.WebApiPlayer;
import co.pixelmc.models.WebApiPostPlayer;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import okhttp3.*;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class PlayerService {
    private final String webApiUrl;

    public PlayerService(String webApiUrl){
        this.webApiUrl = webApiUrl;
    }

    public List<WebApiPlayer> getPlayers() throws IOException {
        Gson gson = new GsonBuilder().setPrettyPrinting().create();

        OkHttpClient okHttpClient = new OkHttpClient();
        Request request = new Request.Builder()
                .url(this.webApiUrl + "/api/players")
                .method("GET", null)
                .build();

        try (Response response = okHttpClient.newCall(request).execute()) {
            String jsonResponse = response.body().string();
            WebApiPlayer[] players = gson.fromJson(jsonResponse, WebApiPlayer[].class);
            return Arrays.asList(players);
        }
    }

    public List<WebApiPlayer> addPlayers(List<WebApiPostPlayer> webApiPostPlayers) throws IOException {
        Gson gson = new GsonBuilder().setPrettyPrinting().create();

        OkHttpClient okHttpClient = new OkHttpClient();
        Request request = new Request.Builder()
                .url(this.webApiUrl + "/api/players")
                .post(RequestBody.create(gson.toJson(webApiPostPlayers).getBytes()))
                .build();

        try (Response response = okHttpClient.newCall(request).execute()) {
            String jsonResponse = response.body().string();
            WebApiPlayer[] players = gson.fromJson(jsonResponse, WebApiPlayer[].class);
            return Arrays.asList(players);
        }
    }
}
